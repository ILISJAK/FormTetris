using System.Linq;

namespace FormTetris
{
    public class Game
    {
        private Board board;
        private Shape currentShape;
        private bool isGameOver;

        public Board Board => board;
        public Shape CurrentShape => currentShape;
        public bool IsGameOver => isGameOver;

        public Game()
        {
            board = new Board();
            Start();
        }

        public void Start()
        {
            InitializeNewShape();
        }

        public void Update()
        {
            if (isGameOver) return;

            if (!CanMoveShape(0, 1))
            {
                PlaceShapeAndCheckLines();
            }
            else
            {
                MoveShapeDown();
            }
            isGameOver = CheckGameOver();
        }

        private void InitializeNewShape()
        {
            currentShape = Shapes.GetRandomShape();

            int shapeWidth = currentShape.Blocks.Max(block => block.X) - currentShape.Blocks.Min(block => block.X) + 1;
            int startX = (Board.BoardWidth - shapeWidth) / 2;
            int startY = -currentShape.Blocks.Min(block => block.Y);

            foreach (var block in currentShape.Blocks)
            {
                block.X += startX;
                block.Y += startY;
            }

            // Check if any block of the new shape is colliding
            if (currentShape.Blocks.Any(block => board.IsPositionOccupied(block.X, block.Y)))
            {
                isGameOver = true;
            }
        }

        private bool CanMoveShape(int deltaX, int deltaY)
        {
            return currentShape.Blocks.All(block =>
                !board.IsPositionOccupied(block.X + deltaX, block.Y + deltaY) &&
                block.X + deltaX >= 0 && block.X + deltaX < board.BoardWidth &&
                block.Y + deltaY < board.BoardHeight);
        }

        private void PlaceShapeAndCheckLines()
        {
            board.PlaceShape(currentShape);
            board.CheckLines();
            InitializeNewShape();
        }

        public void MoveShapeDown()
        {
            if (CanMoveShape(0, 1))
            {
                currentShape.MoveDown();
            }
            else
            {
                PlaceShapeAndCheckLines();
            }
        }

        public void MoveShapeLeft()
        {
            if (CanMoveShape(-1, 0))
            {
                currentShape.MoveLeft();
                if (!CanMoveShape(0, 1))  // Check for collision immediately after moving
                {
                    PlaceShapeAndCheckLines();
                }
            }
        }

        public void MoveShapeRight()
        {
            if (CanMoveShape(1, 0))
            {
                currentShape.MoveRight();
                if (!CanMoveShape(0, 1))  // Check for collision immediately after moving
                {
                    PlaceShapeAndCheckLines();
                }
            }
        }

        public void DropShape()
        {
            if (CanMoveShape(0, 1))
            {
                currentShape.MoveDown();
            }
            else
            {
                PlaceShapeAndCheckLines();
            }
        }


        public void RotateShape(bool clockwise)
        {
            currentShape.Rotate(clockwise, board);
        }

        private bool CheckGameOver()
        {
            // Assuming IsPositionOccupied checks if a specific position is occupied
            for (int x = 0; x < board.BoardWidth; x++)
            {
                if (board.IsPositionOccupied(x, 0)) // Check the top row for a game over condition
                    return true;
            }
            return false;
        }
    }
}
