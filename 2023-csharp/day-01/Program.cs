using System;
using System.IO;
using static System.Console;

namespace AdventOfCode;

public class Program 
{
    private static readonly Dictionary<string, int> numSet = new()
    {
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4 },
        { "five", 5 },
        { "six", 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine", 9 }
    };

    private static readonly HashSet<char> numHash = new( new char[] { 'o', 't', 'f', 's', 'e', 'n' } );
    private static readonly HashSet<char> numHashDesc = new( new char[] { 'e', 'o', 'r', 'x', 'n', 't' } );

    static void Main( string[] args )
    {
        if ( args.Length != 1 )
        {
            WriteLine( "Invalid input" );

            return;
        }

        string inputFile = args[0];

        if ( !File.Exists(inputFile) )
        {
            WriteLine( $"File [{inputFile}] does not exist" );

            return;
        }


        using StreamReader sr = File.OpenText( inputFile );

        string? line = string.Empty;

        int sum = 0;

        while ( ( line = sr.ReadLine() ) is not null )
        {
            sum += CalibrationValue( line );
        }

        WriteLine( $"Done!\r\nResult: {sum}" );
    }

    private static int CalibrationValue ( string input )
    {
        if ( string.IsNullOrEmpty(input) )
        {
            return 0;
        }

        int left = 0;
        int right = input.Length - 1;

        int leftNum = 0;
        int rightNum = 0;

        while( left <= right )
        {
            char leftChar = input[left];
            char rightChar = input[right];

            // Digit Check
            if ( leftNum > 0 && rightNum > 0 )
            {
                break;
            }

            if ( leftNum == 0 && Char.IsDigit( leftChar ) )
            {
                leftNum = (int)Char.GetNumericValue( leftChar );
            }

            if ( rightNum == 0 && Char.IsDigit( rightChar ) )
            {
                rightNum = (int)Char.GetNumericValue( rightChar );
            }

            // Text Portion Check
            if ( leftNum == 0 && numHash.Contains( leftChar ) )
            {
                leftNum = WordCheck( input, left );
            }

            if ( rightNum == 0 && numHashDesc.Contains( rightChar ) )
            {
                rightNum = WordCheckDesk( input, right );
            }

            // Updating pointers
            if ( leftNum == 0 )
            {
                left++;
            }

            if ( rightNum == 0 )
            {
                right--;
            }
        }

        return int.TryParse( $"{leftNum}{rightNum}", out int result )
                   ? result
                   : 0;
    }

    private static int WordCheck ( string input, int index )
    {
        for ( int i = index, j = 1; i < input.Length && j <= 5; i++, j++ )
        {
            string currentPortion = input.Substring( index, j );

            if ( numSet.TryGetValue( currentPortion, out int value ) )
            {
                return value;
            }
        }

        return 0;
    }

    private static int WordCheckDesk ( string input, int index )
    {
        for ( int right = index, j = 1; right >= 0 && j <= 5; right--, j++ )
        {
            string currentPortion = input.Substring( right, j );

            if ( numSet.TryGetValue( currentPortion, out int value ) )
            {
                return value;
            }
        }

        return 0;
    }
}
