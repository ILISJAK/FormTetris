using System;
using System.Windows.Forms;

namespace FormTetris
{
    public class InputManager
    {
        private Game game;
        private Action invalidate;

        public InputManager(Game game, Action invalidate)
        {
            this.game = game;
            this.invalidate = invalidate;
        }

        public void HandleKeyDown(KeyEventArgs e)
        {
            if (game.IsGameOver)
            {
                return; // Don't handle input if the game is over
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
                    // game.RotateShape(true); // Uncomment or implement if rotation is needed
                    break;
                case Keys.Down:
                    game.MoveShapeDown();
                    break;
                case Keys.Q:
                    game.RotateShape(false);
                    break;
                case Keys.E:
                    game.RotateShape(true);
                    break;
            }

            invalidate(); // Request to redraw the form
        }
    }

}
