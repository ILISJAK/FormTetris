using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FormTetris
{
    public class Shape
    {
        public string ShapeType { get; private set; }
        private List<Block> blocks;
        private int rotationIndex;
        private List<List<Block>> orientations;
        public Color ShapeColor { get; set; }

        public List<Block> Blocks => blocks;

        public Shape(string shapeType, List<List<Block>> shapeOrientations, Color color)
        {
            ShapeType = shapeType;
            orientations = shapeOrientations;
            rotationIndex = 0;
            blocks = new List<Block>(orientations[rotationIndex]);
            ShapeColor = color;
        }

        public Shape Clone()
        {
            Shape clonedShape = new Shape(ShapeType, orientations, ShapeColor);
            clonedShape.blocks = new List<Block>(blocks.Select(block => new Block(block.X, block.Y)));
            clonedShape.rotationIndex = rotationIndex;
            return clonedShape;
        }

        public void ResetBlocks(List<Block> originalBlocks)
        {
            blocks = new List<Block>(originalBlocks);
        }

        public void MoveLeft()
        {
            foreach (var block in blocks)
            {
                block.X -= 1;
            }
        }

        public void MoveRight()
        {
            foreach (var block in blocks)
            {
                block.X += 1;
            }
        }

        public void MoveDown()
        {
            foreach (var block in blocks)
            {
                block.Y += 1;
            }
        }

        public void Rotate(bool clockwise, Board board)
        {
            // Assuming the pivot is the second block in the list for simplicity
            Block pivot = blocks[1];
            List<Block> newPositions = new List<Block>();

            // Calculate new positions based on rotation
            foreach (var block in blocks)
            {
                int relativeX = block.X - pivot.X;
                int relativeY = block.Y - pivot.Y;

                if (clockwise)
                {
                    // Clockwise rotation
                    newPositions.Add(new Block(pivot.X + relativeY, pivot.Y - relativeX));
                }
                else
                {
                    // Counterclockwise rotation
                    newPositions.Add(new Block(pivot.X - relativeY, pivot.Y + relativeX));
                }
            }

            // Check for collisions with the board boundaries or other blocks
            foreach (var pos in newPositions)
            {
                if (pos.X < 0 || pos.X >= board.BoardWidth || pos.Y < 0 || pos.Y >= board.BoardHeight ||
                    board.IsPositionOccupied(pos.X, pos.Y))
                {
                    return; // Collision detected, so don't rotate
                }
            }

            // If no collision, update blocks to their new positions
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i].X = newPositions[i].X;
                blocks[i].Y = newPositions[i].Y;
            }
        }
    }
}
