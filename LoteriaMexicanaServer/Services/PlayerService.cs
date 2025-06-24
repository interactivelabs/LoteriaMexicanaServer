using LoteriaMexicanaServer.Managers;
using LoteriaMexicanaTypes.Records;

namespace LoteriaMexicanaServer.Services;

public class PlayerService(PlayerManager playerManager)
{
    public Player PlayerJoinedGame(string connectionId)
    {
        var player = new Models.Player() { Id = connectionId };
        playerManager.AddPlayer(player);
        return new Player
        {
            Id = player.Id,
            DisplayName = player.DisplayName,
            MySheet = player.MySheet
        };
    }
}
