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
        IEnumerable<long> lastPredictedNums = Sequencer.ProcessSequences( inputFilePath );

        WriteLine( $"The sum of the extrapolated values: {lastPredictedNums.Sum()}" );
    }
}
