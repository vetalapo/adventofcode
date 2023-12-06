namespace AdventOfCode;

public class MapsParser
{
    private readonly IEnumerable<long> _seeds = [];
    
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

    private readonly IEnumerable<long> _locations = [];

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

        this._locations = CalculateLocations( this._seeds, this._maps );
    }

    public IEnumerable<long> Locations
    {
        get
        {
            return this._locations;
        }
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

    private static IEnumerable<long> CalculateLocations( IEnumerable<long> seeds, Dictionary<string, List<(long source, long destination, long length)>> maps )
    {
        foreach ( long seed in seeds )
        {
            yield return Location( seed, ref maps );
        }
    }

    private static long Location( long seed, ref Dictionary<string, List<(long source, long destination, long length)>> maps )
    {
        long cursor = seed;

        foreach( KeyValuePair<string, List<(long source, long destination, long length)>> map in maps )
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
