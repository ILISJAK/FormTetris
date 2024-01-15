using FormTetris;
using System;
using System.Drawing;
using System.Windows.Forms;

public class GameInitializer
{
    public Game Game { get; private set; }
    public Size DefaultSize { get; private set; }
    private Timer renderTimer;
    private Action invalidateAction;

    public GameInitializer(Size defaultSize, Action invalidateAction)
    {
        DefaultSize = defaultSize;
        this.invalidateAction = invalidateAction;
        Game = new Game();
        InitializeRenderTimer();
    }

    private void InitializeRenderTimer()
    {
        renderTimer = new Timer { Interval = 1000 / 60 }; // 60 FPS for rendering
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
        invalidateAction();
    }
}
