namespace Doodle_Maps.MapGeneration;

public struct Point
{
    public int Column { get; }
    public int Row { get; }

    public Point(int column, int row)
    {
        Column = column;
        Row = row;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Point other)
            return Column == other.Column && Row == other.Row;
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Column, Row);
    }
}