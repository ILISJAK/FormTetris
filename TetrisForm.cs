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
        private Point gameAreaStart;

        private bool isFullScreen = false;
        private Size defaultSize = new Size(800, 600);

        public TetrisForm()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void SetAspectRatio()
        {
            this.ClientSize = defaultSize;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            ConfigureForm();
        }

        private void ToggleFullScreen()
        {
            if (!isFullScreen)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = FormBorderStyle.None;
                this.Bounds = Screen.PrimaryScreen.Bounds;
                isFullScreen = true;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.WindowState = FormWindowState.Normal;
                this.ClientSize = defaultSize;
                ConfigureForm();
                isFullScreen = false;
            }
        }

        private void ConfigureForm()
        {
            // Calculation for the centered game area position
            int gameAreaWidth = game.Board.BoardWidth * BlockSize;
            int gameAreaHeight = game.Board.BoardHeight * BlockSize;

            // Calculate the starting point to center the game area in the window
            // Adjust these calculations based on your desired layout and additional side information
            int sidePadding = (this.ClientSize.Width - gameAreaWidth) / 2;
            int topBottomPadding = (this.ClientSize.Height - gameAreaHeight) / 2;

            gameAreaStart = new Point(sidePadding, topBottomPadding);

            // Additional UI setup, if necessary
            // For example, setting up labels or panels for score display on the sides
            // ...

            this.BackColor = Color.Black; // Set background color; adjust as desired
        }


        private void InitializeGame()
        {
            game = new Game();
            game.Start();
            ConfigureForm();

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
            DrawBoardOutline(graphics);
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

        private void DrawBoardOutline(Graphics graphics)
        {
            // Calculate the size of the outline based on the game area size
            int outlineWidth = game.Board.BoardWidth * BlockSize;
            int outlineHeight = game.Board.BoardHeight * BlockSize;

            // Offset the outline position by half a block size to encompass the entire game area
            Point outlineStart = new Point(gameAreaStart.X - BlockSize / 2, gameAreaStart.Y - BlockSize / 2);

            // Draw the outline
            Pen outlinePen = new Pen(Color.White, 2); // 2 is the thickness of the outline; adjust as needed
            graphics.DrawRectangle(outlinePen, outlineStart.X, outlineStart.Y, outlineWidth, outlineHeight);
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

            if (e.KeyCode == Keys.F11)
            {
                ToggleFullScreen();
                return;
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
