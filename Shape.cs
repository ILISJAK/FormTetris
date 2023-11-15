using System.Collections.Generic;

namespace FormTetris
{
    public class Shape
    {
        private List<Block> blocks;

        public List<Block> Blocks
        {
            get { return blocks; }
        }

        public Shape()
        {
            blocks = new List<Block>
            {
                new Block { X = 0, Y = 0 },
                new Block { X = 1, Y = 0 },
                new Block { X = 2, Y = 0 },
                new Block { X = 3, Y = 0 }
            };
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
            // Implement rotation logic here.
            // This can be complex depending on the shape.
            // For a simple implementation, you could rotate around the first block in the list.
        }

        // Additional methods as needed
    }
}
