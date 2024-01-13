using System.Collections.Generic;
using System.Drawing;

namespace FormTetris
{
    public class ShapeT : Shape
    {
        public ShapeT() : base("T", new List<List<Block>>
        {
            new List<Block> { new Block(1, 0), new Block(0, 1), new Block(1, 1), new Block(2, 1) },
        }, Color.Purple)
        {
        }
    }
}
