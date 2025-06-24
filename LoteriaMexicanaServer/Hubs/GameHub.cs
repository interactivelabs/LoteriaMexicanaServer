using LoteriaMexicanaServer.Services;
using LoteriaMexicanaTypes.Hubs;
using Microsoft.AspNetCore.SignalR;
using TypedSignalR.Client;

namespace LoteriaMexicanaServer.Hubs;

[Hub]
public interface IServerGameHub : IGameHub;

[Receiver]
public interface IServerGameHubClient : IGameHubClient;

public class GameHub(GameActionsService gameActionsService, PlayerService playerService) : Hub<IServerGameHubClient>, IServerGameHub
{
    //create player information on connection start
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
        await Clients.Group(gameRoom.Id).OnGameRoomEnter(player, gameRoom);
    }

    public async Task LeaveRoom()
    {
        var connectionId = Context.ConnectionId;
        var (playerId, gameRoomId) = gameActionsService.PlayerLeftGame(connectionId);

        await Clients.Group(gameRoomId).OnGameRoomLeave(playerId);
        await Groups.RemoveFromGroupAsync(connectionId, gameRoomId);
    }
}
