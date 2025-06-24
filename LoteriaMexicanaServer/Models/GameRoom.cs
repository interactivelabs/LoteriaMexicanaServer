namespace LoteriaMexicanaServer.Models;

public record GameRoom(int PlayersLimit = 10) : LoteriaMexicanaTypes.Records.GameRoom
{
    public int PlayersLimit { get; } = PlayersLimit;
    public List<Player> Players { get; init; } = [];
    public List<Sheet> Sheets { get; set; } = [];

    public Dictionary<string, string> PlayerSheets { get; } = new();
}