namespace LoteriaMexicanaServer.Constants;

public enum WinningPatternType
{
    Row,
    Column,
    Diagonal,
    Corners,
    Pozo
}

public static class WinningPatterns
{
    private static readonly Dictionary<WinningPatternType, List<List<int>>> Patterns = new()
    {
        {
            WinningPatternType.Row, [
                new List<int> { 0, 1, 2, 3 },
                new List<int> { 4, 5, 6, 7 },
                new List<int> { 8, 9, 10, 11 },
                new List<int> { 12, 13, 14, 15 }
            ]
        },
        {
            WinningPatternType.Column, [
                new List<int> { 0, 4, 8, 12 },
                new List<int> { 1, 5, 9, 13 },
                new List<int> { 2, 6, 10, 14 },
                new List<int> { 3, 7, 11, 15 }
            ]
        },
        {
            WinningPatternType.Diagonal, [
                new List<int> { 0, 5, 10, 15 },
                new List<int> { 3, 6, 9, 12 }
            ]
        },
        {
            WinningPatternType.Corners, [
                new List<int> { 0, 3, 12, 15 }
            ]
        },
        {
            WinningPatternType.Pozo, [
                new List<int> { 0, 1, 4, 5 },
                new List<int> { 1, 2, 5, 6 },
                new List<int> { 2, 3, 6, 7 },
                new List<int> { 4, 5, 8, 9 },
                new List<int> { 5, 6, 9, 10 },
                new List<int> { 6, 7, 10, 11 },
                new List<int> { 8, 9, 12, 13 },
                new List<int> { 9, 10, 13, 14 },
                new List<int> { 10, 11, 14, 15 }
            ]
        }
    };

    public static List<List<int>> GetAllPatterns()
    {
        return Patterns.Values.SelectMany(p => p).ToList();
    }

    public static List<List<int>> GetPatterns(IEnumerable<WinningPatternType> patternTypes)
    {
        return patternTypes.SelectMany(pt => Patterns[pt]).ToList();
    }
}
