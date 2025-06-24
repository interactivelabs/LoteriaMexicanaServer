namespace LoteriaMexicanaServer.Models;

public record GameRoom(int PlayersLimit = 10) : LoteriaMexicanaTypes.Records.GameRoom
{
    public int PlayersLimit { get; set; } = PlayersLimit;
    public List<Player> Players { get; set; } = [];
}
