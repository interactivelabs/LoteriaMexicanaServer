namespace LoteriaMexicanaTypes.Records;

public record Player
{
    public required string Id { get; init; }
    public string DisplayName { get; init; } = string.Empty;
    public Sheet MySheet { get; set; } = null!;
}