#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <stdbool.h>

int main( int argc, char *argv[] )
{
    // Handle args
    char input[25] = "input.txt";

    if ( argc > 1 )
    {
        strcpy_s( input, 25, argv[1] );
    }

    // Read File
    FILE *fp;

    fopen_s( &fp, input, "r" );

    int bufferSize = 3500;
    char line[bufferSize];

    int lineCounter = 0;

    char pattern[] = "mul(#,#)";
    char onMul[] = "do()";
    char offMul[] = "don't()";

    bool enabledMul = true;

    int mulInstructionsSum = 0;
    int mulEnabledSum = 0;

    // Get line
    while( fgets( line, bufferSize, fp ) )
    {
        lineCounter++;

        int lineLength = strlen( line );
        
        // Iterate line
        for ( int i = 0, j = 0, op = 0; i < lineLength; i++ )
        {
            // On/Off Mul
            if ( line[i] == onMul[0] )
            {
                int d = 1;

                // Check for do
                while ( i + d < lineLength && d < 4 )
                {
                    if ( line[i + d] != onMul[d] )
                    {
                        break;
                    }

                    d++;
                }

                if ( d == 4 )
                {
                    enabledMul = true;

                    i += 3;
                }
                else
                {
                    // Check for dont's
                    d = 1;

                    while ( i + d < lineLength && d < 7 )
                    {
                        if ( line[i + d] != offMul[d] )
                        {
                            break;
                        }

                        d++;
                    }

                    if ( d == 7 )
                    {
                        enabledMul = false;

                        i += 6;
                    }
                }
            }

            // mul pattrern check
            int currOperands[2];

            while ( i < lineLength && j < 8 && ( pattern[j] == '#' || line[i] == pattern[j] ) )
            {
                if ( pattern[j] == '#' )
                {
                    int sIndex = 0;

                    char numStr[5];

                    while ( i < lineLength && line[i] >= '0' && line[i] <= '9' )
                    {
                        numStr[sIndex] = line[i];

                        i++;
                        sIndex++;
                    }

                    numStr[sIndex] = '\0';

                    currOperands[op] = atoi( numStr );

                    op++;
                }
                else
                {
                    i++;
                }

                j++;
            }

            // Counting results
            if ( j == 8 && currOperands[0] != 0 && currOperands[1] != 0 )
            {
                int currMul = currOperands[0] * currOperands[1];

                mulInstructionsSum += currMul;

                if ( enabledMul )
                {
                    mulEnabledSum += currMul;
                }

                i--;
            }

            // Reset vars
            j = 0;
            op = 0;

            currOperands[0] = 0;
            currOperands[1] = 0;
        }
    }

    // Print Results
    printf( "Sum of the results of the multiplications: %d\n", mulInstructionsSum );
    printf( "Sum of the enabled multiplications: %d\n", mulEnabledSum );

    return 0;
}
