namespace LoteriaMexicanaServer.Models;

public record Player : LoteriaMexicanaTypes.Records.Player
{
    public string CurrentRoom { get; set; } = string.Empty;
}