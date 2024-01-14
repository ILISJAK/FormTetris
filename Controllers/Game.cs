﻿using System;
using System.Linq;
using System.Windows.Forms;

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

        private Timer gameLoopTimer;

        public Shape GhostShape => ghostShape;
        public Board Board => board;
        public Shape CurrentShape => currentShape;
        public bool IsGameOver => isGameOver;
        public bool IsRunning => isRunning;
        private bool manualDropOccurred;

        public Game()
        {
            board = new Board();
            bag = new ShapeBag();
            gameLoopTimer = new Timer();
            gameLoopTimer.Interval = 1000 / 2;
            gameLoopTimer.Tick += new EventHandler(GameLoop);
            Start();
        }

        private void GameLoop(object sender, EventArgs e)
        {
            Update();
        }

        public void Start()
        {
            InitializeNewShape();
            Reset();
            isRunning = true;
            gameLoopTimer.Start();
        }
        public void Pause()
        {
            gameLoopTimer.Stop();
            isRunning = false;
        }

        public void Resume()
        {
            if (!isGameOver)
            {
                gameLoopTimer.Start();
                isRunning = true;
            }
        }

        public void Reset()
        {
            board.ClearBoard();
            bag.Reset();
            InitializeNewShape();
            isGameOver = false;
            isRunning = false;
        }

        public void Update()
        {
            if (isGameOver)
            {
                return;
            }

            if (!manualDropOccurred && !CanMoveShape(0, 1))
            {
                PlaceShapeAndCheckLines();
                isGameOver = CheckGameOver();
                if (isGameOver)
                {
                    return;
                }
            }
            else if (!manualDropOccurred)
            {
                MoveShapeDown();
            }
            manualDropOccurred = false;
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
            currentShape = bag.GetNextShape(); // placeholder since we are only examining this shape

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
                // If there's a collision when the shape should be in the initial position,
                // then it's game over because the spawning area is blocked.
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

        // overload for ghostshape
        private bool CanMoveShape(int deltaX, int deltaY, Shape shape)
        {
            return shape.Blocks.All(block =>
                !board.IsPositionOccupied(block.X + deltaX, block.Y + deltaY) &&
                block.X + deltaX >= 0 && block.X + deltaX < board.BoardWidth &&
                block.Y + deltaY < board.BoardHeight);
        }

        private void PlaceShapeAndCheckLines()
        {
            board.PlaceShape(currentShape);
            board.CheckLines();
            if (!isGameOver)
            {
                InitializeNewShape();
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
                    // Clear the current shape to make sure it doesn't appear on the board after game over
                    currentShape = null;
                    isRunning = false;
                    return true;
                }
            }
            return false;
        }
    }
}