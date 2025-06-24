namespace LoteriaMexicanaTypes.Records;

public record Sheet
{
    public required string Id { get; init; }
    public int[] CardIds { get; protected init; } = [];
}
