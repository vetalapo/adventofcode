using System.Collections;

namespace AdventOfCode;

public partial class Network : IEnumerable, IEnumerator
{
    // Map
    private readonly Dictionary<string, Node> _map = [];

    private readonly Dictionary<string, (bool Left, bool Right)> _visitMap = [];
    
    private string _currentNodeIndex = string.Empty;

    // Instructions
    private InstructionType[] _instructionSet = [];

    private int _instructionPosition = -1;

    // Counter
    private long _counter = 0;

    // Limiters
    private string _keyLimiter = string.Empty;

    // Constructors
    public Network( string inputFilePath )
    {
        Parse( inputFilePath );

        this._currentNodeIndex = this.FirstKey;
    }

    // Public fields, and enumeration
    public string FirstKey => this._map.FirstOrDefault().Key;

    public string LastKey => this._map.LastOrDefault().Key;

    public long Count => this._counter;

    public int KeyCount => this._map.Count;

    public Map Current
    {
        get
        {
            if ( this._map.TryGetValue( this._currentNodeIndex, out Node? node ) )
            {
                return new Map( this._currentNodeIndex, node );
            }

            throw new IndexOutOfRangeException( $"No {nameof(Node)} at the location [{this._currentNodeIndex}]" );
        }
    }

    object IEnumerator.Current => Current;

    public (int Index, InstructionType Instruction) CurrentInstruction =>
    (
        Index: _instructionPosition,
        Instruction: _instructionPosition == -1 ? InstructionType._undefined : _instructionSet[_instructionPosition]
    );

    public IEnumerator GetEnumerator()
    {
        return (IEnumerator)this;
    }

    public bool MoveNext()
    {
        this._counter++;
        this._instructionPosition++;

        if ( this._instructionPosition >= this._instructionSet.Length )
        {
            this._instructionPosition = 0;
        }

        if( !this._map.TryGetValue( this._currentNodeIndex, out Node? node ) )
        {
            return false;
        }

        bool isLeft = CurrentInstruction.Instruction == InstructionType.Left;

        this._visitMap[this._currentNodeIndex] = (Left: isLeft, Right: !isLeft );

        this._currentNodeIndex = isLeft ? node.Left : node.Right;

        if ( this._visitMap[this._currentNodeIndex].Left && this._visitMap[this._currentNodeIndex].Right )
        {
            return false;
        }

        return this._currentNodeIndex != ( String.IsNullOrEmpty( this._keyLimiter ) ? this.LastKey : this._keyLimiter );
    }

    public void IterateAll( )
    {
        foreach( Map item in this ) { }
    }

    public void IterateUntillKey( string key )
    {
        this._keyLimiter = key;

        foreach( Map item in this ) { }
    }

    public void IterateFromToKey( string startNodeIndex, string endNodeIndex )
    {
        this._currentNodeIndex = startNodeIndex;
        this._keyLimiter = endNodeIndex;

        foreach( Map item in this ) { }
    }

    public void Reset()
    {
        _instructionPosition = -1;
        _currentNodeIndex = string.Empty;
    }

    // Class Methods

    public void AddNode( string key, Node node )
    {
        this._map.Add( key, node );
        this._visitMap.Add( key, (Left: false, Right: false) );
    }

    private void Parse( string inputFilePath )
    {
        if ( !File.Exists( inputFilePath ) )
        {
            throw new FileNotFoundException( $"File not fount at: {inputFilePath}" );
        }

        foreach ( string line in File.ReadLines( inputFilePath ) )
        {
            if ( String.IsNullOrWhiteSpace( line ) )
            {
                continue;
            }

            if ( line.Contains( '=' ) )
            {
                string[] record = line.Split( '=' );
                string[] nodes = record[1]
                    .Replace( "(", string.Empty )
                    .Replace( ")", string.Empty )
                    .Trim()
                    .Split( ',', StringSplitOptions.RemoveEmptyEntries );
                
                Node node = new( nodes[0].Trim(), nodes[1].Trim() );

                AddNode( record[0].Trim(), node );
            }
            else
            {
                LoadInstructions( line.Trim() );
            }
        }
    }

    private void LoadInstructions( string input )
    {
        List<InstructionType> chars = [];

        foreach ( char c in input )
        {
            switch( c )
            {
                case 'L':
                    chars.Add( InstructionType.Left );
                    break;
                case 'R':
                    chars.Add( InstructionType.Right );
                    break;
                default:
                    chars.Add( InstructionType._undefined );
                    break;
            }
        }

        this._instructionSet = [.. chars];
    }
}
