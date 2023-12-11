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
            throw new FileNotFoundException( $"File not found at: {inputFilePath}" );
        }

        WriteLine( $"Hello, day 10! Input file: {inputFilePath}" );
    }
}
