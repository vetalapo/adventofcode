namespace AdventOfCode;

public static class Types
{
    public static Dictionary<char, PipeDirection> Direction = new()
    {
        { '|', PipeDirection.NorthAndSouth },
        { '-', PipeDirection.EastAndWest },
        { 'L', PipeDirection.NorthAndEast },
        { 'J', PipeDirection.NorthAndWest },
        { '7', PipeDirection.SouthAndWest },
        { 'F', PipeDirection.SouthAndEast },
        { '.', PipeDirection.NoDirection }
    };
}

public struct Coords( int x, int y )
{
    public int X => x;
    public int Y => y;
    public int Steps = 0;

    public override string ToString() => $"({X}, {Y})";
}

public enum PipeDirection
{
    NorthAndSouth,
    EastAndWest,
    NorthAndEast,
    NorthAndWest,
    SouthAndEast,
    SouthAndWest,
    NoDirection
}
