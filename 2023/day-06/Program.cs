using static System.Console;

namespace AdventOfCode;

public class Program
{
    public static void Main( string[] args )
    {
        string inputFilePath = "itput.txt";

        if ( args.Length > 0 && !String.IsNullOrWhiteSpace( args[0] ) )
        {
            inputFilePath = args[0];
        }

        if ( !File.Exists( inputFilePath ) )
        {
            throw new FileNotFoundException( $"Input file is not found at [{inputFilePath}]" );
        }

        WriteLine( $"Day 6 of Advent of Code! File path: [{inputFilePath}]" );
    }
}
