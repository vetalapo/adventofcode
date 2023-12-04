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

        // Part II
        for ( int i = 0; i < cards.Count; i++ )
        {
            void appendCopies()
            {
                for ( int j = cards[i].Number; j < cards[i].Number + cards[i].MatchingNumberCount && j < cards.Count; j++ )
                {
                    cards[j].AddUpCopy();
                }
            }

            appendCopies();

            for ( int counter = 0; counter < cards[i].Copies; counter++ )
            {
                appendCopies();
            }
        }

        int instancesTotal = cards.Sum( x => x.Copies ) + cards.Count;

        WriteLine( $"Scratchcard sets amount total: {instancesTotal}" );
    }
}
