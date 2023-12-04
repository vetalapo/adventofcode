using System;
using static System.Console;

namespace AdventOfCode;

public class Program
{
    public static void Main( string[] args )
    {
        if ( args.Length == 0 )
        {
            throw new ArgumentNullException( "No arguments" );
        }

        string filePath = args[0];

        if ( !File.Exists( filePath ) )
        {
            throw new FileNotFoundException( $"No file at: {filePath}" );
        }

        List<Card> cards = Card.ParseFile( filePath ).ToList();

        // Part I
        int cardsPointsSum = cards.Sum( c => c.Points );

        WriteLine( $"Colorful cards worth in total: {cardsPointsSum} points" );
    }
}
