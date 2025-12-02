using static System.Console;

namespace AdventOfCode;

public class Program
{
    public static void Main( string[] args )
    {
        string inputFilePath = "input.txt";

        if ( args.Length > 0 )
        {
            inputFilePath = args[0];
        }

        Network network = new( inputFilePath );

        //
        // Part I
        //
        long steps = network.Steps( "AAA", "ZZZ" );

        WriteLine( $"Amount of steps to reach ZZZ: {steps}" );
        
        //
        // Part II
        // Brute force will take forever, input is done in such a way
        // so doing Least common multiple (LCM) will make it a lot faster
        // but first calc distances from each starting point to their Z value
        //
        
        // Getting start nodes
        IEnumerable<string> startNodes = network.StartNodes( 'A' );

        List<Network> networks = [];

        // Create nets of maps, set initial pointers, remove key limiter to run indefinitely ( if needed )
        foreach ( string node in startNodes )
        {
            Network net = new( inputFilePath );
            net.SetStartPoint( node );
            net.RemoveKeyLimiter();

            networks.Add( net );
        }

        // Iterating until the end window for each net is found
        // Network class is build to count its own iterations
        while ( !networks.All( x => x.Current.Key.EndsWith( 'Z' ) ) )
        {           
            foreach( Network net in networks )
            {
                if ( !net.Current.Key.EndsWith( 'Z' ) )
                {
                    net.MoveNext();
                }
            }
        }

        // Distances from each starting point to their Z value,
        // Doing LCM on all of them will get the result
        long[] distances = networks.Select( x => x.Count ).ToArray();
        long amountOfStepsLCM = distances.Aggregate( 1L, LCM );

        WriteLine( $"Amount of steps before all nodes end with Z: {amountOfStepsLCM}" );
    }

    /// <summary>
    /// Least common multiple
    /// https://en.wikipedia.org/wiki/Least_common_multiple
    /// </summary>
    private static long LCM( long a, long b )
    {
        return a * b / GCD( a, b );
    }

    /// <summary>
    /// Greatest common divisor
    /// https://en.wikipedia.org/wiki/Greatest_common_divisor
    /// https://en.wikipedia.org/wiki/Euclidean_algorithm
    /// </summary>
    private static long GCD( long a, long b )
    {
        while ( a != b )
        {
            if ( a > b )
            {
                a -= b;
            }
            else
            {
                b -= a;
            }
        }

        return a;
    }
}
