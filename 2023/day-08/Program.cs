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
        Network network = new( inputFilePath );

        network.IterateFromToKey( "AAA", "ZZZ" );

        WriteLine( $"Amount of steps to reach ZZZ: {network.Count}" );
    }
}
