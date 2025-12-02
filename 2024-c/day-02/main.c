#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <stdbool.h>

bool isSafe( int arr[], int size );
bool isDampenedSafe( int arr[], int size );
bool isAscSafe( int arr[], int size );
bool isDescSafe( int arr[], int size );

int main( int argc, char *argv[] )
{
    // Checking args
    char input[20] = "input.txt";

    if ( argc > 1 )
    {
        strcpy_s( input, 20, argv[1] );
    }

    // Read file
    FILE *fp;
    fopen_s(&fp, input, "r");

    int bufferLength = 50;
    
    char line[bufferLength];
    char *delimiter = " ";

    int amountOfSafeReports = 0;
    int amountOfDampenedSafeReports = 0;

    while( fgets( line, bufferLength, fp ) )
    {
        char *next = NULL;
        char *token = strtok_s( line, " ", &next );

        int elements[10];
        int counter = 0;

        while ( token )
        {
            elements[counter] = atoi( token );

            counter++;

            token = strtok_s( NULL, " ", &next );
        }

        // Solution calculation
        if ( isSafe( elements, counter ) )
        {
            amountOfSafeReports++;
            amountOfDampenedSafeReports++;
        }
        else
        {
            if ( isDampenedSafe( elements, counter ) )
            {
                amountOfDampenedSafeReports++;
            }
        }
    }

    printf( "Amount of safe reports: %d\n", amountOfSafeReports );
    printf( "Amount of dampened safe reports: %d\n", amountOfDampenedSafeReports );

    return 0;
}

bool isSafe( int arr[], int size )
{
    return isAscSafe( arr, size ) || isDescSafe( arr, size );
}

bool isDampenedSafe( int arr[], int size )
{
    for ( int i = 0; i < size; i++ )
    {
        int currArr[size - 1];

        for ( int j = 0, k = 0; j < size; j++ )
        {
            if ( i == j )
            {
                continue;
            }

            currArr[k] = arr[j];

            k++;
        }

        if ( isAscSafe( currArr, size - 1 ) || isDescSafe( currArr, size - 1 ) )
        {
            return true;
        }
    }

    return false;
}

bool isAscSafe( int arr[], int size )
{
    for ( int i = 1; i < size; i++ )
    {
        int diff = arr[i] - arr[i - 1];

        if ( arr[i - 1] >= arr[i] || diff < 1 || diff > 3 )
        {
            return false;
        }
    }

    return true;
}

bool isDescSafe( int arr[], int size )
{
    for ( int i = 1; i < size; i++ )
    {
        int diff = arr[i - 1] - arr[i];

        if ( arr[i - 1] <= arr[i] || diff < 1 || diff > 3 )
        {
            return false;
        }
    }

    return true;
}
