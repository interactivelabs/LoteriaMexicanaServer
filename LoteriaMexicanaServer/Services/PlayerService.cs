using LoteriaMexicanaServer.Managers;
using LoteriaMexicanaTypes.Records;

namespace LoteriaMexicanaServer.Services;

public class PlayerService(PlayerManager playerManager)
{
    public void PlayerJoinedGame(string connectionId)
    {
        var player = new Models.Player { Id = connectionId };
        playerManager.AddPlayer(player);
    }
}
