using System;
using System.Collections.Generic;

namespace FormTetris
{
    public class ShapeBag
    {
        private Queue<Shape> bag;
        private Random random;

        public ShapeBag()
        {
            random = new Random();
            FillBag();
        }

        private void FillBag()
        {
            var allShapes = new List<Shape>
            {
                new ShapeI(),
                new ShapeJ(),
                new ShapeL(),
                new ShapeO(),
                new ShapeS(),
                new ShapeT(),
                new ShapeZ()
            };

            // Shuffle the list using Fisher-Yates shuffle
            for (int i = allShapes.Count - 1; i > 0; i--)
            {
                int swapIndex = random.Next(i + 1);
                Shape temp = allShapes[i];
                allShapes[i] = allShapes[swapIndex];
                allShapes[swapIndex] = temp;
            }

            // Enqueue the shapes into the bag
            bag = new Queue<Shape>(allShapes);
        }

        public Shape GetNextShape()
        {
            if (bag.Count == 0)
                FillBag();

            return bag.Dequeue();
        }
        public void Reset()
        {
            bag.Clear();
            FillBag();
        }

    }
}
