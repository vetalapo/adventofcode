namespace AdventOfCode;

public class Card
{
    public int Number { get; private set; }

    public int Copies { get; private set; }

    public HashSet<int> Winning { get; private set; } = [];

    public HashSet<int> Have { get; private set; } = [];

    public Card() {}

    public Card( string singleTextCard )
    {
        Card card = Card.Parse( singleTextCard );

        this.Number = card.Number;
        this.Winning = card.Winning;
        this.Have = card.Have;
    }

    public Card( int number, IEnumerable<int> winning, IEnumerable<int> have )
    {
        this.Number = number;
        this.Winning = new HashSet<int>( winning );
        this.Have = new HashSet<int>( have );
    }

    public int Points
    {
        get
        {
            return (int)Math.Pow( 2, this.MatchingNumberCount - 1 );
        }
    }

    public int MatchingNumberCount
    {
        get
        {
            return WinningNumbers().Count();
        }
    }

    public void AddUpCopy()
    {
        this.Copies++;
    }

    public static IEnumerable<Card> ParseFile( string path )
    {
        foreach ( string line in File.ReadLines( path ) )
        {
            yield return new Card( line );
        }
    }

    public static Card Parse( string textCard )
    {
        // Format: Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
        string[] cardParts = textCard.Split( ':', StringSplitOptions.RemoveEmptyEntries );
        string[] cardIdParts = cardParts[0].Trim().Split( ' ', StringSplitOptions.RemoveEmptyEntries );
        string[] numParts = cardParts[1].Trim().Split( '|', StringSplitOptions.RemoveEmptyEntries );
        string[] winNums = numParts[0].Trim().Split( ' ', StringSplitOptions.RemoveEmptyEntries );
        string[] haveNums = numParts[1].Trim().Split( ' ', StringSplitOptions.RemoveEmptyEntries );

        int.TryParse( cardIdParts[1], out int parsedCardNum );

        return new Card( parsedCardNum, GetInts( winNums ), GetInts( haveNums ) );
    }

    public IEnumerable<int> WinningNumbers()
    {
        foreach ( int num in this.Winning )
        {
            if ( this.Have.Contains( num ) )
            {
                yield return num; 
            }
        }
    }

    private static IEnumerable<int> GetInts( string[] nums )
    {
        foreach ( string num in nums )
        {
            int.TryParse( num.Trim(), out int parsedNum );

            yield return parsedNum;
        }
    }
}
