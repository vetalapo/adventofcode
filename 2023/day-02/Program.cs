using System;
using static System.Console;

namespace AdventOfCode;

public class Program
{
    public static void Main( string[] args )
    {
        if ( args.Length != 1 )
        {
            WriteLine( "Unsupported amount of arguments" );
            
            return;
        }

        string inputFile = args[0];

        if ( !File.Exists( inputFile ) )
        {
            WriteLine( $"File [{inputFile}] does not exist" );
            
            return;
        }

        List<Game> games = [];
        
        foreach ( string line in File.ReadLines( inputFile ) )
        {
            games.Add( Game.ParseGame( line ) );
        }

        WriteLine( games.Count );
    }
}
