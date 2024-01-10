using System;
using System.Collections.Generic;
using System.Drawing;

namespace FormTetris
{
    public static class Shapes
    {
        public static Shape GetRandomShape()
        {
            var random = new Random();
            var shapeKeys = new List<string>(SrsData.Keys); // Get all shape type keys
            int index = random.Next(shapeKeys.Count); // Select a random index
            string shapeType = shapeKeys[index]; // Get the shape type
            var shapeDefinition = ShapeDefinitions[index]; // Get the shape definition
            return new Shape(shapeType, shapeDefinition); // Create a new shape with its type
        }

        public static Dictionary<string, Dictionary<(int, int), List<Point>>> GetSrsData()
        {
            return SrsData;
        }


        private static readonly Dictionary<string, Dictionary<(int, int), List<Point>>> SrsData =
            new Dictionary<string, Dictionary<(int, int), List<Point>>>
            {
                ["I"] = new Dictionary<(int, int), List<Point>>
                {
                    // From 0 to R, R to 0, 0 to L, L to 0
                    {(0, 1), new List<Point> {new Point(0, 0), new Point(-2, 0), new Point(1, 0), new Point(-2, -1), new Point(1, 2)}},
                    {(1, 0), new List<Point> {new Point(0, 0), new Point(2, 0), new Point(-1, 0), new Point(2, 1), new Point(-1, -2)}},
                    {(0, 3), new List<Point> {new Point(0, 0), new Point(-1, 0), new Point(2, 0), new Point(-1, 2), new Point(2, -1)}},
                    {(3, 0), new List<Point> {new Point(0, 0), new Point(1, 0), new Point(-2, 0), new Point(1, -2), new Point(-2, 1)}},
                    // From R to 2, 2 to R, 2 to L, L to 2
                    {(1, 2), new List<Point> {new Point(0, 0), new Point(-1, 0), new Point(2, 0), new Point(-1, -2), new Point(2, 1)}},
                    {(2, 1), new List<Point> {new Point(0, 0), new Point(1, 0), new Point(-2, 0), new Point(1, 2), new Point(-2, -1)}},
                    {(2, 3), new List<Point> {new Point(0, 0), new Point(2, 0), new Point(-1, 0), new Point(2, -1), new Point(-1, 2)}},
                    {(3, 2), new List<Point> {new Point(0, 0), new Point(-2, 0), new Point(1, 0), new Point(-2, 1), new Point(1, -2)}},
                },
                ["O"] = new Dictionary<(int, int), List<Point>>
{
                    // O shape does not need to kick, but we define it for completeness
                    {(0, 0), new List<Point> {new Point(0, 0)}}, // From 0 to 0
                    {(0, 1), new List<Point> {new Point(0, 0)}}, // From 0 to R
                    {(1, 0), new List<Point> {new Point(0, 0)}}, // From R to 0
                    // Repeat for all other transitions, O shape does not rotate
                },
                ["J"] = new Dictionary<(int, int), List<Point>>
                {
                    // From 0 to R, R to 0, 0 to L, L to 0
                    {(0, 1), new List<Point> {new Point(0, 0), new Point(-1, 0), new Point(-1, -1), new Point(0, 2), new Point(-1, 2)}},
                    {(1, 0), new List<Point> {new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(0, -2), new Point(1, -2)}},
                    {(0, 3), new List<Point> {new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(0, -2), new Point(1, -2)}},
                    {(3, 0), new List<Point> {new Point(0, 0), new Point(-1, 0), new Point(-1, -1), new Point(0, 2), new Point(-1, 2)}},
                    // From R to 2, 2 to R, 2 to L, L to 2
                    {(1, 2), new List<Point> {new Point(0, 0), new Point(1, 0), new Point(1, -1), new Point(0, 2), new Point(1, 2)}},
                    {(2, 1), new List<Point> {new Point(0, 0), new Point(-1, 0), new Point(-1, 1), new Point(0, -2), new Point(-1, -2)}},
                    {(2, 3), new List<Point> {new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(0, -2), new Point(1, -2)}},
                    {(3, 2), new List<Point> {new Point(0, 0), new Point(-1, 0), new Point(-1, -1), new Point(0, 2), new Point(-1, 2)}},
                },
                ["L"] = new Dictionary<(int, int), List<Point>>
                {
                    // From 0 to R, R to 0, 0 to L, L to 0
                    {(0, 1), new List<Point> {new Point(0, 0), new Point(-1, 0), new Point(-1, -1), new Point(0, 2), new Point(-1, 2)}},
                    {(1, 0), new List<Point> {new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(0, -2), new Point(1, -2)}},
                    {(0, 3), new List<Point> {new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(0, -2), new Point(1, -2)}},
                    {(3, 0), new List<Point> {new Point(0, 0), new Point(-1, 0), new Point(-1, -1), new Point(0, 2), new Point(-1, 2)}},
                    // From R to 2, 2 to R, 2 to L, L to 2
                    {(1, 2), new List<Point> {new Point(0, 0), new Point(1, 0), new Point(1, -1), new Point(0, 2), new Point(1, 2)}},
                    {(2, 1), new List<Point> {new Point(0, 0), new Point(-1, 0), new Point(-1, 1), new Point(0, -2), new Point(-1, -2)}},
                    {(2, 3), new List<Point> {new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(0, -2), new Point(1, -2)}},
                    {(3, 2), new List<Point> {new Point(0, 0), new Point(-1, 0), new Point(-1, -1), new Point(0, 2), new Point(-1, 2)}},
                },
                ["S"] = new Dictionary<(int, int), List<Point>>
                {
                    // From 0 to R, R to 0, 0 to L, L to 0
                    {(0, 1), new List<Point> {new Point(0, 0), new Point(-1, 0), new Point(-1, -1), new Point(0, 2), new Point(-1, 2)}},
                    {(1, 0), new List<Point> {new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(0, -2), new Point(1, -2)}},
                    {(0, 3), new List<Point> {new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(0, -2), new Point(1, -2)}},
                    {(3, 0), new List<Point> {new Point(0, 0), new Point(-1, 0), new Point(-1, -1), new Point(0, 2), new Point(-1, 2)}},
                    // From R to 2, 2 to R, 2 to L, L to 2
                    {(1, 2), new List<Point> {new Point(0, 0), new Point(1, 0), new Point(1, -1), new Point(0, 2), new Point(1, 2)}},
                    {(2, 1), new List<Point> {new Point(0, 0), new Point(-1, 0), new Point(-1, 1), new Point(0, -2), new Point(-1, -2)}},
                    {(2, 3), new List<Point> {new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(0, -2), new Point(1, -2)}},
                    {(3, 2), new List<Point> {new Point(0, 0), new Point(-1, 0), new Point(-1, -1), new Point(0, 2), new Point(-1, 2)}},
                },
                ["T"] = new Dictionary<(int, int), List<Point>>
                {
                    // From 0 to R, R to 0, 0 to L, L to 0
                    {(0, 1), new List<Point> {new Point(0, 0), new Point(-1, 0), new Point(-1, -1), new Point(0, 2), new Point(-1, 2)}},
                    {(1, 0), new List<Point> {new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(0, -2), new Point(1, -2)}},
                    {(0, 3), new List<Point> {new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(0, -2), new Point(1, -2)}},
                    {(3, 0), new List<Point> {new Point(0, 0), new Point(-1, 0), new Point(-1, -1), new Point(0, 2), new Point(-1, 2)}},
                    // From R to 2, 2 to R, 2 to L, L to 2
                    {(1, 2), new List<Point> {new Point(0, 0), new Point(1, 0), new Point(1, -1), new Point(0, 2), new Point(1, 2)}},
                    {(2, 1), new List<Point> {new Point(0, 0), new Point(-1, 0), new Point(-1, 1), new Point(0, -2), new Point(-1, -2)}},
                    {(2, 3), new List<Point> {new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(0, -2), new Point(1, -2)}},
                    {(3, 2), new List<Point> {new Point(0, 0), new Point(-1, 0), new Point(-1, -1), new Point(0, 2), new Point(-1, 2)}},
                },
                ["Z"] = new Dictionary<(int, int), List<Point>>
                {
                    // From 0 to R, R to 0, 0 to L, L to 0
                    {(0, 1), new List<Point> {new Point(0, 0), new Point(-1, 0), new Point(-1, -1), new Point(0, 2), new Point(-1, 2)}},
                    {(1, 0), new List<Point> {new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(0, -2), new Point(1, -2)}},
                    {(0, 3), new List<Point> {new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(0, -2), new Point(1, -2)}},
                    {(3, 0), new List<Point> {new Point(0, 0), new Point(-1, 0), new Point(-1, -1), new Point(0, 2), new Point(-1, 2)}},
                    // From R to 2, 2 to R, 2 to L, L to 2
                    {(1, 2), new List<Point> {new Point(0, 0), new Point(1, 0), new Point(1, -1), new Point(0, 2), new Point(1, 2)}},
                    {(2, 1), new List<Point> {new Point(0, 0), new Point(-1, 0), new Point(-1, 1), new Point(0, -2), new Point(-1, -2)}},
                    {(2, 3), new List<Point> {new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(0, -2), new Point(1, -2)}},
                    {(3, 2), new List<Point> {new Point(0, 0), new Point(-1, 0), new Point(-1, -1), new Point(0, 2), new Point(-1, 2)}},
                }
            };

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
