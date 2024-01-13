using System;
using System.Collections.Generic;
using System.Drawing;

namespace FormTetris
{
    public class GameRenderer
    {
        private Game game;
        private int blockSize;
        private Point gameAreaStart;
        private Dictionary<Point, DateTime> blockDrawTimes = new Dictionary<Point, DateTime>();
        private readonly TimeSpan glowDuration = TimeSpan.FromSeconds(0.5);

        public GameRenderer(Game game, int blockSize, Point gameAreaStart)
        {
            this.game = game;
            this.blockSize = blockSize;
            this.gameAreaStart = gameAreaStart;
        }

        public void UpdateSize(Size newSize)
        {
            // Recalculate blockSize based on the new size
            blockSize = Math.Min(newSize.Width / game.Board.BoardWidth, newSize.Height / game.Board.BoardHeight);

            // Recalculate gameAreaStart to center the game board in the new window size
            int gameAreaWidth = game.Board.BoardWidth * blockSize;
            int gameAreaHeight = game.Board.BoardHeight * blockSize;
            gameAreaStart = new Point((newSize.Width - gameAreaWidth) / 2, (newSize.Height - gameAreaHeight) / 2);
        }

        public void DrawGame(Graphics graphics)
        {
            if (!game.IsRunning)
            {
                return;
            }
            DrawBoardOutline(graphics);
            DrawBoard(graphics);
            DrawCurrentShape(graphics);
        }

        public void UpdateBlockSizeAndStart(int blockSize, Point gameAreaStart)
        {
            this.blockSize = blockSize;
            this.gameAreaStart = gameAreaStart;
        }

        private void DrawBoardOutline(Graphics graphics)
        {
            int outlineWidth = game.Board.BoardWidth * blockSize;
            int outlineHeight = game.Board.BoardHeight * blockSize;
            Point outlineStart = gameAreaStart;

            using (Pen outlinePen = new Pen(Color.White, 2)) // Outline thickness
            {
                graphics.DrawRectangle(outlinePen, outlineStart.X, outlineStart.Y, outlineWidth, outlineHeight);
            }
        }

        private void DrawBoard(Graphics graphics)
        {
            for (int x = 0; x < game.Board.BoardWidth; x++)
            {
                for (int y = 0; y < game.Board.BoardHeight; y++)
                {
                    Color? blockColor = game.Board.GetBlockColor(x, y); // Assume GetBlockColor returns Color? (nullable Color)
                    if (blockColor.HasValue)
                    {
                        Brush brush = new SolidBrush(blockColor.Value);
                        DrawBlock(graphics, x, y, brush);
                    }
                }
            }
        }

        private void DrawCurrentShape(Graphics graphics)
        {
            // Only draw the current shape if the game is not over
            if (!game.IsGameOver)
            {
                var shapeColor = game.CurrentShape.ShapeColor;
                Brush shapeBrush = new SolidBrush(shapeColor);

                foreach (var block in game.CurrentShape.Blocks)
                {
                    DrawBlock(graphics, block.X, block.Y, shapeBrush);
                }
            }
        }

        private void DrawBlock(Graphics graphics, int x, int y, Brush brush)
        {
            int drawX = gameAreaStart.X + x * blockSize;
            int drawY = gameAreaStart.Y + y * blockSize;
            graphics.FillRectangle(brush, drawX, drawY, blockSize, blockSize);
        }
    }
}
