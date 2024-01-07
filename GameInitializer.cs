using FormTetris;
using System;
using System.Drawing;
using System.Windows.Forms;

public class GameInitializer
{
    public Game Game { get; private set; }
    public Size DefaultSize { get; private set; }
    private Timer logicTimer;
    private Timer renderTimer;
    private Action invalidateAction;

    public GameInitializer(Size defaultSize, Action invalidateAction)
    {
        DefaultSize = defaultSize;
        this.invalidateAction = invalidateAction;
        Game = new Game();
        Game.Start();
        InitializeTimers();
    }

    private void InitializeTimers()
    {
        logicTimer = new Timer { Interval = 1000 };
        logicTimer.Tick += (sender, e) => Game.Update();
        logicTimer.Start();

        renderTimer = new Timer { Interval = 1000 / 60 };
        renderTimer.Tick += (sender, e) => invalidateAction();
        renderTimer.Start();
    }

    public void Initialize(out GameRenderer renderer)
    {
        renderer = new GameRenderer(Game, DefaultSize.Height / Game.Board.BoardHeight, new Point(0, 0));
    }

    public void UpdateFormSize(Size newSize)
    {
        DefaultSize = newSize;
        // Assuming GameRenderer has a method to update its size based on the new form size
        // renderer.UpdateSize(newSize);
        invalidateAction();
    }
}
