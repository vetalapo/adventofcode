namespace AdventOfCode;

public class Race
{
    public long Time { get; set; }

    public long RecordDistance { get; set; }

    public Race()
    {
    }

    public Race( long time, long recordDistance = 0 )
    {
        this.Time = time;
        this.RecordDistance = recordDistance;
    }

    public static IEnumerable<long> CalcAmountPossibleWins( IEnumerable<Race> races )
    {
        foreach( Race race in races )
        {
            long amountPossibleWins = 0;

            for ( long speed = 1; speed < race.Time; speed++ )
            {
                long time = race.Time - speed;
                long distance = time * speed;

                if ( distance > race.RecordDistance )
                {
                    amountPossibleWins++;
                }
            }

            yield return amountPossibleWins;
        }
    } 

    public static IEnumerable<Race> Parse( string filePath )
    {
        if ( !File.Exists( filePath ) )
        {
            throw new FileNotFoundException( $"Input file is not found at [{filePath}]" );
        }

        List<Race> races = [];

        foreach( string line in File.ReadLines( filePath ) )
        {
            if ( String.IsNullOrWhiteSpace( line ) )
            {
                continue;
            }

            string numPart = line.Trim().Split( ':' )[1].Trim();

            int[] nums = ParseNums( numPart ).ToArray();

            if ( line.StartsWith( "Time" ) )
            {
                foreach ( int num in nums )
                {
                    races.Add( new Race( num ) );
                }
            }

            if ( line.StartsWith( "Distance" ) )
            {
                for( int i = 0; i < nums.Length; i++ )
                {
                    races[i].RecordDistance = nums[i];
                }
            }
        }

        return races;
    }

    public static Race ParseAsSingle( string filePath )
    {
        if ( !File.Exists( filePath ) )
        {
            throw new FileNotFoundException( $"Input file is not found at [{filePath}]" );
        }

        Race race = new();

        foreach( string line in File.ReadLines( filePath ) )
        {
            if ( String.IsNullOrWhiteSpace( line ) )
            {
                continue;
            }

            string numPart = line.Trim().Split( ':' )[1].Trim().Replace( " ", string.Empty );

            long.TryParse( numPart, out long num );

            if ( line.StartsWith( "Time" ) )
            {
                race.Time = num;
            }

            if ( line.StartsWith( "Distance" ) )
            {
                race.RecordDistance = num;
            }
        }

        return race;
    }

    private static IEnumerable<int> ParseNums( string input )
    {
        string[] parts = input.Split( ' ', StringSplitOptions.RemoveEmptyEntries );

        foreach ( string part in parts )
        {
            int.TryParse( part.Trim(), out int num );

            yield return num;
        }
    }
}
