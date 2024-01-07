namespace FormTetris
{
    public class Block
    {
        public Block(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }
        // Add additional properties like color if needed
    }
}
