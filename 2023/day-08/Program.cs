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

        Network network = new( inputFilePath );

        // Part I
        long steps = network.Steps( "AAA", "ZZZ" );

        WriteLine( $"Amount of steps to reach ZZZ: {steps}" );
    }
}
