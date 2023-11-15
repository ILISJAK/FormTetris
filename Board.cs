namespace FormTetris
{
    public class Board
    {
        private const int Width = 10;
        private const int Height = 20;

        // Public properties to access Width and Height
        public int BoardWidth => Width;
        public int BoardHeight => Height;

        private Block[,] grid;

        public Board()
        {
            grid = new Block[Width, Height];
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    grid[x, y] = null;
                }
            }
        }

        public void PlaceShape(Shape shape)
        {
            foreach (var block in shape.Blocks)
            {
                if (block.X >= 0 && block.X < Width && block.Y >= 0 && block.Y < Height)
                {
                    grid[block.X, block.Y] = block;
                }
            }
        }


        // Method to check for line completion
        public void CheckLines()
        {
            for (int y = Height - 1; y >= 0; y--)
            {
                if (IsLineComplete(y))
                {
                    ClearLine(y);
                    ShiftLinesDown(y);
                    y++; // Recheck the same line since they have shifted down
                }
            }
        }

        // Helper methods
        private bool IsLineComplete(int y)
        {
            for (int x = 0; x < Width; x++)
            {
                if (grid[x, y] == null)
                    return false;
            }
            return true;
        }

        private void ClearLine(int y)
        {
            for (int x = 0; x < Width; x++)
            {
                grid[x, y] = null;
            }
        }

        private void ShiftLinesDown(int startingY)
        {
            for (int y = startingY; y > 0; y--)
            {
                for (int x = 0; x < Width; x++)
                {
                    grid[x, y] = grid[x, y - 1];
                }
            }
        }


        // Additional utilities

        public bool IsPositionOccupied(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height && grid[x, y] != null;
        }

    }
}
