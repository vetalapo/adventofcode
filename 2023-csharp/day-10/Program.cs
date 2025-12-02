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

        Walker walker = new( inputFilePath );

        WriteLine( $"Steps to the point farthest from the starting position: {walker.CalcFarthestDistanceFromStart()}" );
    }
}
