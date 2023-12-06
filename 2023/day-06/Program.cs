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

        // Part I
        IEnumerable<Race> races = Race.Parse( inputFilePath );
        IEnumerable<long> amountPossibleWins = Race.CalcAmountPossibleWins( races );

        WriteLine( $"The number of ways to beat the record in  each  race: {amountPossibleWins.Aggregate( 1, (acc, next) => acc *= (int)next )}" );

        // Part II
        Race singleRace = Race.ParseAsSingle( inputFilePath );
        long singlePossibleWins = Race.CalcAmountPossibleWins( new List<Race>() { singleRace } ).FirstOrDefault();

        WriteLine( $"The number of ways to beat the record in single race: {singlePossibleWins}" );
    }
}
