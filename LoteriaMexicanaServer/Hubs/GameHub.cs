using LoteriaMexicanaServer.Services;
using LoteriaMexicanaTypes.Hubs;
using Microsoft.AspNetCore.SignalR;
using TypedSignalR.Client;

namespace LoteriaMexicanaServer.Hubs;

[Hub]
public interface IServerGameHub : IGameHub;

[Receiver]
public interface IServerGameHubClient : IGameHubClient;

public class GameHub(GameActionsService gameActionsService, PlayerService playerService)
    : Hub<IServerGameHubClient>, IServerGameHub
{
    public override Task OnConnectedAsync()
    {
        playerService.PlayerJoinedGame(Context.ConnectionId);
        return base.OnConnectedAsync();
    }

    public async Task JoinRoom()
    {
        var connectionId = Context.ConnectionId;
        var (gameRoom, player) = gameActionsService.PlayerJoinRoom(connectionId);

        await Groups.AddToGroupAsync(connectionId, gameRoom.Id);
        await Clients.Group(gameRoom.Id).OnPlayerEnteredGameRoom(player, gameRoom);
        await Clients.Caller.OnGameRoomEnter(player, gameRoom);
    }

    public async Task LeaveRoom()
    {
        var connectionId = Context.ConnectionId;
        var (playerId, gameRoomId) = gameActionsService.PlayerLeftGameRoom(connectionId);

        await Clients.Group(gameRoomId).OnGameRoomLeave(playerId);
        await Groups.RemoveFromGroupAsync(connectionId, gameRoomId);
    }

    public async Task CallLoteria(Dictionary<int, bool> checkedCards)
    {
        var connectionId = Context.ConnectionId;
        var player = playerService.GetPlayerById(connectionId);

        if (player == null) throw new Exception("Player not found");

        var isWinner = gameActionsService.PlayerCalledLoteria(player, checkedCards);

        if (isWinner)
        {
            await Clients.Caller.OnGameWinner();
            await Clients.Group(player.CurrentRoom).OnPlayerWon(player.Id);
        }
        else
        {
            await Clients.Caller.OnGameLost();
            await Clients.Group(player.CurrentRoom).OnPlayerLost(player.Id);
        }
    }
}
