﻿using static System.Console;

namespace AdventOfCode;

public class Program
{
    public static void Main( string[] args )
    {
        if ( args.Length == 0 )
        {
            throw new ArgumentException( "No arguments provided. Required input.txt" );
        }

        string filePath = args[0];

        MapsParser maps = new( filePath );

        // Part I
        WriteLine( $"Lowest location number: {maps.Locations.Min()}" );
    }
}
