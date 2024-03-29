﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace FormTetris
{
    public class Game
    {
        private Board board;
        private Shape currentShape;
        private Shape ghostShape;
        private bool isGameOver;
        private bool isRunning;
        private ShapeBag bag;

        private Thread gameLoopThread;
        private bool gameLoopRunning;
        private Stopwatch stopwatch;
        private double deltaTime;
        private double fallSpeed;

        public event EventHandler GameOver;
        public ScoreManager ScoreManager { get; private set; }
        public Shape GhostShape => ghostShape;
        public Board Board => board;
        public Shape CurrentShape => currentShape;
        public bool IsGameOver => isGameOver;
        public bool IsRunning => isRunning;
        private bool manualDropOccurred;

        public Game()
        {
            ScoreManager = ScoreManager.Instance;
            board = new Board();
            bag = new ShapeBag();
            gameLoopThread = new Thread(new ThreadStart(GameLoop));
            stopwatch = new Stopwatch();
            board.LinesCleared += OnLinesCleared;
            ScoreManager.LevelChanged += OnLevelChanged;
            UpdateFallSpeed();
        }

        private void GameLoop()
        {
            stopwatch.Start();
            long lastTime = stopwatch.ElapsedMilliseconds;

            while (gameLoopRunning)
            {
                long currentTime = stopwatch.ElapsedMilliseconds;
                deltaTime = (currentTime - lastTime) / 1000.0; // Convert milliseconds to seconds
                lastTime = currentTime;

                Update(deltaTime);

                // Sleep to control the frame rate (60 FPS in this case)
                Thread.Sleep(16);
            }

            stopwatch.Stop();
        }

        public void Start()
        {
            InitializeNewShape();
            Reset();
            isRunning = true;
            gameLoopRunning = true;
            gameLoopThread = new Thread(new ThreadStart(GameLoop));
            gameLoopThread.Start();
            ScoreManager.Instance.StartGame();
        }

        public void Pause()
        {
            Debug.WriteLine("Pausing game...");
            gameLoopRunning = false;
            isRunning = false;
            ScoreManager.Instance.PauseGameTime();
        }

        public void Resume()
        {
            if (!isGameOver)
            {
                if (gameLoopThread == null || !gameLoopThread.IsAlive)
                {
                    gameLoopRunning = true;
                    gameLoopThread = new Thread(new ThreadStart(GameLoop));
                    gameLoopThread.Start();
                }

                isRunning = true;
                ScoreManager.Instance.ResumeGameTime();
            }
        }

        public void Reset()
        {
            board.ClearBoard();
            bag.Reset();
            InitializeNewShape();
            isGameOver = false;
            isRunning = false;
            gameLoopRunning = false;
            ScoreManager.Instance.Reset();
        }

        private double timeSinceLastMove = 0;

        public void Update(double deltaTime)
        {
            if (isGameOver)
            {
                return;
            }

            timeSinceLastMove += deltaTime;

            if (timeSinceLastMove >= fallSpeed)
            {
                if (!manualDropOccurred)
                {
                    MoveShapeDown();
                }
                timeSinceLastMove = 0;
            }

            manualDropOccurred = false;
        }

        private void UpdateFallSpeed()
        {
            // Original fall speed calculation logic
            var newFallSpeed = Math.Max(1.0 - (ScoreManager.Level - 1) * 0.05, 0.1);

            // Debugging information
            Debug.WriteLine($"Updating fall speed. Level: {ScoreManager.Level}, New Fall Speed: {newFallSpeed}");

            // Check if the fall speed has actually changed
            if (Math.Abs(fallSpeed - newFallSpeed) > double.Epsilon)
            {
                Debug.WriteLine($"Fall Speed changed from {fallSpeed} to {newFallSpeed}");
                fallSpeed = newFallSpeed;
            }
        }


        private void UpdateGhostShape()
        {
            // Clone the current shape to create a ghost shape
            ghostShape = currentShape.Clone();

            // Move the ghost shape down until it can't move any further
            while (CanMoveShape(0, 1, ghostShape))
            {
                ghostShape.MoveDown();
            }
        }

        private void InitializeNewShape()
        {
            if (isGameOver) { return; }
            currentShape = bag.GetNextShape();
            if (currentShape == null) { bag.Reset(); }
            // Calculate the width and the leftmost position of the shape
            int minX = currentShape.Blocks.Min(block => block.X);
            int maxX = currentShape.Blocks.Max(block => block.X);
            int shapeWidth = maxX - minX + 1;

            // Center the shape horizontally
            int startX = (Board.BoardWidth - shapeWidth) / 2 - minX;

            // Adjust the Y offset to start the shape from the second-highest row
            int minY = currentShape.Blocks.Min(block => block.Y);
            int shapeHeight = currentShape.Blocks.Max(block => block.Y) - minY + 1;
            int startY = 2 - shapeHeight; // This will place the shape's lowest block at row 1

            // Assign the position to each block of the shape
            foreach (var block in currentShape.Blocks)
            {
                block.X += startX;
                block.Y += startY; // Adjust the Y position of the blocks
            }

            // Check if any block of the new shape is colliding at the new starting position
            if (currentShape.Blocks.Any(block => board.IsPositionOccupied(block.X, block.Y)))
            {
                CheckGameOver();
            }
        }


        private bool CanMoveShape(int deltaX, int deltaY)
        {
            if (currentShape == null)
                return false;
            return currentShape.Blocks.All(block =>
                !board.IsPositionOccupied(block.X + deltaX, block.Y + deltaY) &&
                block.X + deltaX >= 0 && block.X + deltaX < board.BoardWidth &&
                block.Y + deltaY < board.BoardHeight);
        }

        // overload for ghostshape
        private bool CanMoveShape(int deltaX, int deltaY, Shape shape)
        {
            if (currentShape == null)
                return false;
            return shape.Blocks.All(block =>
                !board.IsPositionOccupied(block.X + deltaX, block.Y + deltaY) &&
                block.X + deltaX >= 0 && block.X + deltaX < board.BoardWidth &&
                block.Y + deltaY < board.BoardHeight);
        }

        private void PlaceShapeAndCheckLines()
        {
            board.PlaceShape(currentShape);
            board.CheckLines();

            isGameOver = CheckGameOver();
            if (!isGameOver)
            {
                ScoreManager.Instance.TetrominoDropped();
                InitializeNewShape();
            }
        }

        private void OnLinesCleared(int linesCleared)
        {
            if (linesCleared > 0)
            {
                ScoreManager.Instance.LineCleared(linesCleared);
                DebugForm.Instance.Log($"Lines cleared: {linesCleared}, Current score: {ScoreManager.Instance.TotalScore}");
            }
        }

        public void MoveShapeDown()
        {
            if (CanMoveShape(0, 1))
            {
                currentShape.MoveDown();
                UpdateGhostShape();
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
                UpdateGhostShape();
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
                UpdateGhostShape();
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
                manualDropOccurred = true;
            }
            else
            {
                PlaceShapeAndCheckLines();
            }
        }

        public void FastDrop()
        {
            while (CanMoveShape(0, 1))
            {
                currentShape.MoveDown();
            }
            PlaceShapeAndCheckLines();
            CheckGameOver();
        }

        public void RotateShape(bool clockwise)
        {
            currentShape.Rotate(clockwise, board);
            UpdateGhostShape();
        }

        private bool CheckGameOver()
        {
            for (int x = 0; x < board.BoardWidth; x++)
            {
                if (board.IsPositionOccupied(x, 0))
                {
                    currentShape = null;
                    ScoreManager.Instance.PauseGameTime();
                    isGameOver = true;
                    isRunning = false;
                    OnGameOver();
                    return true;
                }
            }
            return false;
        }
        private void OnLevelChanged(int newLevel)
        {
            UpdateFallSpeed();
        }
        protected virtual void OnGameOver()
        {
            // ScoreManager.Instance.SaveScore();
            GameOver?.Invoke(this, EventArgs.Empty);
        }
    }
}
