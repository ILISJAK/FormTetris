using System;
using System.Collections.Generic;

namespace FormTetris
{
    public class Shape
    {
        private List<Block> blocks;
        public List<Block> Blocks => blocks;

        private static readonly List<List<Block>> Shapes = new List<List<Block>>
        {
            new List<Block> { new Block { X = 0, Y = 0 }, new Block { X = 1, Y = 0 }, new Block { X = 2, Y = 0 }, new Block { X = 3, Y = 0 } }, // I
            new List<Block> { new Block { X = 0, Y = 0 }, new Block { X = 1, Y = 0 }, new Block { X = 0, Y = 1 }, new Block { X = 1, Y = 1 } }, // O
            new List<Block> { new Block { X = 1, Y = 0 }, new Block { X = 0, Y = 1 }, new Block { X = 1, Y = 1 }, new Block { X = 2, Y = 1 } }, // T
            new List<Block> { new Block { X = 1, Y = 0 }, new Block { X = 2, Y = 0 }, new Block { X = 0, Y = 1 }, new Block { X = 1, Y = 1 } }, // S
            new List<Block> { new Block { X = 0, Y = 0 }, new Block { X = 1, Y = 0 }, new Block { X = 1, Y = 1 }, new Block { X = 2, Y = 1 } }, // Z
            new List<Block> { new Block { X = 0, Y = 0 }, new Block { X = 0, Y = 1 }, new Block { X = 1, Y = 1 }, new Block { X = 2, Y = 1 } }, // J
            new List<Block> { new Block { X = 2, Y = 0 }, new Block { X = 0, Y = 1 }, new Block { X = 1, Y = 1 }, new Block { X = 2, Y = 1 } }  // L
        };


        public Shape()
        {
            var random = new Random();
            int shapeIndex = random.Next(Shapes.Count);
            blocks = Shapes[shapeIndex];
        }

        public void ResetBlocks(List<Block> originalBlocks)
        {
            blocks = new List<Block>(originalBlocks);
        }

        // Method to move the shape left
        public void MoveLeft()
        {
            foreach (var block in blocks)
            {
                block.X -= 1;
            }
        }

        // Method to move the shape right
        public void MoveRight()
        {
            foreach (var block in blocks)
            {
                block.X += 1;
            }
        }

        // Method to move the shape down
        public void MoveDown()
        {
            foreach (var block in blocks)
            {
                block.Y += 1;
            }
        }

        // Method to rotate the shape
        public void Rotate()
        {
            var pivot = blocks[0];
            foreach (var block in blocks)
            {
                // Translate block to origin (pivot)
                int translatedX = block.X - pivot.X;
                int translatedY = block.Y - pivot.Y;

                // Rotate 90 degrees clockwise
                int rotatedX = translatedY;
                int rotatedY = -translatedX;

                // Translate block back to pivot position
                block.X = pivot.X + rotatedX;
                block.Y = pivot.Y + rotatedY;
            }
        }

        // Additional methods as needed
    }
}
