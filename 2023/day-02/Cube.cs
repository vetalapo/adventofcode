namespace AdventOfCode;

public class Cube
{
    public Color Color { get; set; }

    public int Amount { get; set; } = 0;

    public Cube()
    {
    }

    public Cube ( Color color, int amount )
    {
        this.Color = color;
        this.Amount = amount;
    }

    public static Cube Parse( string input )
    {
        string[] cubeParts = input.Trim().Split( ' ' );

        // Amount of cubes
        int.TryParse( cubeParts[0], out int amount );

        // Color
        Color color = ConvertColor( cubeParts[1] );

        return new Cube( color, amount );
    }

    public static IEnumerable<IEnumerable<Cube>> ParseSets ( string input )
    {
        string[] cubeSets = input.Split( ';' );

        foreach ( string set in cubeSets )
        {
            yield return ParseSingleSet( set );
        }
    }

    private static IEnumerable<Cube> ParseSingleSet( string input )
    {
        foreach ( string cube in input.Split( ',' ) )
        {
            yield return Parse( cube );
        }
    }

    public static Color ConvertColor( string input ) => input switch
    {
        "red" => Color.Red,
        "green" => Color.Green,
        "blue" => Color.Blue,
        _ => Color.no_color_set
    };
}
