using LoteriaMexicanaServer.Managers;
using LoteriaMexicanaServer.Models;
using Microsoft.AspNetCore.SignalR;

namespace LoteriaMexicanaServer.Hubs;

public class GameHub(GameManager gameManager, PlayerManager playerManager) : Hub
{
    //create player information on connection start
    public override Task OnConnectedAsync()
    {
        playerManager.AddPlayer(new Player() { Id = Context.ConnectionId });
        return base.OnConnectedAsync();
    }

    public async Task JoinRoom()
    {
        var connectionId = Context.ConnectionId;
        var player = playerManager.GetPlayerById(connectionId);
        if (player == null) throw new Exception("Player not found");

        var gameRoom = gameManager.GetFirstEmptyRoom();
        if (gameRoom == null)
        {
            gameRoom = gameManager.CreateGameRoom();
            await Groups.AddToGroupAsync(connectionId, gameRoom.Id);
        }

        player.CurrentRoom = gameRoom.Id;
        gameManager.AddPlayer(player, gameRoom.Id);

        await Clients.Group(gameRoom.Id).SendAsync("New Player Joined Room", gameRoom.Id);
    }

    public async Task LeaveRoom()
    {
        var connectionId = Context.ConnectionId;
        var player = playerManager.GetPlayerById(connectionId);
        if (player == null) throw new Exception("Player not found");

        gameManager.RemovePlayer(player, player.CurrentRoom);
        await Clients.Group(player.CurrentRoom).SendAsync("Player Left Room", player.CurrentRoom);
    }
}
