namespace FormTetris
{
    public class Game
    {
        private Board board;
        private Shape currentShape;
        private bool isGameOver;

        public Game()
        {
            board = new Board();
            // Initialize other game components
        }

        // Method to start the game loop
        public void Start() { /* ... */ }

        public void Update()
        {
            // Move the current shape down
            MoveShapeDown();

            // Check for landing
            if (HasLanded())
            {
                board.PlaceShape(currentShape);
                board.CheckLines();
                InitializeNewShape(); // Create a new shape for the next turn
            }

            // Check for game over condition
            if (IsGameOver())
            {
                isGameOver = true;
                // Handle game over (e.g., display game over message, stop the game loop)
            }
        }

        public void InitializeNewShape()
        {
            currentShape = CreateNewShape();
            // Position the shape at the top of the board
            // For simplicity, let's say the starting position is at the top middle
            int startX = board.BoardWidth / 2;
            int startY = 0;
            foreach (var block in currentShape.Blocks)
            {
                block.X = startX + block.X;
                block.Y = startY + block.Y;
            }
        }

        private Shape CreateNewShape()
        {
            // This method should create and return a new Shape object
            // For now, let's create a simple square shape or any other shape you prefer
            return new Shape(); // Replace this with actual shape creation logic
        }


        private void MoveShapeDown()
        {
            // Move each block of the shape down by 1
            foreach (var block in currentShape.Blocks)
            {
                block.Y += 1;
            }
        }

        private bool HasLanded()
        {
            // Implement logic to check if the shape has landed
            // A shape has landed if any block is at the bottom or on top of another block
            return false; // Replace with actual logic
        }

        private bool IsGameOver()
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
