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

        Sequencer sequencer = new( inputFilePath );

        // Part I
        IEnumerable<long> lastPredictedNums = sequencer.ProcessSequencesForward();

        WriteLine( $"The sum of the extrapolated values: {lastPredictedNums.Sum()}" );

        // Part II
        IEnumerable<long> predictedHistoryNums = sequencer.ProcessSequencesBackwards();

        WriteLine( $"The sum of the historically extrapolated values: {predictedHistoryNums.Sum()}" );
    }
}
