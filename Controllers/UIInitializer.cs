using System.Drawing;
using System.Windows.Forms;

namespace FormTetris
{
    public class UIInitializer
    {
        private Form form;
        private UIElementFactory elementFactory;

        public Panel MainMenuPanel { get; private set; }
        public Panel HelpPanel { get; private set; }
        public Panel OptionsPanel { get; private set; }
        public Panel PausePanel { get; private set; }

        public UIInitializer(Form form)
        {
            this.form = form;
            this.elementFactory = new UIElementFactory();
            InitializePanels();
        }

        private void InitializePanels()
        {
            MainMenuPanel = CreatePanel();
            HelpPanel = CreatePanel();
            OptionsPanel = CreatePanel();
            PausePanel = CreatePanel();

            SetupMainMenu();
            SetupHelpPanel();
            SetupOptionsPanel();
            SetupPausePanel();
        }

        private Panel CreatePanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Fill,
                Visible = false
            };
            form.Controls.Add(panel);
            return panel;
        }

        private void SetupMainMenu()
        {
            FlowLayoutPanel flowLayout = CreateFlowLayout();

            var title = elementFactory.CreateLabel("FormTetris", new Font("Consolas", 24, FontStyle.Bold), ContentAlignment.MiddleCenter);
            title.Margin = new Padding(0, 0, 0, 20);
            flowLayout.Controls.Add(title);

            var startButton = elementFactory.CreateButton("Start Game");
            var optionsButton = elementFactory.CreateButton("Options");
            var helpButton = elementFactory.CreateButton("Help");
            var exitButton = elementFactory.CreateButton("Exit");
            // Note: Assign event handlers for these buttons in FormViewManager

            flowLayout.Controls.Add(startButton);
            flowLayout.Controls.Add(optionsButton);
            flowLayout.Controls.Add(helpButton);
            flowLayout.Controls.Add(exitButton);

            MainMenuPanel.Controls.Add(flowLayout);
            CenterFlowLayoutInParent(MainMenuPanel);
        }

        private void SetupHelpPanel()
        {
            FlowLayoutPanel flowLayout = CreateFlowLayout();

            var controlsLabel = elementFactory.CreateLabel("Controls");
            var keybindsTextBox = elementFactory.CreateTextBox(
                "←: Move Left\r\n→: Move Right\r\n↓: Drop Shape\r\nSPACE: Fast Drop\r\nQ: Rotate Counter-clockwise\r\n" +
                "E: Rotate Clockwise\r\nF11: Toggle Fullscreen\r\nF12: Debug Window",
                true, true
            );
            var backButton = elementFactory.CreateButton("Back to Main Menu");
            // Note: Assign event handler for backButton in FormViewManager

            flowLayout.Controls.Add(controlsLabel);
            flowLayout.Controls.Add(keybindsTextBox);
            flowLayout.Controls.Add(backButton);

            HelpPanel.Controls.Add(flowLayout);
            CenterFlowLayoutInParent(HelpPanel);
        }

        private void SetupOptionsPanel()
        {
            FlowLayoutPanel flowLayout = CreateFlowLayout();

            var backButton = elementFactory.CreateButton("Back to Main Menu");
            // Note: Assign event handler for backButton in FormViewManager

            flowLayout.Controls.Add(backButton);

            OptionsPanel.Controls.Add(flowLayout);
            CenterFlowLayoutInParent(OptionsPanel);
        }

        private void SetupPausePanel()
        {
            FlowLayoutPanel flowLayout = CreateFlowLayout();
            PausePanel.BackColor = Color.FromArgb(128, 0, 0, 0); // Semi-transparent black

            var resumeButton = elementFactory.CreateButton("Resume");
            var restartButton = elementFactory.CreateButton("Restart");
            var quitButton = elementFactory.CreateButton("Quit to Main Menu");
            // Note: Assign event handlers for these buttons in FormViewManager

            flowLayout.Controls.Add(resumeButton);
            flowLayout.Controls.Add(restartButton);
            flowLayout.Controls.Add(quitButton);
            PausePanel.Controls.Add(flowLayout);
            CenterFlowLayoutInParent(PausePanel);
        }

        private FlowLayoutPanel CreateFlowLayout()
        {
            return new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(0)
            };
        }

        private void CenterFlowLayoutInParent(Panel panel)
        {
            foreach (Control control in panel.Controls)
            {
                if (control is FlowLayoutPanel flowLayout)
                {
                    flowLayout.Left = (panel.ClientSize.Width - flowLayout.PreferredSize.Width) / 2;
                    flowLayout.Top = (panel.ClientSize.Height - flowLayout.PreferredSize.Height) / 2;
                }
            }
        }
        public void UpdateLayout(Size newSize)
        {
            // Update the size of each panel
            MainMenuPanel.Size = newSize;
            HelpPanel.Size = newSize;
            OptionsPanel.Size = newSize;
            PausePanel.Size = newSize;

            // Additionally, update the layout of any contained FlowLayoutPanels or other controls
            foreach (var panel in new[] { MainMenuPanel, HelpPanel, OptionsPanel, PausePanel })
            {
                foreach (Control control in panel.Controls)
                {
                    if (control is FlowLayoutPanel flowLayout)
                    {
                        flowLayout.Size = newSize;
                        // Adjust control's layout or position if needed
                        // For example, centering the FlowLayoutPanel within the panel
                        CenterFlowLayoutInParent(panel);
                    }
                }
            }
        }
    }
}