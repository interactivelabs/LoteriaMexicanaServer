using LoteriaMexicanaServer.Managers;
using LoteriaMexicanaTypes.Records;
using LoteriaMexicanaTypes.Hubs;
using Microsoft.AspNetCore.SignalR;
using TypedSignalR.Client;
using Player = LoteriaMexicanaServer.Models.Player;

namespace LoteriaMexicanaServer.Hubs;

[Hub]
public interface IServerGameHub : IGameHub;

[Receiver]
public interface IServerGameHubClient : IGameHubClient;

public class GameHub(GameManager gameManager, PlayerManager playerManager) : Hub<IServerGameHubClient>, IServerGameHub
{
    //create player information on connection start
    public override Task OnConnectedAsync()
    {
        playerManager.AddPlayer(new Player { Id = Context.ConnectionId });
        return base.OnConnectedAsync();
    }

    public async Task JoinRoom()
    {
        var connectionId = Context.ConnectionId;
        var player = playerManager.GetPlayerById(connectionId);
        if (player == null) throw new Exception("Player not found");

        var gameRoom = gameManager.GetFirstEmptyRoom() ?? gameManager.CreateGameRoom();

        player.CurrentRoom = gameRoom.Id;
        gameManager.AddPlayer(player, gameRoom.Id);

        GameRoom roomRecord = new(gameRoom.Id, gameRoom.DisplayName, gameRoom.Seeder, gameRoom.Full);

        await Groups.AddToGroupAsync(connectionId, gameRoom.Id);
        await Clients.Group(gameRoom.Id).OnGameRoomEnter(player.Id, roomRecord);
    }

    public async Task LeaveRoom()
    {
        var connectionId = Context.ConnectionId;
        var player = playerManager.GetPlayerById(connectionId);
        if (player == null) throw new Exception("Player not found");

        var gameRoomId = player.CurrentRoom;
        gameManager.RemovePlayer(player, gameRoomId);
        if (gameManager.IsRoomEmpty(gameRoomId)) gameManager.RemoveRoom(gameRoomId);

        await Clients.Group(gameRoomId).OnGameRoomLeave(player.Id);
        await Groups.RemoveFromGroupAsync(connectionId, gameRoomId);

    }
}
