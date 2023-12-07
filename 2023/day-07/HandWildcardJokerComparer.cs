namespace AdventOfCode;

public class HandWildcardJokerComparer : IComparer<Hand>
{
    private static readonly Dictionary<char, byte> _labelMapWildcardJoker = new()
    {
        { 'A', 1 },
        { 'K', 2 },
        { 'Q', 3 },
        { 'T', 4 },
        { '9', 5 },
        { '8', 6 },
        { '7', 7 },
        { '6', 8 },
        { '5', 9 },
        { '4', 10 },
        { '3', 11 },
        { '2', 12 },
        { 'J', 13 }
    };

    public int Compare( Hand? a, Hand? b )
    {
        return Hand.Compare( a, b, _labelMapWildcardJoker );
    }
}
