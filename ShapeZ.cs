using System.Collections.Generic;
using System.Drawing;

namespace FormTetris
{
    public class ShapeZ : Shape
    {
        public ShapeZ() : base("Z", new List<List<Block>>
        {
            new List<Block> { new Block(0, 0), new Block(1, 0), new Block(1, 1), new Block(2, 1) },
        }, Color.Red)
        {
        }
    }
}
