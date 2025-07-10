using LoteriaMexicanaTypes.Records;

namespace LoteriaMexicanaTypes.Hubs;

public interface IGameHubClient
{
    // Notify All Players in the Room there is a new player
    Task OnPlayerEnteredGameRoom(Player player, GameRoom gameRoom);

    // Notify the player he joined a room
    Task OnGameRoomEnter(Player player, GameRoom gameRoom);

    Task OnGameRoomLeave(string playerId);

    Task OnGameWinner();

    Task OnPlayerWon(string playerId);

    Task OnGameLost();

    Task OnPlayerLost(string playerId);
}
