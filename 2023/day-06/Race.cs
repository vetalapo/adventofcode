namespace AdventOfCode;

public class Race
{
    public int Time { get; set; }

    public int RecordDistance { get; set; }

    public Race()
    {
    }

    public Race( int time, int recordDistance = 0 )
    {
        this.Time = time;
        this.RecordDistance = recordDistance;
    }

    public static IEnumerable<int> CalcAmountPossibleWins( IEnumerable<Race> races )
    {
        foreach( Race race in races )
        {
            int amountPossibleWins = 0;

            for ( int speed = 1; speed < race.Time; speed++ )
            {
                int time = race.Time - speed;
                int distance = time * speed;

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
