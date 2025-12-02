using static System.Console;

namespace AdventOfCode;

public class Walker
{
    public readonly PipeDirection[][] Field;

    public readonly Coordinate StartingPosition;

    public Walker( string inputFilePath )
    {
        (PipeDirection[][] Field, Coordinate StartingPosition) parseResult = Parse( inputFilePath );
        
        this.Field = parseResult.Field;
        this.StartingPosition = parseResult.StartingPosition;
    }

    public int CalcFarthestDistanceFromStart()
    {
        WriteLine( StartingPosition );

        return 0;
    }

    private static ( PipeDirection[][] Field, Coordinate StartingPosition ) Parse( string inputFilePath )
    {
        if ( !File.Exists( inputFilePath ) )
        {
            throw new FileNotFoundException( $"File not found at: {inputFilePath}" );
        }

        string[] lines = File.ReadAllLines( inputFilePath );

        PipeDirection[][] fieldResult = new PipeDirection[lines.Length][];
        Coordinate startingPosition = new( 0, 0 );
        
        for ( int i = 0; i < lines.Length; i++ )
        {
            fieldResult[i] = new PipeDirection[lines[i].Length];

            for ( int j = 0; j < lines[i].Length; j++ )
            {
                fieldResult[i][j] = Types.DirectionMap[lines[i][j]];

                if ( fieldResult[i][j] == PipeDirection.StartingPosition )
                {
                    startingPosition = new Coordinate( i, j );
                }
            }
        }

        return (fieldResult, startingPosition);
    }
}
