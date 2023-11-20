using System;
using System.Drawing;
using System.Windows.Forms;

namespace FormTetris
{
    public partial class TetrisForm : Form
    {
        private Game game;
        private Timer gameTimer;
        private int BlockSize = 20; // Size of each block in pixels
        private Point gameAreaStart;

        private bool isFullScreen = false;
        private Size defaultSize = new Size(800, 600); // Default window size

        public TetrisForm()
        {
            InitializeComponent();
            InitializeGame();
            SetAspectRatio();
        }

        private void SetAspectRatio()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Prevents resizing
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ClientSize = defaultSize;
            ConfigureForm();
        }

        private void ToggleFullScreen()
        {
            if (!isFullScreen)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                isFullScreen = true;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.WindowState = FormWindowState.Normal;
                this.ClientSize = defaultSize;
                isFullScreen = false;
            }
            ConfigureForm(); // Recalculate game area start
        }

        private void ConfigureForm()
        {
            // Adjust BlockSize based on the height of the window
            BlockSize = this.ClientSize.Height / game.Board.BoardHeight;

            // Recalculate gameAreaWidth and gameAreaHeight based on the new BlockSize
            int gameAreaWidth = game.Board.BoardWidth * BlockSize;
            int gameAreaHeight = game.Board.BoardHeight * BlockSize;

            // Center the game area both horizontally and vertically
            gameAreaStart = new Point((this.ClientSize.Width - gameAreaWidth) / 2,
                                      (this.ClientSize.Height - gameAreaHeight) / 2);

            this.BackColor = Color.Black;
        }

        private void InitializeGame()
        {
            game = new Game();
            game.Start();

            gameTimer = new Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            game.Update();
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawGame(e.Graphics);
        }

        private void DrawGame(Graphics graphics)
        {
            DrawBoardOutline(graphics);
            DrawBoard(graphics);
            DrawCurrentShape(graphics);
        }

        private void DrawBoardOutline(Graphics graphics)
        {
            int outlineWidth = game.Board.BoardWidth * BlockSize;
            int outlineHeight = game.Board.BoardHeight * BlockSize;
            Point outlineStart = gameAreaStart;

            Pen outlinePen = new Pen(Color.White, 2); // Outline thickness
            graphics.DrawRectangle(outlinePen, outlineStart.X, outlineStart.Y, outlineWidth, outlineHeight);
        }


        private void DrawBoard(Graphics graphics)
        {
            for (int x = 0; x < game.Board.BoardWidth; x++)
            {
                for (int y = 0; y < game.Board.BoardHeight; y++)
                {
                    if (game.Board.IsPositionOccupied(x, y))
                    {
                        DrawBlock(graphics, x, y, Brushes.Gray);
                    }
                }
            }
        }

        private void DrawCurrentShape(Graphics graphics)
        {
            foreach (var block in game.CurrentShape.Blocks)
            {
                DrawBlock(graphics, block.X, block.Y, Brushes.Blue);
            }
        }

        private void DrawBlock(Graphics graphics, int x, int y, Brush brush)
        {
            int drawX = gameAreaStart.X + x * BlockSize;
            int drawY = gameAreaStart.Y + y * BlockSize;
            graphics.FillRectangle(brush, drawX, drawY, BlockSize, BlockSize);
        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (game.IsGameOver)
            {
                return; // Don't handle input if the game is over
            }

            if (e.KeyCode == Keys.F11)
            {
                ToggleFullScreen();
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.Left:
                    game.MoveShapeLeft();
                    break;
                case Keys.Right:
                    game.MoveShapeRight();
                    break;
                case Keys.Up:
                    game.RotateShape();
                    break;
                case Keys.Down:
                    game.MoveShapeDown();
                    break;
                case Keys.Space:
                    game.RotateShape();
                    break;
            }

            this.Invalidate(); // Redraw the form to reflect the changes
        }

    }
}
