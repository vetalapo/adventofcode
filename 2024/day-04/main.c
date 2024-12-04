#include <stdio.h>
#include <string.h>

char xmasPattern[] = "XMAS";
int xmasPatternLength = 4;

char masPattern[] = "MAS";
int masPatternLength = 3;

typedef struct
{
    int row;
    int col;
} position;


int forwardCount( char matrix[][200], int sideLength, position pos, char pattern[], int patternLength, int patternIndex );
int backwardCount( char matrix[][200], int sideLength, position pos, char pattern[], int patternLength, int patternIndex );
int upCount( char matrix[][200], int sideLength, position pos, char pattern[], int patternLength, int patternIndex );
int downCount( char matrix[][200], int sideLength, position pos, char pattern[], int patternLength, int patternIndex );
int upDiagLeftCount( char matrix[][200], int sideLength, position pos, char pattern[], int patternLength, int patternIndex );
int upDiagRightCount( char matrix[][200], int sideLength, position pos, char pattern[], int patternLength, int patternIndex );
int downDiagLeftCount( char matrix[][200], int sideLength, position pos, char pattern[], int patternLength, int patternIndex );
int downDiagRightCount( char matrix[][200], int sideLength, position pos, char pattern[], int patternLength, int patternIndex );

int main( int argc, char *argv[] )
{
    // Handle args
    char input[25] = "input.txt";

    if ( argc > 1 )
    {
        strcpy_s( input, 25, argv[1] );
    }

    // Read file
    FILE *fp;

    fopen_s( &fp, input, "r" );

    int bufferSize = 200;

    char matrix[bufferSize][bufferSize];
    char line[bufferSize];
    int sideSize = 0;

    int lineIndex = 0;

    while ( fgets( line, bufferSize, fp ) )
    {
        if ( lineIndex == 0 )
        {
            sideSize = strlen( line );
        }

        strcpy_s( matrix[lineIndex], sideSize + 1, line );

        matrix[lineIndex][sideSize + 1] = '\0';

        lineIndex++;
    }

    // Search matrix
    int xmasCount = 0;
    int xmasCountII = 0;

    for ( int row = 0; row < sideSize; row++ )
    {
        for ( int col = 0; col < sideSize; col++ )
        {
            position pos = { row, col };

            // Part I
            if ( matrix[row][col] == 'X' )
            {
                xmasCount += forwardCount( matrix, sideSize, pos, xmasPattern, xmasPatternLength, 0 );
                xmasCount += backwardCount( matrix, sideSize, pos, xmasPattern, xmasPatternLength, 0 );
                xmasCount += upCount( matrix, sideSize, pos, xmasPattern, xmasPatternLength, 0 );
                xmasCount += downCount( matrix, sideSize, pos, xmasPattern, xmasPatternLength, 0 );
                xmasCount += upDiagLeftCount( matrix, sideSize, pos, xmasPattern, xmasPatternLength, 0 );
                xmasCount += upDiagRightCount( matrix, sideSize, pos, xmasPattern, xmasPatternLength, 0 );
                xmasCount += downDiagLeftCount( matrix, sideSize, pos, xmasPattern, xmasPatternLength, 0 );
                xmasCount += downDiagRightCount( matrix, sideSize, pos, xmasPattern, xmasPatternLength, 0 );
            }

            // Part II
            if ( matrix[row][col] == 'A' )
            {
                int currMasCount = 0;

                position topLeft = { pos.row - 1, pos.col - 1 };
                position topRight = { pos.row - 1, pos.col + 1 };
                position bottomLeft = { pos.row + 1, pos.col - 1 };
                position bottomRight = { pos.row + 1, pos.col + 1 };

                currMasCount += upDiagLeftCount( matrix, sideSize, bottomRight, masPattern, masPatternLength, 0 );
                currMasCount += upDiagRightCount( matrix, sideSize, bottomLeft, masPattern, masPatternLength, 0 );
                currMasCount += downDiagLeftCount( matrix, sideSize, topRight, masPattern, masPatternLength, 0 );
                currMasCount += downDiagRightCount( matrix, sideSize, topLeft, masPattern, masPatternLength, 0 );

                // 2 Means coplete X
                if ( currMasCount == 2 )
                {
                    xmasCountII++;
                }
            }
        }
    }

    printf( "%s count: %d\n", xmasPattern, xmasCount );
    printf( "X-MAS count: %d\n", xmasCountII );

    return 0;
}

