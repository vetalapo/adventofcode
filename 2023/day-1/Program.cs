using System;
using System.IO;

namespace AdventOfCode;

public class Solution 
{
    static void Main( string[] args )
    {
        if ( args.Count() != 1 )
        {
            Console.WriteLine( "Invalid input" );

            return;
        }

        string inputFile = args[0];

        if ( !File.Exists(inputFile) )
        {
            Console.WriteLine( $"File [{inputFile}] does not exist" );

            return;
        }


        using StreamReader sr = File.OpenText( inputFile );

        string line = string.Empty;

        int sum = 0;

        while ( ( line = sr.ReadLine() ) is not null )
        {
            int currentCalibrationValue = CalibrationValue( line );

            sum += currentCalibrationValue;
        }

        Console.WriteLine( $"\r\n\r\nDone!\r\nResult: {sum}" );
    }

    private static int CalibrationValue ( string input )
    {
        int left = 0;
        int right = input.Length - 1;

        while( left <= right )
        {
            bool isLeftDigit = Char.IsDigit( input[left] );
            bool isRightDigit = Char.IsDigit( input[right] );
            
            if ( isLeftDigit && isRightDigit ) {
                break;
            }

            if ( !isLeftDigit ) {
                left++;
            }

            if ( !isRightDigit ) {
                right--;
            }
        }

        int.TryParse( $"{input[left]}{input[right]}", out int result );

        return result;
    }
}
