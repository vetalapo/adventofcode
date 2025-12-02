using static System.Console;

namespace AdventOfCode;

public class Program
{
    public static void Main( string[] args )
    {
        string inputFilePath = "input.txt";

        if ( args.Length > 0 )
        {
            inputFilePath = args[0];
        }

        // Part I
        Hand[] hands = [.. Hand.Parse( inputFilePath ).Order()];

        WriteLine( $"Total winnings: {SumWinnings( ref hands )}" );

        // Part II
        Hand[] handsWildcardJoker = [.. Hand.Parse( inputFilePath, isJokerWildcard: true ).OrderBy( x => x, new HandWildcardJokerComparer())];
        
        WriteLine( $"Total winnings with wildcard Joker: {SumWinnings( ref handsWildcardJoker )}" );
    }

    private static long SumWinnings( ref Hand[] hands )
    {
        long totalWinnings = 0;

        for ( int i = 0; i < hands.Length; i++ )
        {
            totalWinnings += (i + 1) * hands[i].Bid;
        }

        return totalWinnings;
    }
}
