using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FormTetris
{
    public class FormViewManager
    {
        private Form form;
        private Game game;
        private UIInitializer uiInitializer;
        private UIEventHandlers uiEventHandlers;

        public FormViewManager(Form form, Game game)
        {
            this.form = form;
            this.game = game;
            uiInitializer = new UIInitializer(form);
            uiEventHandlers = new UIEventHandlers(uiInitializer, this);
            game.GameOver += OnGameOver;

            DebugForm.Instance.Log("FormViewManager initialized.");
        }

        public void UpdateLayout(Size newSize)
        {
            uiInitializer.UpdateLayout(newSize);
        }

        public void ShowMainMenu()
        {
            SetPanelVisibility(uiInitializer.MainMenuPanel);
            game.Pause();
        }

        public void ShowGameScreen()
        {
            SetAllPanelsInvisible();
            game.Resume();
        }

        public void ShowHelpScreen()
        {
            SetPanelVisibility(uiInitializer.HelpPanel);
            game.Pause();
        }

        public void ShowOptionsScreen()
        {
            SetPanelVisibility(uiInitializer.OptionsPanel);
            game.Pause();
        }

        public void ShowEndGameScreen()
        {
            var submitButton = uiInitializer.EndGamePanel.Controls.Find("submitButton", true).FirstOrDefault() as Button;
            if (submitButton != null)
            {
                submitButton.Visible = !uiEventHandlers.ScoreSubmitted;
            }
            SetPanelVisibility(uiInitializer.EndGamePanel);
            game.Pause();
        }
        public void ShowSubmitScorePanel()
        {
            SetPanelVisibility(uiInitializer.SubmitScorePanel);
        }
        public void HideSubmitScorePanel()
        {
            uiInitializer.SubmitScorePanel.Visible = false;
            ShowEndGameScreen();
        }

        public void TogglePause()
        {
            if (game.IsRunning && !game.IsGameOver)
            {
                SetPanelVisibility(uiInitializer.PausePanel);
                game.Pause();
            }
            else
            {
                ShowGameScreen();
            }
        }

        public void StartNewGame()
        {
            game.Reset();
            game.Start();
            ShowGameScreen();
        }

        public void ExitGame()
        {
            form.Close();
        }

        private void SetPanelVisibility(Panel panelToShow)
        {
            uiInitializer.MainMenuPanel.Visible = panelToShow == uiInitializer.MainMenuPanel;
            uiInitializer.HelpPanel.Visible = panelToShow == uiInitializer.HelpPanel;
            uiInitializer.OptionsPanel.Visible = panelToShow == uiInitializer.OptionsPanel;
            uiInitializer.PausePanel.Visible = panelToShow == uiInitializer.PausePanel;
            uiInitializer.EndGamePanel.Visible = panelToShow == uiInitializer.EndGamePanel;
            uiInitializer.SubmitScorePanel.Visible = panelToShow == uiInitializer.SubmitScorePanel;

            if (panelToShow != null)
            {
                panelToShow.BringToFront();
            }
        }

        private void SetAllPanelsInvisible()
        {
            uiInitializer.MainMenuPanel.Visible = false;
            uiInitializer.HelpPanel.Visible = false;
            uiInitializer.OptionsPanel.Visible = false;
            uiInitializer.PausePanel.Visible = false;
            uiInitializer.EndGamePanel.Visible = false;
            uiInitializer.SubmitScorePanel.Visible = false;
        }
        public bool IsPausePanelVisible()
        {
            return uiInitializer.PausePanel.Visible;
        }
        public void PopulateLeaderboard()
        {
            uiInitializer.PopulateLeaderboard();
        }
        private void UpdateEndGameStats()
        {
            ScoreManager scoreManager = ScoreManager.Instance;
            Debug.WriteLine($"Updating end game stats: Total Score: {scoreManager.TotalScore}, Game Time: {scoreManager.GameTime}, Lines Cleared: {scoreManager.LinesCleared}, Level: {scoreManager.Level}, Tetrises: {scoreManager.Tetrises}, T-Spins: {scoreManager.TSpins}, Combos: {scoreManager.Combos}, TPM: {scoreManager.TPM}, LPM: {scoreManager.LPM}");
            uiInitializer.UpdateEndGameStats(scoreManager);
        }

        private void OnGameOver(object sender, EventArgs e)
        {
            Debug.WriteLine("Game over event triggered.");
            form.Invoke(new Action(() =>
            {
                UpdateEndGameStats();
                ShowEndGameScreen();
            }));
        }

    }
}
