namespace AdventOfCode;

public class Game
{
    public int Id { get; set; }

    public IEnumerable<IEnumerable<Cube>> CubeSet { get; set; } = [];

    public Game()
    {
    }

    public Game( int id, IEnumerable<IEnumerable<Cube>> cubeSet )
    {
        this.Id = id;
        this.CubeSet = cubeSet;
    }

    public static IEnumerable<Game> GetGamesFromFile( string filePath )
    {
        if ( !File.Exists( filePath ) )
        {
            throw new FileNotFoundException( $"File [{filePath}] does not exist" );
        }
        
        foreach ( string line in File.ReadLines( filePath ) )
        {
            yield return Game.Parse( line );
        }
    }

    public static Game Parse( string input )
    {
        string[] gameParts = input.Split( ':' );

        // Id
        int id = Game.ParseId( gameParts[0] );

        // Cube Sets
        IEnumerable<IEnumerable<Cube>> set = Cube.ParseSets( gameParts[1] );

        return new Game( id, set );
    }

    private static int ParseId( string input )
    {
        int.TryParse( input.Split( ' ' )[1], out int gameId );

        return gameId;
    }
}
