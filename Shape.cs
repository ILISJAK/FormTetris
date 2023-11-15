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
            blocks = new List<Block>();
            // Initialize the shape with blocks
        }

        // Method to rotate the shape
        public void Rotate() { /* ... */ }

        // Other methods as needed
    }

}
