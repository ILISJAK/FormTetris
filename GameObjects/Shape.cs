using System;
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

        public IEnumerable<Block> Blocks => blocks.AsReadOnly();

        public Shape(string shapeType, List<List<Block>> shapeOrientations, Color color)
        {
            ShapeType = shapeType ?? throw new ArgumentNullException(nameof(shapeType));
            orientations = shapeOrientations ?? throw new ArgumentNullException(nameof(shapeOrientations));
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
            if (ShapeType == "O") { return; }

            // Calculate new positions based on rotation
            List<Block> newPositions = CalculateRotatedPositions(clockwise);

            // Try to rotate without moving
            if (IsRotationValid(newPositions, 0, 0, board))
            {
                ApplyRotation(newPositions);
                return;
            }

            // Lazy Wall kick: Try moving left (-1) or right (1) to rotate
            // Additionally, check for downward movement if too close to ceiling
            foreach (int offsetX in new[] { -1, 1 })
            {
                if (IsRotationValid(newPositions, offsetX, 0, board))
                {
                    ApplyRotation(newPositions, offsetX);
                    return;
                }

                // Check for ceiling condition: move down and try rotate
                if (IsRotationValid(newPositions, offsetX, 1, board))
                {
                    ApplyRotation(newPositions, offsetX, 1);
                    return;
                }
            }

            // Check for direct downward movement
            if (IsRotationValid(newPositions, 0, 1, board))
            {
                ApplyRotation(newPositions, 0, 1);
                return;
            }
        }

        // helper methods for rotation 

        private List<Block> CalculateRotatedPositions(bool clockwise)
        {
            Block pivot = blocks[1];
            List<Block> newPositions = new List<Block>();

            foreach (var block in blocks)
            {
                int relativeX = block.X - pivot.X;
                int relativeY = block.Y - pivot.Y;

                if (clockwise)
                {
                    newPositions.Add(new Block(pivot.X + relativeY, pivot.Y - relativeX));
                }
                else
                {
                    newPositions.Add(new Block(pivot.X - relativeY, pivot.Y + relativeX));
                }
            }

            return newPositions;
        }

        private bool IsRotationValid(List<Block> newPositions, int offsetX, int offsetY, Board board)
        {
            return newPositions.All(pos => board.IsPositionWithinBounds(pos.X + offsetX, pos.Y + offsetY) &&
                                           !board.IsPositionOccupied(pos.X + offsetX, pos.Y + offsetY));
        }

        private void ApplyRotation(List<Block> newPositions, int offsetX = 0, int offsetY = 0)
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i].X = newPositions[i].X + offsetX;
                blocks[i].Y = newPositions[i].Y + offsetY;
            }
        }
    }
}
