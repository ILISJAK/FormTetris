using System.Collections.Generic;
using System.Drawing;

namespace FormTetris
{
    public class ShapeJ : Shape
    {
        public ShapeJ() : base("J", new List<List<Block>>
        {
            new List<Block> { new Block(0, 1), new Block(0, 0), new Block(1, 0), new Block(2, 0) },
        }, Color.BlueViolet)
        {
        }
    }
}
