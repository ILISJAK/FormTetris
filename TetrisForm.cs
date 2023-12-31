﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace FormTetris
{
    public partial class TetrisForm : Form
    {
        private GameRenderer gameRenderer;
        private InputManager inputManager;
        private GameInitializer gameInitializer;
        private FormWindowConfiguration windowConfig;
        private FormViewManager viewManager;

        public TetrisForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.BackColor = Color.Black; // Set the background color

            gameInitializer = new GameInitializer(this.ClientSize, Invalidate);
            gameInitializer.Initialize(out gameRenderer);

            // Update game renderer with initial form size
            gameRenderer.UpdateSize(this.ClientSize);

            inputManager = new InputManager(gameInitializer.Game, Invalidate);
            this.KeyDown += OnKeyDown;

            windowConfig = new FormWindowConfiguration(this, new Size(800, 600));
            viewManager = new FormViewManager(this);

            this.Resize += TetrisForm_Resize;
            SetAspectRatio();
        }

        private void TetrisForm_Resize(object sender, EventArgs e)
        {
            gameInitializer.UpdateFormSize(this.ClientSize);
            gameRenderer.UpdateSize(this.ClientSize);
            this.Invalidate();
        }

        private void SetAspectRatio()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ClientSize = gameInitializer.DefaultSize;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
            {
                windowConfig.ToggleFullScreen();
                return;
            }

            inputManager.HandleKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            inputManager.HandleKeyUp(e); // Pass the event to the InputManager
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            gameRenderer.DrawGame(e.Graphics);
        }
    }
}
