namespace LoteriaMexicanaTypes.Records;

public record Sheet
{
    public required string Id { get; init; }
    public List<int> CardIds { get; init; } = [];
}