using System.Collections.Generic;
using System.Linq;

namespace FormTetris
{
    public class Shape
    {
        public string ShapeType { get; private set; }
        private List<Block> blocks;
        private int rotationIndex;
        private List<List<Block>> orientations;

        public List<Block> Blocks => new List<Block>(blocks);

        public Shape(string shapeType, List<List<Block>> shapeOrientations)
        {
            ShapeType = shapeType;
            orientations = shapeOrientations;
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
            DebugForm.Instance.Log("Attempting to rotate shape.");

            int currentRotationState = rotationIndex;
            int newRotationState = clockwise ? (rotationIndex + 1) % orientations.Count
                                             : (rotationIndex - 1 + orientations.Count) % orientations.Count;

            var wallKickDataDictionary = Shapes.GetSrsData()[this.ShapeType];
            var rotationKey = (currentRotationState, newRotationState);

            if (wallKickDataDictionary.TryGetValue(rotationKey, out var wallKickData))
            {
                DebugForm.Instance.Log($"Rotation data found for {ShapeType} from {currentRotationState} to {newRotationState}.");
                foreach (var point in wallKickData)
                {
                    var translatedBlocks = Blocks.Select(block => new Block(block.X + point.X, block.Y + point.Y)).ToList();
                    if (IsValidPosition(translatedBlocks, board))
                    {
                        blocks = translatedBlocks;
                        rotationIndex = newRotationState;
                        DebugForm.Instance.Log("Rotation successful.");
                        return;
                    }
                }
                DebugForm.Instance.Log("Rotation failed. No valid position found.");
            }
            else
            {
                DebugForm.Instance.Log($"No rotation data found for {ShapeType} from {currentRotationState} to {newRotationState}.");
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
    }
}
