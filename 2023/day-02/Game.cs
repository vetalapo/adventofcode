namespace AdventOfCode;

public class Game
{
    public int Id { get; set; }

    public List<List<Cube>> CubeSet { get; set; } = [];

    public static Game ParseGame( string input )
    {
        var result = new Game();

        string[] gameParts = input.Split( ':' );

        // Id
        int.TryParse( gameParts[0].Split( ' ' )[1], out int gameId );
        result.Id = gameId;

        // Cube Sets
        string[] cubeSets = gameParts[1].Split( ';' );

        foreach ( string set in cubeSets )
        {
            List<Cube> cubes = [];

            foreach ( string cube in set.Split( ',' ) )
            {
                Cube parsedCube = new();

                string[] cubeParts = cube.Trim().Split( ' ' );

                // Amount of cubes
                int.TryParse( cubeParts[0], out int amount );
                parsedCube.Amount = amount;

                // Color
                parsedCube.Color = Cube.ConvertColor( cubeParts[1] );

                cubes.Add( parsedCube );
            }

            result.CubeSet.Add( cubes );
        }

        return result;
    }
}

public class Cube
{
    public Color Color { get; set; }

    public int Amount { get; set; } = 0;

    public static Color ConvertColor( string input ) => input switch
    {
        "red" => Color.Red,
        "green" => Color.Green,
        "blue" => Color.Blue,
        _ => Color.no_color_set
    };
}

public enum Color
{
    no_color_set,
    Red,
    Green,
    Blue
}
