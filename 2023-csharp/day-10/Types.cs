namespace AdventOfCode;

public static class Types
{
    public static Dictionary<char, PipeDirection> DirectionMap = new()
    {
        { '|', PipeDirection.NorthAndSouth },
        { '-', PipeDirection.EastAndWest },
        { 'L', PipeDirection.NorthAndEast },
        { 'J', PipeDirection.NorthAndWest },
        { '7', PipeDirection.SouthAndWest },
        { 'F', PipeDirection.SouthAndEast },
        { '.', PipeDirection.NoDirection },
        { 'S', PipeDirection.StartingPosition }
    };
}

public struct Coordinate( int x, int y )
{
    public int X => x;
    public int Y => y;

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
    NoDirection,
    StartingPosition
}
