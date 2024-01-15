using System.Drawing;
using System.Windows.Forms;

namespace FormTetris
{
    public class FormViewManager
    {
        private Form form;
        private Game game;
        private UIInitializer uiInitializer;

        public FormViewManager(Form form, Game game)
        {
            this.form = form;
            this.game = game;
            uiInitializer = new UIInitializer(form);
            new UIEventHandlers(uiInitializer, this);


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

        public void TogglePause()
        {
            if (game.IsRunning)
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
        }
        public bool IsPausePanelVisible()
        {
            return uiInitializer.PausePanel.Visible;
        }

    }
}
