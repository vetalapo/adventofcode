#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <stdbool.h>

bool isCorrectlyOrdered( char orderings[3000][5], int orderingsSize, char rule[25][3], int ruleSize );
int getMidReordered( char orderings[3000][5], int orderingsSize, char rule[25][3], int ruleSize );

int main( int argc, char *argv[] )
{
    // Handl args
    char input[25] = "input.txt";

    if ( argc > 1 )
    {
        strcpy_s( input, 25, argv[1] );
    }

    // Read File
    FILE *fp;

    fopen_s( &fp, input, "r" );

    int bufferSize = 100;

    char line[bufferSize];

    int lineIndex = 0;
    int orderingsIndex = 0;

    bool isOrderingRules = true;

    char orderings[3000][5];

    int correctlyOrderedSum = 0;
    int reorderedSum = 0;

    while( fgets( line, bufferSize, fp ) )
    {
        int lineLength = strlen( line );

        if ( lineLength <= 1 )
        {
            isOrderingRules = false;
            
            continue;
        }

        if ( isOrderingRules )
        {
            snprintf( orderings[orderingsIndex], 5, "%c%c%c%c", line[0], line[1], line[3], line[4] );
            
            orderingsIndex++;

            snprintf( orderings[orderingsIndex], 5, "%c%c%c%c", line[3], line[4], line[0], line[1] );

            orderingsIndex++;
        }
        else
        {
            char rule[25][3];
            int ri = 0;

            for ( int i = 2; i < lineLength; i += 3 )
            {
                snprintf( rule[ri], 3, "%c%c", line[i - 2], line[i - 1] );
                ri++;
            }
            
            if ( isCorrectlyOrdered( orderings, orderingsIndex, rule, ri ) )
            {
                // Part I
                correctlyOrderedSum += atoi( rule[ri / 2] );
            }
            else
            {
                // Part II
                reorderedSum += getMidReordered( orderings, orderingsIndex, rule, ri );
            }
        }

        lineIndex++;
    }

    printf( "Correctly-ordered middle number sum: %d\n", correctlyOrderedSum );
    printf( "Correctly-reordered sum: %d\n", reorderedSum );
}

bool isCorrectlyOrdered( char orderings[3000][5], int orderingsSize, char rule[25][3], int ruleSize )
{
    for ( int i = 0; i < ruleSize - 1; i++ )
    {
        for ( int j = i + 1; j < ruleSize; j++ )
        {
            char currRevToken[5];

            snprintf( currRevToken, 5, "%s%s", rule[j], rule[i] );

            for ( int o = 0; o < orderingsSize; o++ )
            {
                if ( o % 2 != 0 )
                {
                    continue;
                }

                if ( strcmp( currRevToken, orderings[o] ) == 0 )
                {
                    return false;
                }
            }

        }
    }

    return true;
}

int getMidReordered( char orderings[3000][5], int orderingsSize, char rule[25][3], int ruleSize )
{
    for ( int i = 0; i < ruleSize - 1; i++ )
    {
        for ( int j = i + 1; j < ruleSize; j++ )
        {
            char currToken[5];
            char currRevToken[5];

            snprintf( currToken, 5, "%s%s", rule[i], rule[j] );
            snprintf( currRevToken, 5, "%s%s", rule[j], rule[i] );

            for ( int o = 0; o < orderingsSize; o++ )
            {
                if ( o % 2 != 0 )
                {
                    continue;
                }

                if ( strcmp( currRevToken, orderings[o] ) == 0 )
                {
                    char temp[3];
                    strcpy_s( temp, 3, rule[i] );
                    
                    strcpy_s( rule[i], 3, rule[j] );
                    strcpy_s( rule[j], 3, temp );
                }
            }

        }
    }

    return atoi( rule[ruleSize / 2] );
}
