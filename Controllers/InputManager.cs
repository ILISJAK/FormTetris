using System;
using System.Windows.Forms;

namespace FormTetris
{
    public class InputManager
    {
        private Game game;
        private Action invalidate;
        private bool isLeftKeyPressed;
        private bool isRightKeyPressed;
        private bool isDownKeyPressed;
        private Timer actionTimer;

        public InputManager(Game game, Action invalidate)
        {
            this.game = game;
            this.invalidate = invalidate;
            InitializeActionTimer();
        }

        private void InitializeActionTimer()
        {
            actionTimer = new Timer();
            actionTimer.Interval = 50; // Adjust for desired responsiveness
            actionTimer.Tick += ActionTimer_Tick;
        }

        private void ActionTimer_Tick(object sender, EventArgs e)
        {
            if (isLeftKeyPressed)
            {
                game.MoveShapeLeft();
            }

            if (isRightKeyPressed)
            {
                game.MoveShapeRight();
            }

            if (isDownKeyPressed)
            {
                game.DropShape();
            }

            invalidate();
        }

        public void HandleKeyDown(KeyEventArgs e)
        {
            if (game.IsGameOver || !game.IsRunning)
            {
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.Left:
                    isLeftKeyPressed = true;
                    break;
                case Keys.Right:
                    isRightKeyPressed = true;
                    break;
                case Keys.Down:
                    isDownKeyPressed = true;
                    break;
                case Keys.Q:
                    game.RotateShape(false);
                    break;
                case Keys.E:
                    game.RotateShape(true);
                    break;
                case Keys.Space:
                    game.FastDrop();
                    break;
            }

            actionTimer.Start();
            invalidate();
        }

        public void HandleKeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    isLeftKeyPressed = false;
                    break;
                case Keys.Right:
                    isRightKeyPressed = false;
                    break;
                case Keys.Down:
                    isDownKeyPressed = false;
                    break;
            }

            if (!isLeftKeyPressed && !isRightKeyPressed && !isDownKeyPressed)
            {
                actionTimer.Stop();
            }
        }
        public void HandleSpacebarPress()
        {
            if (game.IsRunning && !game.IsGameOver)
            {
                game.FastDrop();
                invalidate();
            }
        }
    }
}
