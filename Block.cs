using System.Drawing;

public class Block
{
    public int X { get; set; }
    public int Y { get; set; }

    public Block(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void RotateClockwise(Point pivot)
    {
        int newX = pivot.Y + pivot.X - Y;
        int newY = pivot.X - pivot.Y + X;
        X = newX;
        Y = newY;
    }

    public void RotateCounterClockwise(Point pivot)
    {
        int newX = pivot.Y - pivot.X + Y;
        int newY = pivot.X + pivot.Y - X;
        X = newX;
        Y = newY;
    }
}
