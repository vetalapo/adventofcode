using static System.Console;

namespace AdventOfCode;

public class Program
{
    public static void Main( string[] args )
    {
        string inputFilePath = "input.txt";

        if ( args.Length > 0 && !String.IsNullOrWhiteSpace( args[0] ) )
        {
            inputFilePath = args[0];
        }

        IEnumerable<Race> races = Race.Parse( inputFilePath );

        // Part I
        IEnumerable<int> amountPossibleWins = Race.CalcAmountPossibleWins( races );

        WriteLine( $"The number of ways to beat the record in each race: {amountPossibleWins.Aggregate( 1, (acc, next) => acc *= next )}" );
    }
}
