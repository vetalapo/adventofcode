using System;
using System.Security.Cryptography.X509Certificates;
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
            games.Add( Game.Parse( line ) );
        }

        IEnumerable<int> notValidGames = games
            .Where( x => x.CubeSet.Any( y => y
            .Any( z => ( z.Color == Color.Red && z.Amount > 12 ) ||
                     ( z.Color == Color.Green && z.Amount > 13 ) ||
                     ( z.Color == Color.Blue && z.Amount > 14 )  ) ) )
            .Select( x => x.Id );

        IEnumerable<int> validGames = games
            .Select( x => x.Id )
            .Except( notValidGames );

        WriteLine( $"Result: { validGames.Sum() }" );
    }
}
