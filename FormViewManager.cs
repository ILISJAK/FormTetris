using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FormTetris
{
    public class FormViewManager
    {
        private Form form;
        private Panel mainMenuPanel;
        private Panel helpPanel;
        private Panel optionsPanel;
        private Panel pausePanel;
        private Game game;

        private Size buttonSize = new Size(200, 40); // Width: 200px, Height: 40px
        private Font buttonFont = new Font("Consolas", 12, FontStyle.Bold); // Example: Arial, 12pt, Bold
        private Color textColor = Color.White;

        public FormViewManager(Form form, Game game)
        {
            this.form = form;
            this.game = game;
            InitializePanels();
            DebugForm.Instance.Log("FormViewManager initialized.");
        }

        private void InitializePanels()
        {
            // Instantiate and set up properties for panels
            mainMenuPanel = new Panel { Dock = DockStyle.Fill, Visible = false };
            helpPanel = new Panel { Dock = DockStyle.Fill, Visible = false };
            optionsPanel = new Panel { Dock = DockStyle.Fill, Visible = false };

            // Add panels to form. Initially, all are invisible except for main menu.
            form.Controls.Add(mainMenuPanel);
            form.Controls.Add(helpPanel);
            form.Controls.Add(optionsPanel);

            SetupMainMenu();
            SetupHelpPanel();
            SetupPausePanel();

            // Initially, show the main menu
            ShowMainMenu();
        }

        public void TogglePause()
        {
            // If the game is currently running, pause it
            if (game.IsRunning)
            {
                game.Pause();
                pausePanel.Visible = true; // Show the pause panel
                pausePanel.Focus();        // Optional: Set focus to the pause panel
                DebugForm.Instance.Log("Game paused.");
            }
            // If the game is currently paused, resume it
            else if (!game.IsRunning && pausePanel.Visible)
            {
                game.Resume();
                pausePanel.Visible = false; // Hide the pause panel
                form.Focus();               // Set focus back to the main form
                DebugForm.Instance.Log("Game resumed.");
            }
        }


        public bool IsPausePanelVisible()
        {
            return pausePanel.Visible;
        }

        public void ShowMainMenu()
        {
            mainMenuPanel.Visible = true;
            mainMenuPanel.BringToFront(); // Ensure main menu panel is in front
            pausePanel.Visible = false; // Hide the pause panel
            game.Pause();
            DebugForm.Instance.Log("Main menu shown.");
        }

        public void ShowGameScreen()
        {
            mainMenuPanel.Visible = false;
            helpPanel.Visible = false;
            optionsPanel.Visible = false;
            game.Resume();
        }

        public void ShowHelpScreen()
        {
            mainMenuPanel.Visible = false;
            optionsPanel.Visible = false;
            helpPanel.Visible = true;
            game.Pause();
        }

        public void ShowOptionsScreen()
        {
            mainMenuPanel.Visible = false;
            helpPanel.Visible = false;
            optionsPanel.Visible = true;
            game.Pause();
        }

        // Call this method when you want to start a new game
        public void StartNewGame()
        {
            TogglePause();
            game.Reset();
            ShowGameScreen();
        }

        // You could call this method in response to a Game Over situation
        public void EndGame()
        {
            ShowMainMenu();
        }

        private void SetupMainMenu()
        {
            // Clear existing controls in the main menu panel
            mainMenuPanel.Controls.Clear();

            // Create a FlowLayoutPanel for automatic layout of buttons
            FlowLayoutPanel flowLayout = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(0) // Adjust padding as needed
            };

            var textBanner = new Label
            {
                Text = "FormTetris",
                Font = new Font("Consolas", 24, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
            };

            // Set the text banner's width and center it within the FlowLayoutPanel
            textBanner.Width = flowLayout.Width;
            textBanner.Margin = new Padding(0, 0, 0, 20); // Add some space below the banner

            // Add the title banner to the flowLayout
            flowLayout.Controls.Add(textBanner);

            // Create and add buttons to the flowLayout
            var startButton = CreateButton("Start Game");
            var optionsButton = CreateButton("Options");
            var helpButton = CreateButton("Help");
            var exitButton = CreateButton("Exit");

            // Assign click event handlers
            startButton.Click += (sender, e) => StartNewGame();
            optionsButton.Click += (sender, e) => ShowOptionsScreen();
            helpButton.Click += (sender, e) => ShowHelpScreen();
            exitButton.Click += (sender, e) => form.Close();

            // Add buttons to the flowLayout
            flowLayout.Controls.Add(startButton);
            flowLayout.Controls.Add(optionsButton);
            flowLayout.Controls.Add(helpButton);
            flowLayout.Controls.Add(exitButton);

            // Add the flowLayout to the main menu panel
            mainMenuPanel.Controls.Add(flowLayout);

            // Center the flowLayout within mainMenuPanel
            CenterFlowLayoutInParent();
        }

        public void CenterFlowLayoutInParent()
        {
            // Assuming there's only one FlowLayoutPanel in mainMenuPanel.Controls
            var flowLayout = mainMenuPanel.Controls.OfType<FlowLayoutPanel>().FirstOrDefault();
            if (flowLayout != null)
            {
                flowLayout.Left = (mainMenuPanel.Width - flowLayout.PreferredSize.Width) / 2;
                flowLayout.Top = (mainMenuPanel.Height - flowLayout.PreferredSize.Height) / 2;
            }
        }

        // This method should be called from the form's Resize event
        public void UpdateLayout(Size newSize)
        {
            mainMenuPanel.Size = newSize;
            CenterFlowLayoutInParent();
        }

        // Helper method to create a button with common properties
        private Button CreateButton(string text)
        {
            return new Button
            {
                Text = text,
                Size = buttonSize, // Assuming buttonSize is a member variable
                ForeColor = textColor, // Assuming textColor is a member variable
                Font = buttonFont, // Assuming buttonFont is a member variable
                Margin = new Padding(0, 0, 0, 10) // Margin for spacing between buttons
            };
        }


        private void SetupHelpPanel()
        {
            // Clear existing controls in the help panel
            helpPanel.Controls.Clear();

            // Create and configure the text for the keybinds
            var keybindsText = new TextBox();
            keybindsText.Multiline = true;
            keybindsText.Text = "Keybinds:\r\n" +
                               "Left Arrow: Move Left\r\n" +
                               "Right Arrow: Move Right\r\n" +
                               "Down Arrow: Drop Shape\r\n" +
                               "Q: Rotate Shape Counter-clockwise\r\n" +
                               "E: Rotate Shape Clockwise";
            keybindsText.Font = buttonFont; // Use the same font as buttons
            keybindsText.ReadOnly = true;
            keybindsText.Size = new Size(400, 200);
            keybindsText.Location = new Point((form.ClientSize.Width - keybindsText.Width) / 2, 100);

            // Create a button to return to the main menu
            var backButton = new Button { Text = "Back to Main Menu", Size = buttonSize, ForeColor = textColor, Font = buttonFont };
            backButton.Location = new Point((form.ClientSize.Width - buttonSize.Width) / 2, keybindsText.Bottom + 20);
            backButton.Click += (sender, e) => ShowMainMenu(); // Return to the main menu when clicked

            // Add controls to the help panel
            helpPanel.Controls.Add(keybindsText);
            helpPanel.Controls.Add(backButton);
        }
        private void SetupPausePanel()
        {
            pausePanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(128, 0, 0, 0), // Semi-transparent black
                Visible = false
            };

            // Create and configure buttons for the pause panel
            var resumeButton = new Button { Text = "Resume", Size = buttonSize, ForeColor = textColor, Font = buttonFont };
            resumeButton.Location = new Point((form.ClientSize.Width - buttonSize.Width) / 2, 100);
            resumeButton.Click += (sender, e) =>
            {
                DebugForm.Instance.Log("Resume button clicked.");
                TogglePause();
            };

            var restartButton = new Button { Text = "Restart", Size = buttonSize, ForeColor = textColor, Font = buttonFont };
            restartButton.Location = new Point((form.ClientSize.Width - buttonSize.Width) / 2, resumeButton.Bottom + 10);
            restartButton.Click += (sender, e) => StartNewGame();

            var quitButton = new Button { Text = "Quit to Main Menu", Size = buttonSize, ForeColor = textColor, Font = buttonFont };
            quitButton.Location = new Point((form.ClientSize.Width - buttonSize.Width) / 2, restartButton.Bottom + 10);
            quitButton.Click += (sender, e) =>
            {
                DebugForm.Instance.Log("Quit to Main Menu button clicked.");
                ShowMainMenu(); // Directly call ShowMainMenu
            };

            // Add buttons to the pause panel
            pausePanel.Controls.Add(resumeButton);
            pausePanel.Controls.Add(restartButton);
            pausePanel.Controls.Add(quitButton);

            // Add the pause panel to the form
            form.Controls.Add(pausePanel);
            form.Controls.SetChildIndex(pausePanel, 0); // Ensure pause panel is on top
        }
    }
}
