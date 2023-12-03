using System;
using static System.Console;

namespace AdventOfCode;

public class Program
{
    public static async Task Main( string[] args )
    {
        if ( args.Length == 0 )
        {
            throw new ArgumentOutOfRangeException( "No aruments provided" );
        }

        string inputFile = args[0];

        if ( !File.Exists( inputFile ) )
        {
            throw new FileNotFoundException( $"Cannot locate [{inputFile}]" );
        }

        string[] allText = await File.ReadAllLinesAsync( inputFile );
        
        // Part I
        WriteLine( $"The sum of all of the part numbers in the engine schematic: {SumPartNumbers( allText )}" );
        
        // Part II
        WriteLine( $"The sum of all of the gear ratios in your engine schematic: {SumGearRatio( allText )}" );
    }

    private static int SumPartNumbers( string[] input )
    {
        int partNumberSum = 0;

        for( int i = 0; i < input.Length; i++ )
        {
            string line = input[i];

            for ( int j = 0; j < line.Length; j++)
            {
                char c = line[j];
                
                // Looking for a number in line
                if ( Char.IsDigit( c ) )
                {
                    int numIndex = j;
                    
                    // Getting Number
                    string numStr = string.Empty;
                    
                    do
                    {
                        numStr += line[j];
                        j++;
                    }
                    while ( j < line.Length && Char.IsDigit(line[j]) );

                    // Getting window and Checking if adjacent
                    int windowLength = numStr.Length + 2;

                    string window = GetWindow( input, i, numIndex, windowLength );

                    if ( IsAdjacentToSymbol( window ) )
                    {
                        int.TryParse( numStr, out int num );
                        partNumberSum += num;
                    }
                }
            }
        }

        return partNumberSum;
    }

    private static int SumGearRatio( string[] input )
    {
        int gearRatioSum = 0;

        for( int i = 0; i < input.Length; i++ )
        {
            string line = input[i];

            for ( int j = 0; j < line.Length; j++ )
            {
                char c = line[j];

                if ( c == '*' )
                {
                    string window = GetWindow( input, i, j, 3 );

                    if ( !IsTwoNumbersInWindow( window ) )
                    {
                        continue;
                    }

                    gearRatioSum += GetGearRatio( input, i, j );
                }
            }
        }

        return gearRatioSum;
    }

    private static int GetGearRatio( string[] input, int rowIndex, int colIndex )
    {
        int result = 1;

        for ( int row = rowIndex - 1; row <= rowIndex + 1; row++ )
        {
            string line = input[row];

            for ( int j = colIndex - 1; j <= colIndex + 1; j++ )
            {
                string lineNum = string.Empty;

                // Found part of a number
                if ( Char.IsDigit( line[j] ) && String.IsNullOrEmpty( lineNum ) )
                {
                    // Determining Number borders
                    int left = j;
                    int right = j;

                    while( true )
                    {
                        bool isLeftDigit = left - 1 >= 0 && Char.IsDigit( line[left - 1] );
                        bool isRightDigit = right + 1 < line.Length && Char.IsDigit( line[right + 1] );

                        if ( !isLeftDigit && !isRightDigit )
                        {
                            break;
                        }

                        if ( isLeftDigit )
                        {
                            left--;
                        }

                        if ( isRightDigit )
                        {
                            right++;
                        }
                    }

                    lineNum = line.Substring( left, right - left + 1 );

                    j = right + 1;
                }

                if ( !String.IsNullOrEmpty( lineNum ) )
                {
                    int.TryParse( lineNum, out int parsedNum );

                    result *= parsedNum;
                }
            }
        }

        return result;
    }

    private static bool IsTwoNumbersInWindow( string window )
    {
        int result = 0;

        for ( int i = 0; i < window.Length; i++ )
        {
            if ( Char.IsDigit( window[i] ) )
            {
                do
                {
                    i++;
                }
                while ( i < window.Length && Char.IsDigit( window[i] ) );

                result++;
            }
        }

        return result == 2;
    }

    private static bool IsAdjacentToSymbol( string str )
    {
        foreach( char c in str )
        {
            if ( !( Char.IsDigit(c) || c == '.' || c == '\n' ) )
            {
                return true;
            }
        }

        return false;
    }

    private static string GetWindow( string[] input, int lineIndex, int numIndex, int width )
    {
        // Amount of lines to go through
        int startLineIndex = lineIndex - 1 >= 0
            ? lineIndex - 1
            : lineIndex;

        int endLineIndex = lineIndex + 1 < input.Length
            ? lineIndex + 1
            : lineIndex;

        // Staring index in line
        int startNumIndex = numIndex - 1 >= 0
            ? numIndex - 1
            : numIndex;

        // Reduce window size if bounds by the number
        if ( numIndex - 1 < 0 )
        {
            width--;
        }

        string result = string.Empty;

        for ( int i = startLineIndex; i <= endLineIndex; i++ )
        {
            string line = input[i];

            for ( int j = startNumIndex, counter = 0; j < line.Length && counter < width; j++, counter++ )
            {
                result += line[j];
            }
            
            result += "\n";
        }

        return result.Trim();
    }
}
