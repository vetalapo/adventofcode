namespace AdventOfCode;

public class Sequencer( string inputFilePath )
{
    private readonly IEnumerable<IEnumerable<long>> _sequences = Parse( inputFilePath );

    public IEnumerable<long> ProcessSequencesForward()
    {
        foreach( IEnumerable<long> seq in this._sequences )
        {
            yield return seq.Last() + FutureSequence( seq.ToArray() )[^1];
        }
    }

    public IEnumerable<long> ProcessSequencesBackwards()
    {
        foreach( IEnumerable<long> seq in this._sequences )
        {
            yield return seq.First() - DownSequence( seq.ToArray() )[0];
        }
    }

    public static long[] FutureSequence( long[] seq )
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

        long[] recursiveRes = FutureSequence( result );

        return [..result, result[^1] + recursiveRes[^1] ];
    }

    public static long[] DownSequence( long[] seq )
    {
        if ( seq.All( n => n == 0 ) )
        {
            return seq;
        }

        long[] mainSeq = new long[ seq.Length - 1 ];

        for ( int i = 0, j = 0; i < seq.Length - 1; i++, j++ )
        {
            mainSeq[j] = seq[i + 1] - seq[i];
        }

        long[] recursiveRes = DownSequence( mainSeq );
        
        // Compiling result
        long[] finSeq = new long[mainSeq.Length + 1];
        finSeq[0] = mainSeq[0] - recursiveRes[0];

        for ( int i = 1; i < finSeq.Length; i++ )
        {
            finSeq[i] = mainSeq[i - 1];
        }

        return finSeq;
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
