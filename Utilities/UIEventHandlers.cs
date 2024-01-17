using System.Linq;
using System.Windows.Forms;

namespace FormTetris
{
    public class UIEventHandlers
    {
        private UIInitializer uiInitializer;
        private FormViewManager viewManager;
        private bool scoreSubmitted = false;

        public bool ScoreSubmitted => scoreSubmitted;

        public UIEventHandlers(UIInitializer uiInitializer, FormViewManager viewManager)
        {
            this.uiInitializer = uiInitializer;
            this.viewManager = viewManager;

            SetupMainMenuHandlers();
            SetupHelpPanelHandlers();
            SetupOptionsPanelHandlers();
            SetupPausePanelHandlers();
            SetupEndGamePanelHandlers();
            SetupSubmitScorePanelHandlers();
        }

        private void SetupMainMenuHandlers()
        {
            if (uiInitializer.MainMenuPanel.Controls.Count > 0 &&
                uiInitializer.MainMenuPanel.Controls[0] is Control mainMenuControl &&
                mainMenuControl.Controls.Count >= 5) // 1 label + 4 buttons
            {
                // Adjust the indices to start from 1
                if (mainMenuControl.Controls[1] is Button startButton)
                    startButton.Click += (sender, e) => viewManager.StartNewGame();
                if (mainMenuControl.Controls[2] is Button optionsButton)
                    optionsButton.Click += (sender, e) => viewManager.ShowOptionsScreen();
                if (mainMenuControl.Controls[3] is Button helpButton)
                    helpButton.Click += (sender, e) => viewManager.ShowHelpScreen();
                if (mainMenuControl.Controls[4] is Button exitButton)
                    exitButton.Click += (sender, e) => viewManager.ExitGame();
            }
            DebugForm.Instance.Log("MainMenu handlers set up.");
        }

        private void SetupHelpPanelHandlers()
        {
            var backButton = uiInitializer.HelpPanel.Controls[0].Controls[2] as Button;
            backButton.Click += (sender, e) => viewManager.ShowMainMenu();
            DebugForm.Instance.Log("HelpPanel handlers set up.");
        }

        private void SetupOptionsPanelHandlers()
        {
            var backButton = uiInitializer.OptionsPanel.Controls[0].Controls[0] as Button;
            backButton.Click += (sender, e) => viewManager.ShowMainMenu();
            DebugForm.Instance.Log("OptionsPanel handlers set up.");
        }

        private void SetupPausePanelHandlers()
        {
            var pausePanelButtons = uiInitializer.PausePanel.Controls[0].Controls;
            (pausePanelButtons[0] as Button).Click += (sender, e) => viewManager.TogglePause();
            (pausePanelButtons[1] as Button).Click += (sender, e) => viewManager.StartNewGame();
            (pausePanelButtons[2] as Button).Click += (sender, e) => viewManager.ShowMainMenu();
            DebugForm.Instance.Log("PausePanel handlers set up.");
        }
        public void SetupEndGamePanelHandlers()
        {
            var submitButton = uiInitializer.EndGamePanel.Controls.Find("submitButton", true).FirstOrDefault() as Button;
            if (submitButton != null)
                submitButton.Click += (sender, e) => viewManager.ShowSubmitScorePanel();

            var restartButton = uiInitializer.EndGamePanel.Controls.Find("restartButton", true).FirstOrDefault() as Button;
            if (restartButton != null)
                restartButton.Click += (sender, e) => viewManager.StartNewGame();

            var returnButton = uiInitializer.EndGamePanel.Controls.Find("returnButton", true).FirstOrDefault() as Button;
            if (returnButton != null)
                returnButton.Click += (sender, e) => viewManager.ShowMainMenu();

            DebugForm.Instance.Log("EndGamePanel handlers set up.");
        }

        public void SetupSubmitScorePanelHandlers()
        {
            var submitScorePanelControls = uiInitializer.SubmitScorePanel.Controls[0].Controls;

            // Assuming the order is: [Label, TextBox, OK Button, Cancel Button]
            var okButton = submitScorePanelControls[2] as Button;
            var cancelButton = submitScorePanelControls[3] as Button;
            var playerNameTextBox = submitScorePanelControls[1] as TextBox;

            okButton.Click += (sender, e) => SubmitScore(playerNameTextBox.Text);
            cancelButton.Click += (sender, e) => viewManager.HideSubmitScorePanel();
        }

        private void SubmitScore(string playerName)
        {
            if (string.IsNullOrWhiteSpace(playerName))
            {
                MessageBox.Show("Please enter your name.", "Name Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (playerName.Length > 3)
            {
                MessageBox.Show("Name must be 3 characters or less.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ScoreManager.Instance.SaveScore(playerName.ToUpper());
            scoreSubmitted = true;
            viewManager.ShowEndGameScreen();
            viewManager.PopulateLeaderboard();
        }
    }
}