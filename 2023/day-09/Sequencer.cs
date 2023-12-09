namespace AdventOfCode;

public class Sequencer
{
    public static IEnumerable<long> ProcessSequences ( string inputFilePath )
    {
        IEnumerable<IEnumerable<long>> sequences = Sequencer.Parse( inputFilePath );

        foreach( IEnumerable<long> seq in sequences )
        {
            yield return seq.Last() + DownSequence( seq.ToArray() )[^1];
        }
    }

    public static long[] DownSequence( long[] seq )
    {
        if ( seq.All( n => n == 0 ) )
        {
            return seq;
        }

        long[] result = new long[ seq.Length - 1 ];

        for ( int i = 0, j = 0; i < seq.Length - 1; i++, j++ )
        {
            result[j] = seq[i + 1] - seq[i];
        }

        long[] recursiveRes = DownSequence( result );

        return [..result, result[^1] + recursiveRes[^1] ];
    }

    public static IEnumerable<IEnumerable<long>> Parse( string inputFilePath )
    {
        if ( !File.Exists( inputFilePath ) )
        {
            throw new FileNotFoundException( $"File not fount at: {inputFilePath}" );
        }

        foreach ( string line in File.ReadLines( inputFilePath ) )
        {
            yield return ParseNumList( line );
        }
    }

    private static IEnumerable<long> ParseNumList ( string numLine )
    {
        string[] numbers = numLine.Split( ' ', StringSplitOptions.RemoveEmptyEntries );

        foreach ( string num in numbers )
        {
            long.TryParse( num.Trim(), out long parsedNum );

            yield return parsedNum;
        }
    }
}
