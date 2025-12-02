namespace AdventOfCode;

public class Hand : IComparable
{
    private static readonly Dictionary<char, byte> _cardMap = new()
    {
        { 'A', 1 },
        { 'K', 2 },
        { 'Q', 3 },
        { 'J', 4 },
        { 'T', 5 },
        { '9', 6 },
        { '8', 7 },
        { '7', 8 },
        { '6', 9 },
        { '5', 10 },
        { '4', 11 },
        { '3', 12 },
        { '2', 13 }
    };

    public string Value { get; private set; } = string.Empty;

    public int Bid { get; private set; }
    
    public HandType Type { get; private set; } = HandType._undefined;

    public Hand() { }

    public Hand( string value, int bid, bool isJokerWildcard = false )
    {
        this.Value = value;
        this.Bid = bid;
        this.Type = CalcType( value, isJokerWildcard );
    }

    public override string ToString()
    {
        return $"{this.Value} - [{this.Type}]";
    }

    public int CompareTo( object? obj )
    {
        return Compare( this, obj, _cardMap );
    }

    public static int Compare( object? a, object? b, Dictionary<char, byte> labelMap )
    {
        if ( a is not Hand aHand || b is not Hand bHand )
        {
            throw new ArgumentException( $"Object is not {nameof(Hand)}" );
        }

        if ( aHand.Type < bHand.Type )
        {
            return 1;
        }
        else if ( aHand.Type > bHand.Type )
        {
            return -1;
        }

        for ( int i = 0; i < aHand.Value.Length; i++ )
        {
            byte thisCard = labelMap[aHand.Value[i]];
            byte otherCard = labelMap[bHand.Value[i]];

            if ( thisCard < otherCard )
            {
                return 1;
            }
            else if ( thisCard > otherCard )
            {
                return -1;
            }
        }

        return 0;
    }

    public static IEnumerable<Hand> Parse( string inputFilePath, bool isJokerWildcard = false )
    {
        if ( !File.Exists( inputFilePath ) )
        {
            throw new FileNotFoundException( $"Input file not fount at [{inputFilePath}]" );
        }

        foreach ( string line in File.ReadLines( inputFilePath ) )
        {
            string[] lineParts = line.Trim().Split( ' ', StringSplitOptions.RemoveEmptyEntries );

            int.TryParse( lineParts[1], out int bid );

            yield return new Hand( lineParts[0].Trim(), bid, isJokerWildcard );
        }
    }

    private static HandType CalcType( string handValue, bool isJokerWildcard = false )
    {
        Dictionary<char, int> countMap = [];

        foreach ( char c in handValue )
        {
            countMap.TryAdd( c, 0 );
            countMap[c]++;
        }

        if ( isJokerWildcard && countMap.TryGetValue( 'J', out int jokerCardAmount ) && countMap.Count > 1 )
        {
            char maxCard = countMap
                .Where( x => x.Key != 'J' )
                .Aggregate( (l, r) => l.Value > r.Value ? l : r ).Key;

            countMap[maxCard] += jokerCardAmount;
            countMap.Remove( 'J' );
        }

        int maxRepeated = countMap.Values.Max();
        int minRepeated = countMap.Values.Min();
        int amountPairs = countMap.Values.Where( x => x == 2 ).Count();

        return maxRepeated switch
        {
            5 => HandType.FiveOfAKind,
            4 => HandType.FourOfAKind,
            3 when minRepeated == 2 => HandType.FullHouse,
            3 when minRepeated == 1 => HandType.ThreeOfAKind,
            2 when amountPairs == 2 => HandType.TwoPair,
            2 when amountPairs == 1 => HandType.OnePair,
            1 => HandType.HighCard,
            _ => HandType._undefined
        };
    }
}
