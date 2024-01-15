using System;
using System.Drawing;

namespace FormTetris
{
    public class GameHUD
    {
        private ScoreManager scoreManager;
        private Font hudFont = new Font("Consolas", 14);
        private Point hudPosition = new Point(10, 10); // You can adjust this as needed

        public GameHUD(ScoreManager scoreManager)
        {
            this.scoreManager = scoreManager;
        }

        public void DrawHUD(Graphics graphics)
        {
            string hudText = $"SCORE:\n{scoreManager.TotalScore}\nTIME:\n{FormatTime(scoreManager.GameTime)}\n" +
                             $"LINES:\n{scoreManager.LinesCleared}\nLEVEL:\n{scoreManager.Level}\n" +
                             $"GOAL:\n{CalculateGoal()}\nTETRISES:\n{scoreManager.Tetrises}\n" +
                             $"T-SPINS:\n{scoreManager.TSpins}\nCOMBOS:\n{scoreManager.Combos}\n" +
                             $"TPM:\n{scoreManager.TPM}\nLPM:\n{scoreManager.LPM}";

            using (SolidBrush brush = new SolidBrush(Color.FromArgb(180, 255, 255, 255))) // Semi-transparent white
            {
                graphics.DrawString(hudText, hudFont, brush, hudPosition);
            }
        }

        private string FormatTime(TimeSpan time)
        {
            return time.ToString(@"mm\:ss\.ff");
        }

        private int CalculateGoal()
        {
            // Assuming goal is calculated based on level and lines cleared
            return (scoreManager.Level + 1) * 10 - scoreManager.LinesCleared;
        }
    }

}
