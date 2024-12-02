#include <stdio.h>
#include <stdlib.h>
#include <math.h>

int ascComparer( const void* a, const void* b )
{
    return (*(int*)a - *(int*)b );
}

int main( int argc, char* argv[] )
{
    // Check arguments
    if ( argc < 2 )
    {
        printf( "No input file provided\n" );
        
        return 1;
    }

    // Open file
    errno_t fErr;
    FILE *fPtr;

    fErr = fopen_s( &fPtr, argv[1], "r" );

    if( fErr != 0 )
    {
        printf( "Not able to open the file.\n" );

        return 1;
    }

    // Read file
    int length = 15;
    char line[length];

    char which = 'f';
    char num1Str[10];
    char num2Str[10];

    int lineIndex = 0;

    int nums1[1000 * sizeof (int)];
    int nums2[1000 * sizeof (int)];

    while ( fgets( line, length, fPtr ) != NULL )
    {
        int i = 0;
        int fileI = 0;
        which = 'f';

        while( line[i] != '\n' )
        {
            if ( line[i] == ' ' )
            {
                fileI = 0;
                which = 's';
                num1Str[i] = '\0';
                i++;

                continue;
            }

            if ( which == 'f' )
            {
                num1Str[fileI] = line[i];
            }
            else
            {
                num2Str[fileI] = line[i];
            }

            i++;
            fileI++;
        }

        num2Str[fileI + 1] = '\0';

        nums1[lineIndex] = atoi( num1Str );
        nums2[lineIndex] = atoi( num2Str );

        lineIndex++;
    }

    // Process data
    qsort( nums1, 1000, sizeof(int), ascComparer );
    qsort( nums2, 1000, sizeof(int), ascComparer );
    
    int totalDistance = 0;
    int similarityScore = 0;

    for ( int i = 0; i < 1000; i++ )
    {
        totalDistance += abs( nums2[i] - nums1[i] );

        printf("%d %d\n", nums1[i], nums2[i]);

        // Part II similarity score
        int counter = 0;

        for ( int j = 999; j >= 0 && nums1[i] <= nums2[j]; j-- )
        {
            if ( nums1[i] == nums2[j] )
            {
                counter++;
            }
        }

        if ( counter > 0 )
        {
            similarityScore += nums1[i] * counter;
        }
    }

    printf( "Total distance between lists: %d\n", totalDistance );
    printf( "Similarity score: %d\n", similarityScore );

    // Closing stream
    if ( fPtr )
    {
        fErr = fclose( fPtr );

        if ( fErr != 0 )
        {
            printf( "The file \"%s\" was not closed\n", argv[1] );
        }
    }

    return 0;
}
