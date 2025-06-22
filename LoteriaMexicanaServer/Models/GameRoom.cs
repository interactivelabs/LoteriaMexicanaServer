namespace LoteriaMexicanaServer.Models;

public class GameRoom(int playersLimit = 10)
{
    public string Id { get; init; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public int PlayersLimit { get; set; } = playersLimit;
    public bool Full { get; set; }
    public List<Player> Players { get; set; } = [];
}
