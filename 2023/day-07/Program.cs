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

        if ( !File.Exists( inputFilePath ) )
        {
            throw new FileNotFoundException( $"Input file not fount at [{inputFilePath}]" );
        }

        WriteLine( $"Day 7 Advent of code! Hello!\nInput File: {inputFilePath}" );
    }
}
