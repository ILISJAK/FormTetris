using FormTetris.Data.Repositories;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FormTetris.Data.Repositories.ScoreRepository;

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
        public Panel EndGamePanel { get; private set; }
        public Panel SubmitScorePanel { get; private set; }

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
            EndGamePanel = CreatePanel();
            SubmitScorePanel = CreatePanel();

            SetupMainMenu();
            SetupHelpPanel();
            SetupOptionsPanel();
            SetupPausePanel();
            SetupEndGamePanel();
            SetupSubmitScorePanel();
            PopulateLeaderboard();
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

            flowLayout.Controls.Add(backButton);

            OptionsPanel.Controls.Add(flowLayout);
            CenterFlowLayoutInParent(OptionsPanel);
        }

        private void SetupPausePanel()
        {
            FlowLayoutPanel flowLayout = CreateFlowLayout();
            PausePanel.BackColor = Color.FromArgb(128, 0, 0, 0);

            var resumeButton = elementFactory.CreateButton("Resume");
            var restartButton = elementFactory.CreateButton("Restart");
            var quitButton = elementFactory.CreateButton("Quit to Main Menu");

            flowLayout.Controls.Add(resumeButton);
            flowLayout.Controls.Add(restartButton);
            flowLayout.Controls.Add(quitButton);
            PausePanel.Controls.Add(flowLayout);
            CenterFlowLayoutInParent(PausePanel);
        }

        private void SetupEndGamePanel()
        {
            EndGamePanel.Controls.Clear();

            TableLayoutPanel tblLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.Black
            };
            tblLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            FlowLayoutPanel gameInfoPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true,
                BackColor = Color.Black
            };

            gameInfoPanel.Controls.Add(elementFactory.CreateLabel("Total Score: ", null, ContentAlignment.TopLeft, "totalScoreLabel"));
            gameInfoPanel.Controls.Add(elementFactory.CreateLabel("Game Time: ", null, ContentAlignment.TopLeft, "gameTimeLabel"));
            gameInfoPanel.Controls.Add(elementFactory.CreateLabel("Lines Cleared: ", null, ContentAlignment.TopLeft, "linesClearedLabel"));
            gameInfoPanel.Controls.Add(elementFactory.CreateLabel("Level: ", null, ContentAlignment.TopLeft, "levelLabel"));
            gameInfoPanel.Controls.Add(elementFactory.CreateLabel("Tetrises: ", null, ContentAlignment.TopLeft, "tetrisesLabel"));
            gameInfoPanel.Controls.Add(elementFactory.CreateLabel("T-Spins: ", null, ContentAlignment.TopLeft, "tSpinsLabel"));
            gameInfoPanel.Controls.Add(elementFactory.CreateLabel("Combos: ", null, ContentAlignment.TopLeft, "combosLabel"));
            gameInfoPanel.Controls.Add(elementFactory.CreateLabel("TPM: ", null, ContentAlignment.TopLeft, "tpmLabel"));
            gameInfoPanel.Controls.Add(elementFactory.CreateLabel("LPM: ", null, ContentAlignment.TopLeft, "lpmLabel"));

            Panel leaderboardPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Black
            };

            var leaderboardDataGrid = elementFactory.CreateDataGridView();
            leaderboardDataGrid.Name = "leaderboardDataGrid";
            ConfigureLeaderboardDataGridView(leaderboardDataGrid);
            leaderboardPanel.Controls.Add(leaderboardDataGrid);

            tblLayout.Controls.Add(gameInfoPanel, 0, 0);
            tblLayout.Controls.Add(leaderboardPanel, 1, 0);

            FlowLayoutPanel buttonsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                BackColor = Color.Black,
                Padding = new Padding(5)
            };

            var submitButton = elementFactory.CreateButton("Submit Score");
            submitButton.Name = "submitButton";
            var restartButton = elementFactory.CreateButton("Restart Game");
            restartButton.Name = "restartButton";
            var returnButton = elementFactory.CreateButton("Return to Menu");
            returnButton.Name = "returnButton";

            buttonsPanel.Controls.Add(submitButton);
            buttonsPanel.Controls.Add(restartButton);
            buttonsPanel.Controls.Add(returnButton);

            EndGamePanel.Controls.Add(tblLayout);
            EndGamePanel.Controls.Add(buttonsPanel);
            EndGamePanel.Visible = true;
            EndGamePanel.BringToFront();
            EndGamePanel.Refresh();
        }

        private void SetupSubmitScorePanel()
        {
            FlowLayoutPanel flowLayout = CreateFlowLayout();

            var nameLabel = elementFactory.CreateLabel("Enter your name:");
            var playerNameTextBox = elementFactory.CreateTextBox("", false, false);
            playerNameTextBox.ForeColor = Color.Black;
            var okButton = elementFactory.CreateButton("OK");
            var cancelButton = elementFactory.CreateButton("Cancel");

            flowLayout.Controls.Add(nameLabel);
            flowLayout.Controls.Add(playerNameTextBox);
            flowLayout.Controls.Add(okButton);
            flowLayout.Controls.Add(cancelButton);

            flowLayout.Anchor = AnchorStyles.None;
            SubmitScorePanel.Controls.Add(flowLayout);
            CenterFlowLayoutInParent(SubmitScorePanel);
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

        private string FormatStat(string label, string value)
        {
            int maxLabelLength = new[] { "Total Score:", "Game Time:", "Lines Cleared:", "Level:", "Tetrises:", "T-Spins:", "Combos:", "TPM:", "LPM:" }.Max(l => l.Length);
            return $"{label.PadRight(maxLabelLength)}\t{value}";
        }

        public void UpdateEndGameStats(ScoreManager scoreManager)
        {
            TableLayoutPanel tblLayout = EndGamePanel.Controls.OfType<TableLayoutPanel>().FirstOrDefault();
            if (tblLayout != null)
            {
                FlowLayoutPanel gameInfoPanel = tblLayout.GetControlFromPosition(0, 0) as FlowLayoutPanel;

                if (gameInfoPanel != null)
                {
                    UpdateLabel(gameInfoPanel, "totalScoreLabel", FormatStat("SCORE:", scoreManager.TotalScore.ToString()));
                    UpdateLabel(gameInfoPanel, "gameTimeLabel", FormatStat("TIME:", scoreManager.GameTime.ToString(@"hh\:mm\:ss")));
                    UpdateLabel(gameInfoPanel, "linesClearedLabel", FormatStat("LINES:", scoreManager.LinesCleared.ToString()));
                    UpdateLabel(gameInfoPanel, "levelLabel", FormatStat("LEVEL:", scoreManager.Level.ToString()));
                    UpdateLabel(gameInfoPanel, "tetrisesLabel", FormatStat("TETRIS:", scoreManager.Tetrises.ToString()));
                    UpdateLabel(gameInfoPanel, "tSpinsLabel", FormatStat("T-SPIN:", scoreManager.TSpins.ToString()));
                    UpdateLabel(gameInfoPanel, "combosLabel", FormatStat("COMBO:", scoreManager.Combos.ToString()));
                    UpdateLabel(gameInfoPanel, "tpmLabel", FormatStat("TPM:", scoreManager.TPM.ToString()));
                    UpdateLabel(gameInfoPanel, "lpmLabel", FormatStat("LPM:", scoreManager.LPM.ToString()));
                }
            }
        }

        public async Task PopulateLeaderboard()
        {
            var scoreRepository = new ScoreRepository(new TetrisDbContext());
            var scoresWithRanks = await scoreRepository.GetAllScoresWithRanks();

            var bindingList = new BindingList<ScoreWithRank>(scoresWithRanks);
            var source = new BindingSource(bindingList, null);

            var leaderboardDataGrid = EndGamePanel.Controls.Find("leaderboardDataGrid", true).FirstOrDefault() as DataGridView;
            if (leaderboardDataGrid != null)
            {
                leaderboardDataGrid.DataSource = source;
                leaderboardDataGrid.Refresh();
            }
        }


        private void ConfigureLeaderboardDataGridView(DataGridView dataGridView)
        {
            dataGridView.AutoGenerateColumns = false;

            dataGridView.Columns.Insert(0, new DataGridViewTextBoxColumn
            {
                HeaderText = "RANK",
                DataPropertyName = "Rank",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            });

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "PLAYER",
                DataPropertyName = "PlayerPseudonym",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "SCORE",
                DataPropertyName = "TotalScore",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            });

            dataGridView.BackgroundColor = Color.Black;
            dataGridView.ForeColor = Color.White;
            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.MultiSelect = false;
            dataGridView.ReadOnly = true;
        }

        private void UpdateLabel(FlowLayoutPanel panel, string controlName, string text)
        {
            var label = panel.Controls.Find(controlName, false).FirstOrDefault() as Label;
            if (label != null)
            {
                label.Text = text;
            }
        }

        public void UpdateLayout(Size newSize)
        {
            MainMenuPanel.Size = newSize;
            HelpPanel.Size = newSize;
            OptionsPanel.Size = newSize;
            PausePanel.Size = newSize;

            foreach (var panel in new[] { MainMenuPanel, HelpPanel, OptionsPanel, PausePanel })
            {
                foreach (Control control in panel.Controls)
                {
                    if (control is FlowLayoutPanel flowLayout)
                    {
                        flowLayout.Size = newSize;
                        CenterFlowLayoutInParent(panel);
                    }
                }
            }
        }
    }
}
