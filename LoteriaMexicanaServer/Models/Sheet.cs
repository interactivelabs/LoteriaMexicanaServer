using AllCards = LoteriaMexicanaTypes.Models.Cards;

namespace LoteriaMexicanaServer.Models;

public record Sheet : LoteriaMexicanaTypes.Records.Sheet
{
    public Sheet(int seeder)
    {
        Id = Guid.NewGuid().ToString();
        var random = new Random(seeder);
        CardIds = AllCards.All
            .OrderBy(_ => random.Next())
            .Take(16)
            .OrderBy(card => card.Id)
            .Select(card => card.Id)
            .ToList();
    }
}
