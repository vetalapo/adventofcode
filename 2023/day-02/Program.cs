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

        IEnumerable<Game> games = Game.GetGamesFromFile( args[0] );

        IEnumerable<int> notValidGames = games
            .Where( x => x.CubeSet.Any( y => y
            .Any( z => ( z.Color == Color.Red && z.Amount > 12 ) ||
                     ( z.Color == Color.Green && z.Amount > 13 ) ||
                     ( z.Color == Color.Blue && z.Amount > 14 )  ) ) )
            .Select( x => x.Id );

        IEnumerable<int> validGames = games
            .Select( x => x.Id )
            .Except( notValidGames );

        WriteLine( $"The sum of the IDs of valid games: { validGames.Sum() }" );
    }
}
