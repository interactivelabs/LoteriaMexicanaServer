namespace LoteriaMexicanaTypes.Records;

public record GameRoom(
    string Id,
    string DisplayName,
    int Seeder,
    bool Full
);
