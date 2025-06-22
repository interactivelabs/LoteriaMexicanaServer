using LoteriaMexicanaServer.Models;

namespace LoteriaMexicanaServer.Managers;

public class GameManager
{
    private readonly List<GameRoom> _gameRooms = [];

    public GameRoom CreateGameRoom(string displayName = "New Room")
    {
        var uniqueId = Guid.NewGuid();
        var gameRoom = new GameRoom
        {
            Id = uniqueId.ToString(),
            DisplayName = displayName
        };
        _gameRooms.Add(gameRoom);
        return gameRoom;
    }

    public GameRoom? GetFirstEmptyRoom() => _gameRooms.FirstOrDefault(gr => gr.Full == false);

    public void AddPlayer(Player player, string gameRoomId)
    {
        var gameRoom = _gameRooms.FirstOrDefault(x => x.Id == gameRoomId);
        if(gameRoom == null) throw new Exception("Game room not found");

        gameRoom.Players.Add(player);
        if (gameRoom.Players.Count >= gameRoom.PlayersLimit) gameRoom.Full = true;
    }

    public void RemovePlayer(Player player, string gameRoomId)
    {
        var gameRoom = _gameRooms.FirstOrDefault(x => x.Id == gameRoomId);
        if(gameRoom == null) throw new Exception("Game room not found");

        gameRoom.Players.Remove(player);
    }
}
