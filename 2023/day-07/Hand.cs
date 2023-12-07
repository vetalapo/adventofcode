namespace AdventOfCode;

public class Hand : IComparable
{
    private static readonly Dictionary<char, Label> _labelMap = new()
    {
        { 'A', Label.A },
        { 'K', Label.K },
        { 'Q', Label.Q },
        { 'J', Label.J },
        { 'T', Label.T },
        { '9', Label._9 },
        { '8', Label._8 },
        { '7', Label._7 },
        { '6', Label._6 },
        { '5', Label._5 },
        { '4', Label._4 },
        { '3', Label._3 },
        { '2', Label._2 }
    };

    public string Value { get; private set; } = string.Empty;

    public int Bid { get; private set; }
    
    public HandType Type { get; private set; } = HandType._undefined;

    public Hand() { }

    public Hand( string value, int bid )
    {
        this.Value = value;
        this.Bid = bid;
        this.Type = CalcType( value );
    }

    public override string ToString()
    {
        return $"{this.Value} - [{this.Type}]";
    }

    public int CompareTo( object? obj )
    {
        if ( obj is null )
        {
            return 1;
        }

        if ( obj is not Hand otherHand )
        {
            throw new ArgumentException( $"Object is not {nameof(Hand)}" );
        }

        if ( this.Type < otherHand.Type )
        {
            return 1;
        }
        else if ( this.Type > otherHand.Type )
        {
            return -1;
        }

        for ( int i = 0; i < this.Value.Length; i++ )
        {
            Label thisCard = _labelMap[this.Value[i]];
            Label otherCard = _labelMap[otherHand.Value[i]];

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

    public static IEnumerable<Hand> Parse( string inputFilePath )
    {
        if ( !File.Exists( inputFilePath ) )
        {
            throw new FileNotFoundException( $"Input file not fount at [{inputFilePath}]" );
        }

        foreach ( string line in File.ReadLines( inputFilePath ) )
        {
            string[] lineParts = line.Trim().Split( ' ', StringSplitOptions.RemoveEmptyEntries );

            int.TryParse( lineParts[1], out int bid );

            yield return new Hand( lineParts[0].Trim(), bid );
        }
    }

    private static HandType CalcType( string handValue )
    {
        Dictionary<char, int> countMap = [];

        foreach ( char c in handValue )
        {
            countMap.TryAdd( c, 0 );
            countMap[c]++;
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
