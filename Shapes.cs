using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormTetris
{
    public static class Shapes
    {
        public static Shape GetRandomShape()
        {
            var random = new Random();
            int shapeType = random.Next(ShapeDefinitions.Count);
            return new Shape(ShapeDefinitions[shapeType]);
        }

        private static readonly List<List<List<Block>>> ShapeDefinitions = new List<List<List<Block>>>
        {
            // I Shape
            new List<List<Block>>
            {
                new List<Block> { new Block(0, 1), new Block(1, 1), new Block(2, 1), new Block(3, 1) },
                new List<Block> { new Block(2, 0), new Block(2, 1), new Block(2, 2), new Block(2, 3) },
                new List<Block> { new Block(0, 2), new Block(1, 2), new Block(2, 2), new Block(3, 2) },
                new List<Block> { new Block(1, 0), new Block(1, 1), new Block(1, 2), new Block(1, 3) }
            },
            // O Shape
            new List<List<Block>>
            {
                new List<Block> { new Block(0, 0), new Block(1, 0), new Block(0, 1), new Block(1, 1) }
            },
            // T Shape
            new List<List<Block>>
            {
                new List<Block> { new Block(1, 0), new Block(0, 1), new Block(1, 1), new Block(2, 1) },
                new List<Block> { new Block(1, 0), new Block(0, 1), new Block(1, 1), new Block(1, 2) },
                new List<Block> { new Block(0, 1), new Block(1, 1), new Block(2, 1), new Block(1, 2) },
                new List<Block> { new Block(1, 0), new Block(1, 1), new Block(2, 1), new Block(1, 2) }
            },
            // S Shape
            new List<List<Block>>
            {
                new List<Block> { new Block(1, 0), new Block(2, 0), new Block(0, 1), new Block(1, 1) },
                new List<Block> { new Block(1, 0), new Block(1, 1), new Block(2, 1), new Block(2, 2) },
                new List<Block> { new Block(1, 1), new Block(2, 1), new Block(0, 2), new Block(1, 2) },
                new List<Block> { new Block(0, 0), new Block(0, 1), new Block(1, 1), new Block(1, 2) }
            },
            // Z Shape
            new List<List<Block>>
            {
                new List<Block> { new Block(0, 0), new Block(1, 0), new Block(1, 1), new Block(2, 1) },
                new List<Block> { new Block(2, 0), new Block(1, 1), new Block(2, 1), new Block(1, 2) },
                new List<Block> { new Block(0, 1), new Block(1, 1), new Block(1, 2), new Block(2, 2) },
                new List<Block> { new Block(1, 0), new Block(0, 1), new Block(1, 1), new Block(0, 2) }
            },
            // J Shape
            new List<List<Block>>
            {
                new List<Block> { new Block(0, 0), new Block(0, 1), new Block(1, 1), new Block(2, 1) },
                new List<Block> { new Block(1, 0), new Block(2, 0), new Block(1, 1), new Block(1, 2) },
                new List<Block> { new Block(0, 1), new Block(1, 1), new Block(2, 1), new Block(2, 2) },
                new List<Block> { new Block(1, 0), new Block(1, 1), new Block(0, 1), new Block(1, 2) }
            },
            // L Shape
            new List<List<Block>>
            {
                new List<Block> { new Block(2, 0), new Block(0, 1), new Block(1, 1), new Block(2, 1) },
                new List<Block> { new Block(1, 0), new Block(1, 1), new Block(1, 2), new Block(2, 2) },
                new List<Block> { new Block(0, 1), new Block(1, 1), new Block(2, 1), new Block(0, 2) },
                new List<Block> { new Block(0, 0), new Block(1, 0), new Block(1, 1), new Block(1, 2) }
            }
        };
    }
}
