namespace AdventOfCode;

public class MapsParser
{
    private readonly IEnumerable<long> _seeds = [];

    private readonly IEnumerable<long> _fullyRangedSeeds = [];
    
    private readonly Dictionary<string, List<(long source, long destination, long length)>> _maps = new()
    {
        { "seed-to-soil", new List<(long min, long max, long length)>() },
        { "soil-to-fertilizer", new List<(long min, long max, long length)>() },
        { "fertilizer-to-water", new List<(long min, long max, long length)>() },
        { "water-to-light", new List<(long min, long max, long length)>() },
        { "light-to-temperature", new List<(long min, long max, long length)>() },
        { "temperature-to-humidity", new List<(long min, long max, long length)>() },
        { "humidity-to-location", new List<(long min, long max, long length)>() }
    };

    public MapsParser( string filePath )
    {
        if ( !File.Exists( filePath ) )
        {
            throw new FileNotFoundException( $"File not fount at [{filePath}]" );
        }

        string currentMap = string.Empty;

        // Read file, populate maps
        foreach( string line in File.ReadLines( filePath ) )
        {
            if ( String.IsNullOrWhiteSpace( line ) )
            {
                continue;
            }

            if ( line.StartsWith( "seeds" ) )
            {
                string[] seedsParts = line.Split( ':' );

                this._seeds = ParseNumbersLine( seedsParts[1] );

                continue;
            }

            if ( !Char.IsDigit( line[0] ) )
            {
                currentMap = GetMapKey( line );

                continue;
            }

            // Digits processing
            long[] lineDigits = ParseNumbersLine( line ).ToArray();

            PopulateMap( currentMap, lineDigits, ref this._maps );
        }

        // this._fullyRangedSeeds = CalculateSeedRanges( _seeds.ToArray() );
    }

    public Dictionary<string, List<(long source, long destination, long length)>> Maps
    {
        get
        {
            return this._maps;
        }
    }

    public IEnumerable<long> Seeds
    {
        get
        {
            return this._seeds;
        }
    }

    public IEnumerable<long> FullyRangedSeeds
    {
        get
        {
            return this._fullyRangedSeeds;
        }
    }

    public IEnumerable<long> CalculateLocations( IEnumerable<long> seeds )
    {
        foreach ( long seed in seeds )
        {
            yield return Location( seed );
        }
    }

    private long Location( long seed )
    {
        long cursor = seed;

        foreach( KeyValuePair<string, List<(long source, long destination, long length)>> map in this._maps )
        {
            foreach ( (long source, long destination, long length) in map.Value )
            {
                long min = source;
                long max = source + length;

                if ( cursor >= min && cursor <= max )
                {
                    cursor = destination + (cursor - min);
                    break;
                }
            }
        }

        return cursor;
    }

    private static long[] CalculateSeedRanges( long[] list )
    {
        List<long> result = [];

        for ( int i = 0; i < list.Length; i += 2 )
        {
            long numStart = list[i];
            long numEnd = numStart + list[i + 1] - 1;

            do
            {
                result.Add( numStart );
                numStart++;
            }
            while ( numStart <= numEnd );
        }

        return [.. result];
    }

    public long CalculateMinLocationLowMemory()
    {
        long result = long.MaxValue;

        long[] seeds = this._seeds.ToArray();

        for ( int i = 0; i < seeds.Length; i += 2 )
        {
            long numStart = seeds[i];
            long numEnd = numStart + seeds[i + 1] - 1;

            for ( long cursor = numStart; cursor <= numEnd; cursor++ )
            {
                result = Math.Min( result, Location( cursor ) );
            }
        }

        return result;
    }

    private static IEnumerable<long> ParseNumbersLine( string input )
    {
        string[] strNums = input.Trim().Split( ' ', StringSplitOptions.RemoveEmptyEntries );

        foreach ( string strNum in strNums )
        {
            long.TryParse( strNum, out long num );

            yield return num;
        }
    }

    private static string GetMapKey( string input )
    {
        // Format: [seed-to-soil map:]
        string[] parts = input.Trim().Split( ' ', StringSplitOptions.RemoveEmptyEntries );

        return parts[0].Trim();
    }

    private static void PopulateMap( string key, long[] digits,  ref Dictionary<string, List<(long source, long destination, long length)>> maps )
    {
        long destinationStart = digits[0];
        long sourceStart = digits[1];
        long length = digits[2] - 1;
        
        maps[key].Add( new (sourceStart, destinationStart, length) );
    }
}
