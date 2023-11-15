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
            // Move the current shape down
            MoveShapeDown();

            // Check for landing
            if (HasLanded())
            {
                board.PlaceShape(currentShape);
                board.CheckLines();
                InitializeNewShape(); // Create a new shape for the next turn
            }

            // Update game over condition
            isGameOver = CheckGameOver();
        }

        public void InitializeNewShape()
        {
            currentShape = CreateNewShape();
            int startX = Board.BoardWidth / 2 - 2; // Center the shape horizontally
            int startY = 0; // Start at the top of the board
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
