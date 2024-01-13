
using System.Collections.Generic;
using System.Drawing;

namespace FormTetris
{
    public class ShapeI : Shape
    {
        public ShapeI() : base("I", new List<List<Block>>
        {
            new List<Block> { new Block(0, 1), new Block(1, 1), new Block(2, 1), new Block(3, 1) }, // Initial state
        }, Color.Cyan)
        {
        }
    }
}
