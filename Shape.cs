using System;
using System.Collections.Generic;
using System.Linq;

namespace FormTetris
{
    public class Shape
    {
        private List<Block> blocks;
        private int rotationIndex;
        private List<List<Block>> orientations;
        public List<Block> Blocks => new List<Block>(blocks);

        public Shape()
        {
            var random = new Random();
            int shapeType = random.Next(Shapes.Count);

            orientations = Shapes[shapeType];
            rotationIndex = 0;
            blocks = new List<Block>(orientations[rotationIndex]);
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
            var originalBlocks = new List<Block>(Blocks); // Save current state
            int newRotationIndex = clockwise
                ? (rotationIndex + 1) % orientations.Count
                : (rotationIndex - 1 + orientations.Count) % orientations.Count;

            var currentReferenceBlock = blocks[0]; // Reference block for current orientation
            var newReferenceBlock = orientations[newRotationIndex][0]; // Reference block for new orientation

            List<Block> newOrientation = orientations[newRotationIndex]
                .Select(b => new Block(b.X - newReferenceBlock.X + currentReferenceBlock.X,
                                       b.Y - newReferenceBlock.Y + currentReferenceBlock.Y)).ToList();

            if (IsValidPosition(newOrientation, board))
            {
                rotationIndex = newRotationIndex;
                blocks = newOrientation;
            }
            else if (!TryWallKick(newOrientation, board))
            {
                blocks = originalBlocks; // Revert if wall kick fails
            }
        }


        private bool IsValidPosition(List<Block> newOrientation, Board board)
        {
            return newOrientation.All(block => !board.IsPositionOccupied(block.X, block.Y) &&
                                               block.X >= 0 && block.X < board.BoardWidth &&
                                               block.Y < board.BoardHeight);
        }

        private bool TryWallKick(List<Block> newOrientation, Board board)
        {
            var wallKickTranslations = new List<(int x, int y)> { (-1, 0), (1, 0) };

            foreach (var (x, y) in wallKickTranslations)
            {
                var movedOrientation = newOrientation.Select(block => new Block(block.X + x, block.Y + y)).ToList();
                if (IsValidPosition(movedOrientation, board))
                {
                    blocks = movedOrientation;
                    return true;
                }
            }

            return false; // Wall kick not possible
        }

        private static readonly List<List<List<Block>>> Shapes = new List<List<List<Block>>>
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
