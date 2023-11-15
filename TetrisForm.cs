using System;
using System.Drawing;
using System.Windows.Forms;

namespace FormTetris
{
    public partial class TetrisForm : Form
    {
        private Game game;
        private Timer gameTimer;
        private const int BlockSize = 20; // Size of each block in pixels

        public TetrisForm()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            game = new Game();
            game.Start();

            gameTimer = new Timer();
            gameTimer.Interval = 1000; // Set the game update interval (in milliseconds)
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            game.Update();
            this.Invalidate(); // Triggers the Paint event to redraw the form
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawGame(e.Graphics);
        }

        private void DrawGame(Graphics graphics)
        {
            DrawBoard(graphics);
            DrawCurrentShape(graphics);
        }

        private void DrawBoard(Graphics graphics)
        {
            // Draw the board blocks
            for (int x = 0; x < game.Board.BoardWidth; x++)
            {
                for (int y = 0; y < game.Board.BoardHeight; y++)
                {
                    if (game.Board.IsPositionOccupied(x, y))
                    {
                        DrawBlock(graphics, x, y, Brushes.Gray); // Change color as needed
                    }
                }
            }
        }

        private void DrawCurrentShape(Graphics graphics)
        {
            // Draw the current shape
            foreach (var block in game.CurrentShape.Blocks)
            {
                DrawBlock(graphics, block.X, block.Y, Brushes.Blue); // Change color as needed
            }
        }

        private void DrawBlock(Graphics graphics, int x, int y, Brush brush)
        {
            graphics.FillRectangle(brush, x * BlockSize, y * BlockSize, BlockSize, BlockSize);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (game.IsGameOver)
            {
                return; // Don't handle input if the game is over
            }

            switch (e.KeyCode)
            {
                case Keys.Left:
                    game.CurrentShape.MoveLeft();
                    break;
                case Keys.Right:
                    game.CurrentShape.MoveRight();
                    break;
                case Keys.Up:
                    game.CurrentShape.Rotate();
                    break;
                case Keys.Down:
                    game.CurrentShape.MoveDown();
                    break;
            }

            this.Invalidate(); // Redraw the form to reflect the changes
        }
    }
}
