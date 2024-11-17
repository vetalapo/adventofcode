namespace AdventOfCode;

public class Walker
{
    public readonly char[][] Field;

    public readonly Coords StartingPosition;

    public Walker( string inputFilePath )
    {
        (char[][] Field, Coords StartingPosition) parseResult = Parse( inputFilePath );
        
        this.Field = parseResult.Field;
        this.StartingPosition = parseResult.StartingPosition;
    }

    public int CalcFarthestDistanceFromStart()
    {
        Coords left = this.StartingPosition;
        Coords right = this.StartingPosition;

        do
        {
        }
        while ( left.X == right.X && left.Y == right.Y );

        return Math.Max( left.Steps, right.Steps );
    }

    private static (char[][] Field, Coords StartingPosition ) Parse( string inputFilePath )
    {
        if ( !File.Exists( inputFilePath ) )
        {
            throw new FileNotFoundException( $"File not found at: {inputFilePath}" );
        }

        List<char[]> fieldResult = [];
        Coords coords = new( 0, 0 );
        
        string[] lines = File.ReadAllLines( inputFilePath );

        for ( int i = 0; i < lines.Length; i++ )
        {
            string line = lines[i];

            char[] chars = new char[line.Length];

            for ( int j = 0; j < line.Length; j++ )
            {
                chars[j] = line[j];

                if ( line[j] == 'S' )
                {
                    coords = new Coords( i, j );
                }
            }

            fieldResult.Add( chars );
        }

        return ([.. fieldResult], coords );
    }
}
