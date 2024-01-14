using System;

namespace FormTetris
{
    public class ScoreManager
    {
        public int TotalScore { get; private set; }
        public TimeSpan GameTime { get; private set; }
        public int LinesCleared { get; private set; }
        public int Level { get; private set; }
        public int Tetrises { get; private set; }
        public int TSpins { get; private set; }
        public int Combos { get; private set; }
        public int TPM => CalculateTPM(); // Tetrominos per minute
        public int LPM => CalculateLPM(); // Lines per minute

        private DateTime startTime;
        private int tetrominosDropped;

        public ScoreManager()
        {
            Reset();
        }

        public void StartGame()
        {
            startTime = DateTime.Now;
            GameTime = TimeSpan.Zero;
            TotalScore = 0;
            LinesCleared = 0;
            Level = 1;
            Tetrises = 0;
            TSpins = 0;
            Combos = 0;
            tetrominosDropped = 0;
        }

        public void UpdateGameTime()
        {
            GameTime = DateTime.Now - startTime;
        }

        public void Reset()
        {
            TotalScore = 0;
            LinesCleared = 0;
            Level = 0;
            Tetrises = 0;
            TSpins = 0;
            Combos = 0;
            tetrominosDropped = 0;
            startTime = DateTime.Now;
        }

        public void LineCleared(int lines)
        {
            LinesCleared += lines;
            CalculateScore(lines);
            // Increment Tetrises if 4 lines are cleared at once
            if (lines == 4) Tetrises++;
        }

        public void TetrominoDropped()
        {
            tetrominosDropped++;
        }

        public void TSpin()
        {
            TSpins++;
            // Update score appropriately for a T-Spin
            // This method should be called when a T-Spin is performed
        }

        public void Combo(int comboLength)
        {
            Combos = Math.Max(Combos, comboLength);
            // Update score based on combo length
        }

        private void CalculateScore(int lines)
        {
            // Add scoring logic here. This can vary depending on your scoring rules.
            // Example: TotalScore += lines * 100;
        }

        private int CalculateTPM()
        {
            return tetrominosDropped == 0 ? 0 : (int)(tetrominosDropped / GameTime.TotalMinutes);
        }

        private int CalculateLPM()
        {
            return LinesCleared == 0 ? 0 : (int)(LinesCleared / GameTime.TotalMinutes);
        }

        public Score ToScoreEntity()
        {
            return new Score
            {
                TotalScore = TotalScore,
                Time = GameTime,
                LinesCleared = LinesCleared,
                LevelReached = Level,
                Tetrises = Tetrises,
                TSpins = TSpins,
                Combos = Combos,
                TPM = TPM,
                LPM = LPM
            };
        }
    }

}
