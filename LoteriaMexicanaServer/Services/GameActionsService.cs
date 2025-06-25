using LoteriaMexicanaServer.Managers;
using LoteriaMexicanaTypes.Records;

namespace LoteriaMexicanaServer.Services;

public class GameActionsService(GameRoomManager gameRoomManager, PlayerManager playerManager)
{
    public (GameRoom, Player) PlayerJoinRoom(string connectionId)
    {
        var player = playerManager.GetPlayerById(connectionId);
        if (player == null) throw new Exception("Player not found");

        var gameRoom = gameRoomManager.GetFirstEmptyRoom() ?? gameRoomManager.CreateGameRoom();

        var availableSheet = gameRoom.Sheets.FirstOrDefault(sheet => !gameRoom.PlayerSheets.ContainsKey(sheet.Id));
        if (availableSheet == null) throw new Exception("No available sheets");

        gameRoom.PlayerSheets.Add(availableSheet.Id, player.Id);

        player.CurrentRoom = gameRoom.Id;
        player.MySheet = availableSheet;

        gameRoomManager.AddPlayer(player, gameRoom.Id);

        var roomRecord = new GameRoom
        {
            Id = gameRoom.Id,
            DisplayName = gameRoom.DisplayName,
            Seeder = gameRoom.Seeder,
            Full = gameRoom.Full
        };

        var playerRecord = new Player
        {
            Id = player.Id,
            DisplayName = player.DisplayName,
            MySheet = player.MySheet
        };

        return (roomRecord, playerRecord);
    }

    public (string playerId, string roomId) PlayerLeftGameRoom(string connectionId)
    {
        var player = playerManager.GetPlayerById(connectionId);
        if (player == null) throw new Exception("Player not found");

        gameRoomManager.RemovePlayer(player, player.CurrentRoom);
        if (gameRoomManager.IsRoomEmpty(player.CurrentRoom)) gameRoomManager.RemoveRoom(player.CurrentRoom);

        return (player.Id, player.CurrentRoom);
    }
}
