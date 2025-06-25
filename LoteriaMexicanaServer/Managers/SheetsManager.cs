using LoteriaMexicanaServer.Models;

namespace LoteriaMexicanaServer.Managers;

public static class SheetsManager
{
    public static List<Sheet> GetNewSheets(int amount, int seeder)
    {
        var sheets = new List<Sheet>();
        const int maxAttempts = 1000; // Safeguard against infinite loops
        var attempt = 0;

        while (sheets.Count < amount && attempt < maxAttempts)
        {
            var sheet = new Sheet(seeder + attempt)
            {
                Id = Guid.NewGuid().ToString()
            };

            if (!SheetExists(sheet, sheets)) sheets.Add(sheet);

            attempt++;
        }

        if (sheets.Count < amount)
            throw new InvalidOperationException(
                $"Could not generate {amount} unique tablas after {maxAttempts} attempts");

        return sheets;
    }

    private static bool SheetExists(Sheet newSheet, List<Sheet> currentSheets)
    {
        return currentSheets.Any(sheet => newSheet.CardIds.Count == sheet.CardIds.Count &&
                                          newSheet.CardIds.All(card => sheet.CardIds.Contains(card)));
    }
}
