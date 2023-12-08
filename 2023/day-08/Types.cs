namespace AdventOfCode;

public class Node( string left, string right )
{
    public string Left = left;
    public string Right = right;
}

public record Map( string Key, Node Node );

public enum InstructionType
{
    Left,
    Right,
    _undefined
}