int forwardCount( char matrix[][200], int sideLength, position pos, char pattern[], int patternLength, int patternIndex )
{
    if ( patternIndex == patternLength )
    {
        return 1;
    }

    if ( pos.col >= sideLength || matrix[pos.row][pos.col] != pattern[patternIndex] )
    {
        return 0;
    }

    pos.col++;

    return forwardCount( matrix, sideLength, pos, pattern, patternLength, patternIndex + 1 );
}

int backwardCount( char matrix[][200], int sideLength, position pos, char pattern[], int patternLength, int patternIndex )
{
    if ( patternIndex == patternLength )
    {
        return 1;
    }

    if ( pos.col < 0 || matrix[pos.row][pos.col] != pattern[patternIndex] )
    {
        return 0;
    }

    pos.col--;

    return backwardCount( matrix, sideLength, pos, pattern, patternLength, patternIndex + 1 );
}


int upCount( char matrix[][200], int sideLength, position pos, char pattern[], int patternLength, int patternIndex )
{
    if ( patternIndex == patternLength )
    {
        return 1;
    }

    if ( pos.row < 0 || matrix[pos.row][pos.col] != pattern[patternIndex] )
    {
        return 0;
    }

    pos.row--;

    return upCount( matrix, sideLength, pos, pattern, patternLength, patternIndex + 1 );

}

int downCount( char matrix[][200], int sideLength, position pos, char pattern[], int patternLength, int patternIndex )
{
    if ( patternIndex == patternLength )
    {
        return 1;
    }

    if ( pos.row >= sideLength || matrix[pos.row][pos.col] != pattern[patternIndex] )
    {
        return 0;
    }

    pos.row++;

    return downCount( matrix, sideLength, pos, pattern, patternLength, patternIndex + 1 );

}

int upDiagLeftCount( char matrix[][200], int sideLength, position pos, char pattern[], int patternLength, int patternIndex )
{
    if ( patternIndex == patternLength )
    {
        return 1;
    }

    if ( pos.row < 0 || pos.col < 0 || matrix[pos.row][pos.col] != pattern[patternIndex] )
    {
        return 0;
    }

    pos.row--;
    pos.col--;

    return upDiagLeftCount( matrix, sideLength, pos, pattern, patternLength, patternIndex + 1 );
}

int upDiagRightCount( char matrix[][200], int sideLength, position pos, char pattern[], int patternLength, int patternIndex )
{
    if ( patternIndex == patternLength )
    {
        return 1;
    }

    if ( pos.row < 0 || pos.col >= sideLength || matrix[pos.row][pos.col] != pattern[patternIndex] )
    {
        return 0;
    }

    pos.row--;
    pos.col++;

    return upDiagRightCount( matrix, sideLength, pos, pattern, patternLength, patternIndex + 1 );
}

int downDiagLeftCount( char matrix[][200], int sideLength, position pos, char pattern[], int patternLength, int patternIndex )
{
    if ( patternIndex == patternLength )
    {
        return 1;
    }

    if ( pos.row >= sideLength || pos.col < 0 || matrix[pos.row][pos.col] != pattern[patternIndex] )
    {
        return 0;
    }

    pos.row++;
    pos.col--;

    return downDiagLeftCount( matrix, sideLength, pos, pattern, patternLength, patternIndex + 1 );

}

int downDiagRightCount( char matrix[][200], int sideLength, position pos, char pattern[], int patternLength, int patternIndex )
{
    if ( patternIndex == patternLength )
    {
        return 1;
    }

    if ( pos.row >= sideLength || pos.col >= sideLength || matrix[pos.row][pos.col] != pattern[patternIndex] )
    {
        return 0;
    }

    pos.row++;
    pos.col++;

    return downDiagRightCount( matrix, sideLength, pos, pattern, patternLength, patternIndex + 1 );
}
