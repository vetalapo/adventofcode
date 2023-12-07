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
        
        long totalWinnings = 0;

        for ( int i = 0; i < hands.Length; i++ )
        {
            totalWinnings += (i + 1) * hands[i].Bid;
        }

        WriteLine( $"Total winnings: {totalWinnings}" );
    }
}
