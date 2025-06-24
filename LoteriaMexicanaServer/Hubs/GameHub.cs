using LoteriaMexicanaServer.Managers;
using LoteriaMexicanaTypes.Records;
using LoteriaMexicanaTypes.Hubs;
using Microsoft.AspNetCore.SignalR;
using TypedSignalR.Client;
using Player = LoteriaMexicanaServer.Models.Player;
using PlayerRecord = LoteriaMexicanaTypes.Records.Player;

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

        var availableSheet = gameRoom.Sheets.FirstOrDefault(sheet => !gameRoom.PlayerSheets.ContainsKey(sheet.Id));
        if (availableSheet == null) throw new Exception("No available sheets");

        gameRoom.PlayerSheets.Add(availableSheet.Id, player.Id);

        player.CurrentRoom = gameRoom.Id;
        player.MySheet = availableSheet;

        gameManager.AddPlayer(player, gameRoom.Id);

        var roomRecord = new GameRoom
        {
            Id = gameRoom.Id,
            DisplayName = gameRoom.DisplayName,
            Seeder = gameRoom.Seeder,
            Full = gameRoom.Full
        };

        var playerRecord = new PlayerRecord
        {
            Id = player.Id,
            DisplayName = player.DisplayName,
            MySheet = player.MySheet
        };

        await Groups.AddToGroupAsync(connectionId, gameRoom.Id);
        await Clients.Group(gameRoom.Id).OnGameRoomEnter(playerRecord, roomRecord);
    }

    public async Task LeaveRoom()
    {
        var connectionId = Context.ConnectionId;
        var player = playerManager.GetPlayerById(connectionId);
        if (player == null) throw new Exception("Player not found");

        var gameRoomId = player.CurrentRoom;
        gameManager.RemovePlayer(player, gameRoomId);
        if (gameManager.IsRoomEmpty(gameRoomId)) gameManager.RemoveRoom(gameRoomId);

        playerManager.RemovePlayer(player);

        await Clients.Group(gameRoomId).OnGameRoomLeave(player.Id);
        await Groups.RemoveFromGroupAsync(connectionId, gameRoomId);
    }
}