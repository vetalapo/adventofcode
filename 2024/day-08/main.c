#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>

typedef struct
{
    int row;
    int col;
} Point;

typedef struct
{
    Point a;
    Point b;
} Location;

bool isUniqueLocation( char field[50][55], int fieldSize, Location *locations, int *locationsSize, Point point );
bool hasAntinode( char field[50][55], int fieldSize, Point a, Point b );

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

    char line[55];

    // Input vars
    char field[50][55];
    bool fieldVisited[50][55];
    int fieldSize = 0;

    while ( fgets( line, 55, fp ) )
    {
        strcpy_s( field[fieldSize], 55, line );

        fieldSize++;
    }

    Location *locations = malloc( 10000 * sizeof( Location ) );
    int locationsSize = 5;

    int amountOfUniqueLocations = 0;

    for ( int row = 0; row < fieldSize; row++ )
    {
        for ( int col = 0; col < fieldSize; col++ )
        {
            if ( field[row][col] == '.' || field[row][col] == '#' )
            {
                continue;
            }

            Point point = { row, col };
            
            amountOfUniqueLocations += isUniqueLocation( field, fieldSize, locations, &locationsSize, point );

            return 0;
        }
    }

    printf( "\nAmount of unique locations: %d\n", amountOfUniqueLocations );

    free( locations );

    return 0;
}

bool isUniqueLocation( char field[50][55], int fieldSize, Location *locations, int *locationsSize, Point point )
{
    for ( int row = point.row; row < fieldSize; row++ )
    {
        for ( int col = ( row == point.row ? point.col + 1 : 0 ); col < fieldSize; col++ )
        {
            if ( field[row][col] == '.' || field[row][col] == '#' )
            {
                continue;
            }

            if ( field[row][col] == field[point.row][point.col] )
            {
                Point currPoint = { row, col };

                hasAntinode( field, fieldSize, point, currPoint );
            }
        }
    }

    return false;
}

bool hasAntinode( char field[50][55], int fieldSize, Point a, Point b )
{
    printf( "[%c][%d %d] - [%c][%d %d]\n", field[a.row][a.col], a.row, a.col, field[b.row][b.col], b.row, b.col );

    return false;
}
