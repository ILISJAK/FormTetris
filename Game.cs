using System.Collections.Generic;
using System.Linq;

namespace FormTetris
{
    public class Game
    {
        private Board board;
        private Shape currentShape;
        private bool isGameOver;

        public Board Board { get { return board; } }
        public Shape CurrentShape { get { return currentShape; } }
        public bool IsGameOver { get { return isGameOver; } }

        public Game()
        {
            board = new Board();
            // Initialize other game components
        }

        // Method to start the game loop
        public void Start()
        {
            InitializeNewShape();
        }

        public void Update()
        {
            MoveShapeDown();

            // Update game over condition
            isGameOver = CheckGameOver();
        }


        public void InitializeNewShape()
        {
            currentShape = CreateNewShape();

            // Find the width of the shape
            int minX = currentShape.Blocks.Min(block => block.X);
            int maxX = currentShape.Blocks.Max(block => block.X);
            int shapeWidth = maxX - minX + 1;

            // Center the shape horizontally based on its width
            int startX = (Board.BoardWidth - shapeWidth) / 2;

            // Set startY to just above the top of the board
            int startY = -currentShape.Blocks.Min(block => block.Y);

            // Adjust each block's position
            foreach (var block in currentShape.Blocks)
            {
                block.X += startX;
                block.Y += startY;
            }
        }


        private Shape CreateNewShape()
        {
            // This method should create and return a new Shape object
            // For now, let's create a simple square shape or any other shape you prefer
            return new Shape(); // Replace this with actual shape creation logic
        }

        private bool CanMoveShape(int deltaX, int deltaY)
        {
            foreach (var block in currentShape.Blocks)
            {
                int newX = block.X + deltaX;
                int newY = block.Y + deltaY;

                // Check horizontal boundaries and bottom boundary
                if (newX < 0 || newX >= board.BoardWidth || newY >= board.BoardHeight)
                    return false;

                // Check for collision with placed blocks
                if (board.IsPositionOccupied(newX, newY))
                    return false;
            }
            return true;
        }

        private bool CanRotateShape()
        {
            foreach (var block in currentShape.Blocks)
            {
                // Check horizontal boundaries and bottom boundary
                if (block.X < 0 || block.X >= board.BoardWidth || block.Y >= board.BoardHeight)
                    return false;

                // Check for collision with placed blocks
                if (board.IsPositionOccupied(block.X, block.Y))
                    return false;
            }
            return true;
        }

        private void PlaceShapeAndCheckLines()
        {
            board.PlaceShape(currentShape);
            board.CheckLines();
            InitializeNewShape(); // Create a new shape for the next turn
        }


        public void MoveShapeDown()
        {
            if (CanMoveShape(0, 1)) // Check if the shape can move down
            {
                currentShape.MoveDown();
            }
            else
            {
                // Handle what happens when the shape lands
                PlaceShapeAndCheckLines();
            }
        }

        public void MoveShapeLeft()
        {
            if (CanMoveShape(-1, 0))
            {
                currentShape.MoveLeft();
            }
        }

        public void MoveShapeRight()
        {
            if (CanMoveShape(1, 0))
            {
                currentShape.MoveRight();
            }
        }

        public void RotateShape()
        {
            // Save current state in case we need to revert the rotation
            var originalBlocks = new List<Block>(currentShape.Blocks.Select(b => new Block { X = b.X, Y = b.Y }));

            // Rotate the shape
            currentShape.Rotate();

            // Check if the rotation is valid
            if (!CanRotateShape())
            {
                // Revert to original state if rotation is not valid
                currentShape.ResetBlocks(originalBlocks);
            }
        }

        private bool HasLanded()
        {
            return !CanMoveShape(0, 1);
        }

        private bool CheckGameOver()
        {
            // Game over logic
            // A simple check: if any block in the top row is occupied, it's game over
            for (int x = 0; x < board.BoardWidth; x++)
            {
                if (board.IsPositionOccupied(x, 0))
                    return true;
            }
            return false;
        }
    }
}
