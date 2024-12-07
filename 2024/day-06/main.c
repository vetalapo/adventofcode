#include <stdio.h>
#include <string.h>
#include <stdbool.h>

typedef struct
{
    int row;
    int col;
} position;

int countDistinctPositions( char map[150][150], int mapSize, position initGuardPosition, char initDirection );
int countLoopsWithObstacle( char initMap[150][150], int mapSize, position initGuardPosition, char initDirection );
bool isLoop( char map[150][150], int mapSize, position initGuardPosition, char initDirection );

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

    char map[150][150];

    int mapSize = 0;
    char direction;
    position guardPosition;

    int lineIndex = 0;

    char line[150];

    while ( fgets( line, 150, fp ) )
    {
        if ( lineIndex == 0 )
        {
            mapSize = strlen( line ) - 1;
        }

        strcpy_s( map[lineIndex], 150, line );

        // Search for guard starting position
        if ( !direction )
        {
            for ( int i = 0; i < mapSize; i++ )
            {
                if ( line[i] == '^' )
                {
                    direction = '^';

                    guardPosition.row = lineIndex;
                    guardPosition.col = i;
                }
            }
        }

        lineIndex++;
    }

    int amountOfDistinctPosition = countDistinctPositions( map, mapSize, guardPosition, direction );
    int amountOfLoops = countLoopsWithObstacle( map, mapSize, guardPosition, direction );
    
    // Print out results
    printf( "\n" );
    printf( "Amount of distinct positions: %d\n", amountOfDistinctPosition );
    printf( "Amount of loops: %d\n", amountOfLoops );

    return 0;
}

int countDistinctPositions( char map[150][150], int mapSize, position initGuardPosition, char initDirection )
{
    bool visitedMap[150][150];
    char direction = initDirection;
    position guardPosition = { initGuardPosition.row, initGuardPosition.col };

    // Navigate Map
    while ( guardPosition.row >= 0 && guardPosition.row < mapSize && 
            guardPosition.col >= 0 && guardPosition.col < mapSize )
    {
        visitedMap[guardPosition.row][guardPosition.col] = true;

        switch ( direction )
        {
            case '^':
                if ( guardPosition.row - 1 < 0 )
                {
                    break;
                }

                if ( map[guardPosition.row - 1][guardPosition.col] == '#' )
                {
                    direction = '>';
                }
                else
                {
                    guardPosition.row--;
                }

                break;
            case '>':
                if ( guardPosition.col + 1 > mapSize )
                {
                    break;
                }

                if ( map[guardPosition.row][guardPosition.col + 1] == '#' )
                {
                    direction = 'v';
                }
                else
                {
                    guardPosition.col++;
                }

                break;
            case 'v':
            {
                if ( guardPosition.row + 1 > mapSize )
                {
                    break;
                }

                if ( map[guardPosition.row + 1][guardPosition.col] == '#' )
                {
                    direction = '<';
                }
                else
                {
                    guardPosition.row++;
                }
                
                break;
            }
            case '<':
                if ( guardPosition.col - 1 < 0 )
                {
                    break;
                }

                if ( map[guardPosition.row][guardPosition.col - 1] == '#' )
                {
                    direction = '^';
                }
                else
                {
                    guardPosition.col--;
                }

                break;
        }
    }
    
    int amountDistinctPositions = 0;

    // Count results
    for ( int row = 0; row < mapSize; row++ )
    {
        for ( int col = 0; col < mapSize; col++ )
        {
            amountDistinctPositions += visitedMap[row][col];
        }
    }

    return amountDistinctPositions;
}

int countLoopsWithObstacle( char initMap[150][150], int mapSize, position initGuardPosition, char initDirection )
{
    int counter = 0;

    for ( int oRow = initGuardPosition.row; oRow < mapSize; oRow++ )
    {
        for ( int oCol = 0; oCol < mapSize; oCol++ )
        {
            if ( initMap[oRow][oCol] == '^' || initMap[oRow][oCol] == '#' )
            {
                continue;
            }

            char map[150][150];

            for ( int row = 0; row < mapSize; row++ )
            {
                for ( int col = 0; col < mapSize; col++ )
                {
                    map[row][col] = initMap[row][col];
                }
            }

            map[oRow][oCol] = '#';

            if ( isLoop( map, mapSize, initGuardPosition, initDirection ) )
            {
                counter++;
            }
        }
    }

    return counter;
}

bool isLoop( char map[150][150], int mapSize, position initGuardPosition, char initDirection )
{
    char direction = initDirection;
    position guardPosition = { initGuardPosition.row, initGuardPosition.col };

    // Navigate Map
    int loopCheck = 0;

    while ( guardPosition.row >= 0 && guardPosition.row < mapSize && 
            guardPosition.col >= 0 && guardPosition.col < mapSize )
    {
        loopCheck++;
        if ( loopCheck > 10000 )
        {
            return true;
        }

        switch ( direction )
        {
            case '^':
                if ( guardPosition.row - 1 < 0 )
                {
                    return false;
                }

                if ( map[guardPosition.row - 1][guardPosition.col] == '#' )
                {
                    direction = '>';
                }
                else
                {
                    guardPosition.row--;
                }

                break;
            case '>':
                if ( guardPosition.col + 1 > mapSize )
                {
                    return false;
                }

                if ( map[guardPosition.row][guardPosition.col + 1] == '#' )
                {
                    direction = 'v';
                }
                else
                {
                    guardPosition.col++;
                }

                break;
            case 'v':
            {
                if ( guardPosition.row + 1 > mapSize )
                {
                    return false;
                }

                if ( map[guardPosition.row + 1][guardPosition.col] == '#' )
                {
                    direction = '<';
                }
                else
                {
                    guardPosition.row++;
                }
                
                break;
            }
            case '<':
                if ( guardPosition.col - 1 < 0 )
                {
                    return false;
                }

                if ( map[guardPosition.row][guardPosition.col - 1] == '#' )
                {
                    direction = '^';
                }
                else
                {
                    guardPosition.col--;
                }

                break;
        }
    }

    return false;
}
