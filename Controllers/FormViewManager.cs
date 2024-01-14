using System.Drawing;
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
            SetupOptionsPanel();

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
            CenterFlowLayoutInParent(mainMenuPanel);
        }

        public void CenterFlowLayoutInParent(Panel panel)
        {
            foreach (var control in panel.Controls)
            {
                if (control is FlowLayoutPanel flowLayout)
                {
                    // Calculate the preferred size based on its contents
                    var preferredSize = flowLayout.GetPreferredSize(new Size(panel.Width, panel.Height));

                    // Set the size explicitly instead of relying on AutoSize
                    flowLayout.Size = preferredSize;

                    // Center the flowLayout within its parent panel
                    flowLayout.Left = (panel.ClientSize.Width - preferredSize.Width) / 2;
                    flowLayout.Top = (panel.ClientSize.Height - preferredSize.Height) / 2;
                }
            }
        }

        // This method should be called from the form's Resize event
        public void UpdateLayout(Size newSize)
        {
            mainMenuPanel.Size = newSize;
            helpPanel.Size = newSize;
            pausePanel.Size = newSize;
            optionsPanel.Size = newSize;

            CenterFlowLayoutInParent(mainMenuPanel);
            CenterFlowLayoutInParent(helpPanel);
            CenterFlowLayoutInParent(pausePanel);
            CenterFlowLayoutInParent(optionsPanel);
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
            helpPanel.Controls.Clear();

            FlowLayoutPanel helpLayout = new FlowLayoutPanel
            {
                // Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            var controlsLabel = new Label
            {
                Text = "Controls",
                TextAlign = ContentAlignment.TopCenter,
                Font = buttonFont,
                ForeColor = textColor,
                AutoSize = true
            };

            var keybindsTextBox = new TextBox
            {
                Multiline = true,
                Text = "←: Move Left\r\n→: Move Right\r\n" +
                       "↓: Drop Shape\r\nSPACE: Fast Drop\r\nQ: Rotate Counter-clockwise\r\n" +
                       "E: Rotate Clockwise\r\nF11: Toggle Fullscreen\r\nF12: Debug Window",
                ReadOnly = true,
                Size = new Size(300, 300), // Set the size as per your requirement
                Font = buttonFont,
                ForeColor = textColor,
                AutoSize = true, // Set AutoSize to false
                ScrollBars = ScrollBars.Vertical // Add vertical scrollbars if necessary
            };

            var backButton = CreateButton("Back to Main Menu");
            backButton.Click += (sender, e) => ShowMainMenu();

            helpLayout.Controls.Add(controlsLabel);
            helpLayout.Controls.Add(keybindsTextBox);
            helpLayout.Controls.Add(backButton);

            helpPanel.Controls.Add(helpLayout);
            CenterFlowLayoutInParent(helpPanel); // Center the layout in the panel
        }

        private void SetupPausePanel()
        {
            if (pausePanel == null)
            {
                pausePanel = new Panel
                {
                    // Dock = DockStyle.Fill,
                    BackColor = Color.FromArgb(128, 0, 0, 0), // Semi-transparent black
                    Visible = false
                };
                form.Controls.Add(pausePanel); // Ensure pausePanel is added to the form
            }
            else
            {
                pausePanel.Controls.Clear();
            }

            FlowLayoutPanel pauseLayout = new FlowLayoutPanel
            {
                // Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            var resumeButton = CreateButton("Resume");
            resumeButton.Click += (sender, e) =>
            {
                DebugForm.Instance.Log("Resume button clicked.");
                TogglePause();
            };

            var restartButton = CreateButton("Restart");
            restartButton.Click += (sender, e) => StartNewGame();

            var quitButton = CreateButton("Quit to Main Menu");
            quitButton.Click += (sender, e) =>
            {
                DebugForm.Instance.Log("Quit to Main Menu button clicked.");
                ShowMainMenu();
            };

            pauseLayout.Controls.Add(resumeButton);
            pauseLayout.Controls.Add(restartButton);
            pauseLayout.Controls.Add(quitButton);

            pausePanel.Controls.Add(pauseLayout);
            CenterFlowLayoutInParent(pausePanel); // Center the layout in the panel
        }

        private void SetupOptionsPanel()
        {
            optionsPanel.Controls.Clear();

            FlowLayoutPanel optionsLayout = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(0) // Adjust padding as needed
            };

            // Create the back button and add it to the layout
            var backButton = CreateButton("Back to Main Menu");
            backButton.Click += (sender, e) => ShowMainMenu();

            optionsLayout.Controls.Add(backButton);

            // Add the optionsLayout to the options panel
            optionsPanel.Controls.Add(optionsLayout);

            // Center the optionsLayout within the optionsPanel
            CenterFlowLayoutInParent(optionsPanel);
        }

    }
}
