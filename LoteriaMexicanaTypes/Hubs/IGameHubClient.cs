using LoteriaMexicanaTypes.Records;

namespace LoteriaMexicanaTypes.Hubs;

public interface IGameHubClient
{
    Task OnGameRoomEnter(string playerId, GameRoom gameRoom);
    Task OnGameRoomLeave(string playerId);
}
