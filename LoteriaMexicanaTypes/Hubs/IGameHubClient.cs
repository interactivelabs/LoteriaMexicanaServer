using LoteriaMexicanaTypes.Records;

namespace LoteriaMexicanaTypes.Hubs;

public interface IGameHubClient
{
    Task PlayerEnteredRoom(Player player, GameRoom gameRoom);

    Task RoomEnter(Player player, GameRoom gameRoom);

    Task RoomLeave(string playerId);

    Task GameWinner();

    Task PlayerWon(string playerId);

    Task GameLost();

    Task PlayerLost(string playerId);
}