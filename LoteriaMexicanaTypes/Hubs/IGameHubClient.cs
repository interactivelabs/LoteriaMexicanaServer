using LoteriaMexicanaTypes.Records;

namespace LoteriaMexicanaTypes.Hubs;

public interface IGameHubClient
{
    Task OnGameRoomEnter(Player player, GameRoom gameRoom);
    Task OnGameRoomLeave(string playerId);
}