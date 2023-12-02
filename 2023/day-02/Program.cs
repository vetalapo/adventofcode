using System;
using System.Text.RegularExpressions;
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

        // Part I
        IEnumerable<int> notValidGames = games.Where(
                game => game.CubeSet.Any(
                    cubeSet => cubeSet.Any(
                        cube => ( cube.Color == Color.Red && cube.Amount > 12 ) ||
                                ( cube.Color == Color.Green && cube.Amount > 13 ) ||
                                ( cube.Color == Color.Blue && cube.Amount > 14 )
                    )
                )
            )
            .Select( game => game.Id );

        IEnumerable<int> validGames = games
            .Select( game => game.Id )
            .Except( notValidGames );

        WriteLine( $"The sum of the IDs of valid games: { validGames.Sum() }" );

        // Part II
        int sumPowerOfSets = games.Sum( game => game.CubeSet
                    .SelectMany( cubeSet => cubeSet )
                    .GroupBy( cube => cube.Color )
                    .Select( group => new Cube { Color = group.Key, Amount = group.Max( b => b.Amount ) } )
                    .Aggregate( 1, ( total, next ) => total * next.Amount )
        );    

        WriteLine( $"The sum of the power of  the sets: {sumPowerOfSets}" );
    }
}
