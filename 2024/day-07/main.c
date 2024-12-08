#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>

typedef struct {
    unsigned long long doubleCalibration;
    unsigned long long tripleCalibration;
} calibration;

calibration getCalibrationResult( char line[], int lineSize );
bool canBeCombined( int nums[], int size, unsigned long long checkNum, unsigned long long currComb, int currIndex );
bool canBeCombinedAndConcatenated( int nums[], int size, unsigned long long checkNum, unsigned long long currComb, int currIndex );

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

    unsigned long long int calibrationResult = 0;
    unsigned long long int totalCalibrationResult = 0;

    char line[50];

    while ( fgets( line, 50, fp ) )
    {
        int lineSize = strnlen( line, 50 );

        calibration calibrationResults = getCalibrationResult( line, lineSize );

        // Part I
        calibrationResult += calibrationResults.doubleCalibration;

        // Part II
        totalCalibrationResult += calibrationResults.tripleCalibration;
    }

    // Results
    printf( "Calibration Result: %llu\n", calibrationResult );
    printf( "Total Calibration Result: %llu\n", totalCalibrationResult );

    return 0;
}

calibration getCalibrationResult( char line[], int lineSize )
{
    int index = 0;

    // Test Number Parsing
    char testStrBuffer[15];
    char *endPtr;

    while ( index < lineSize && line[index] != ':' )
    {
        testStrBuffer[index] = line[index];

        index++;
    }

    testStrBuffer[index] = '\0';
    index += 2;

    unsigned long long testNum = strtoull( testStrBuffer, &endPtr, 10 );

    // Operators Parsing
    char numbersStr[15][5];
    int numbersIndex = 0;

    for ( ; index < lineSize; index++, numbersIndex++ )
    {
        char currNum[5];
        int currIndex = 0;

        while ( index < lineSize && line[index] != ' ' )
        {
            currNum[currIndex] = line[index];

            index++;
            currIndex++;
        }

        currNum[currIndex] = '\0';
        
        strcpy_s( numbersStr[numbersIndex], 5, currNum );
    }

    int *numbersPtr = malloc( numbersIndex * sizeof( int ) );

    for ( int i = 0; i < numbersIndex; i++ )
    {
        numbersPtr[i] = atoi( numbersStr[i] );
    }

    calibration result;

    result.doubleCalibration = canBeCombined( numbersPtr, numbersIndex, testNum, numbersPtr[0], 1 ) ? testNum : 0;
    result.tripleCalibration = canBeCombinedAndConcatenated( numbersPtr, numbersIndex, testNum, numbersPtr[0], 1 ) ? testNum : 0;

    free( numbersPtr );

    return result;
}

bool canBeCombined( int nums[], int size, unsigned long long checkNum, unsigned long long currComb, int currIndex )
{
    if ( currIndex > size || currComb > checkNum )
    {
        return false;
    }

    if ( currIndex == size && currComb == checkNum )
    {
        return true;
    }

    return canBeCombined( nums, size, checkNum, currComb + nums[currIndex], currIndex + 1 ) |
           canBeCombined( nums, size, checkNum, currComb * nums[currIndex], currIndex + 1 );
}

bool canBeCombinedAndConcatenated( int nums[], int size, unsigned long long checkNum, unsigned long long currComb, int currIndex )
{
    if ( currIndex > size || currComb > checkNum )
    {
        return false;
    }

    if ( currIndex == size && currComb == checkNum )
    {
        return true;
    }

    // Merge 2 numbers together
    char mergerStr[50];
    char *endPtr;

    snprintf( mergerStr, 50, "%llu%d", currComb, nums[currIndex] );

    unsigned long long merger = strtoull( mergerStr, &endPtr, 10 );

    return canBeCombinedAndConcatenated( nums, size, checkNum, currComb + nums[currIndex], currIndex + 1 ) |
           canBeCombinedAndConcatenated( nums, size, checkNum, currComb * nums[currIndex], currIndex + 1 ) |
           canBeCombinedAndConcatenated( nums, size, checkNum, merger, currIndex + 1 );
}
