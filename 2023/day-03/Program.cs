using System;
using static System.Console;

namespace AdventOfCode;

public class Program
{
    public static void Main( string[] args )
    {
        if ( args.Length == 0 )
        {
            throw new ArgumentOutOfRangeException( "No aruments provided" );
        }

        string inputFile = args[0];

        if ( !File.Exists( inputFile ) )
        {
            throw new FileNotFoundException( $"Cannot locate [{inputFile}]" );
        }

        WriteLine( $"Hello day 3!\r\nInput: {inputFile}" );

        foreach( string line in File.ReadLines( inputFile ) )
        {
            WriteLine( line );
        }
    }
}
