using System.Windows.Forms;

namespace FormTetris
{
    public class UIEventHandlers
    {
        private UIInitializer uiInitializer;
        private FormViewManager viewManager;

        public UIEventHandlers(UIInitializer uiInitializer, FormViewManager viewManager)
        {
            this.uiInitializer = uiInitializer;
            this.viewManager = viewManager;

            SetupMainMenuHandlers();
            SetupHelpPanelHandlers();
            SetupOptionsPanelHandlers();
            SetupPausePanelHandlers();
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
        }

        private void SetupHelpPanelHandlers()
        {
            var backButton = uiInitializer.HelpPanel.Controls[0].Controls[2] as Button;
            backButton.Click += (sender, e) => viewManager.ShowMainMenu();
        }

        private void SetupOptionsPanelHandlers()
        {
            var backButton = uiInitializer.OptionsPanel.Controls[0].Controls[0] as Button;
            backButton.Click += (sender, e) => viewManager.ShowMainMenu();
        }

        private void SetupPausePanelHandlers()
        {
            var pausePanelButtons = uiInitializer.PausePanel.Controls[0].Controls;
            (pausePanelButtons[0] as Button).Click += (sender, e) => viewManager.TogglePause();
            (pausePanelButtons[1] as Button).Click += (sender, e) => viewManager.StartNewGame();
            (pausePanelButtons[2] as Button).Click += (sender, e) => viewManager.ShowMainMenu();
        }

    }
}
