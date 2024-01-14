using System;
using System.Drawing;
using System.Windows.Forms;

namespace FormTetris
{
    public partial class DebugForm : Form
    {
        private static DebugForm instance;
        private RichTextBox logBox;

        private DebugForm()
        {
            InitializeComponents();
        }

        public static DebugForm Instance
        {
            get
            {
                if (instance == null || instance.IsDisposed)
                {
                    instance = new DebugForm();
                }
                return instance;
            }
        }

        private void InitializeComponents()
        {
            this.logBox = new RichTextBox();
            this.logBox.Dock = DockStyle.Fill;
            this.logBox.BackColor = Color.Black;
            this.logBox.ForeColor = Color.White;
            this.logBox.BorderStyle = BorderStyle.FixedSingle;
            logBox.Font = new Font("Consolas", 14);
            this.Controls.Add(this.logBox);

            this.Text = "Debug Log";
            this.Size = new Size(600, 400);
            this.BackColor = Color.Black;
        }


        public void Log(string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => Log(message)));
            }
            else
            {
                logBox.AppendText(message + Environment.NewLine);
                // Scroll to the caret (latest text).
                logBox.SelectionStart = logBox.Text.Length;
                logBox.ScrollToCaret();
            }
        }
    }
}
