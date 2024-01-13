﻿using System.Drawing;

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
        private readonly Color?[,] blocks;

        public Board()
        {
            grid = new Block[Width, Height];
            blocks = new Color?[Width, Height];
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
                    grid[block.X, block.Y] = block; // Add this line to update grid for collision detection
                    blocks[block.X, block.Y] = shape.ShapeColor;
                }
            }
        }


        public Color? GetBlockColor(int x, int y)
        {
            return blocks[x, y];
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
                grid[x, y] = null; // This will clear the blocks for collision detection
                blocks[x, y] = null; // Also clear the colors
            }
        }


        private void ShiftLinesDown(int startingY)
        {
            for (int y = startingY; y > 0; y--)
            {
                for (int x = 0; x < Width; x++)
                {
                    grid[x, y] = grid[x, y - 1]; // Shift the block objects down
                    blocks[x, y] = blocks[x, y - 1]; // Shift the colors down
                }
            }
            // Don't forget to clear the top line after shifting down
            for (int x = 0; x < Width; x++)
            {
                grid[x, 0] = null;
                blocks[x, 0] = null;
            }
        }



        // Additional utilities

        public bool IsPositionOccupied(int x, int y)
        {
            // Check if the coordinates are within the board's bounds
            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                // Return true if the position is not null (i.e., there is a block there)
                return grid[x, y] != null;
            }
            // If the position is out of bounds, treat it as occupied to prevent movement outside the board
            return true;
        }

    }
}
