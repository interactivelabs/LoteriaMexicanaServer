namespace LoteriaMexicanaTypes.Records;

public record GameRoom
{
    public required string Id { get; init; }
    public required string DisplayName  { get; init; }
    public int Seeder  { get; init; }
    public bool Full  { get; set; }
}
