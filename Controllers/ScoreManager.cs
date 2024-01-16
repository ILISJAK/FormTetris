using System;
using System.Diagnostics;

namespace FormTetris
{
    public class ScoreManager
    {
        public int TotalScore { get; private set; }
        public TimeSpan GameTime => gameTimeStopwatch.Elapsed;
        public int LinesCleared { get; private set; }
        public int Level { get; private set; }
        public int Tetrises { get; private set; }
        public int TSpins { get; private set; }
        public int Combos { get; private set; }
        public int TPM => CalculateTPM();
        public int LPM => CalculateLPM();

        private readonly Stopwatch gameTimeStopwatch;
        private int tetrominosDropped;
        public delegate void LevelChangedHandler(int newLevel);
        public event LevelChangedHandler LevelChanged;

        private static ScoreManager instance;
        public static ScoreManager Instance => instance ?? (instance = new ScoreManager());

        private ScoreManager()
        {
            gameTimeStopwatch = new Stopwatch();
            Reset();
        }

        public void StartGame()
        {
            Reset();
            gameTimeStopwatch.Start();
        }

        public void PauseGameTime()
        {
            gameTimeStopwatch.Stop();
        }

        public void ResumeGameTime()
        {
            gameTimeStopwatch.Start();
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
            ResetGameTime();
        }

        public void ResetGameTime()
        {
            gameTimeStopwatch.Reset();
        }

        public void LineCleared(int lines)
        {
            LinesCleared += lines;
            CalculateScore(lines);
            if (lines == 4) Tetrises++;
            UpdateLevelBasedOnLinesCleared();
        }

        public void TetrominoDropped()
        {
            tetrominosDropped++;
        }

        public void TSpin()
        {
            TSpins++;
        }

        public void Combo(int comboLength)
        {
            Combos = Math.Max(Combos, comboLength);
        }

        private void CalculateScore(int lines)
        {
            switch (lines)
            {
                case 1:
                    TotalScore += 40 * (Level + 1);
                    break;
                case 2:
                    TotalScore += 100 * (Level + 1);
                    break;
                case 3:
                    TotalScore += 300 * (Level + 1);
                    break;
                case 4:
                    TotalScore += 1200 * (Level + 1);
                    break;
            }
        }

        private void UpdateLevelBasedOnLinesCleared()
        {
            int linesThreshold = 10;
            int newLevel = LinesCleared / linesThreshold;

            // Check if the level has changed before setting it and invoking the event
            if (Level != newLevel)
            {
                Level = newLevel;
                LevelChanged?.Invoke(newLevel);
                // Log information for debugging purposes
                Debug.WriteLine($"Level changed to: {newLevel}");
            }
        }

        private int CalculateTPM()
        {
            double totalMinutes = GameTime.TotalMinutes;
            return totalMinutes > 0 ? (int)(tetrominosDropped / totalMinutes) : 0;
        }

        private int CalculateLPM()
        {
            double totalMinutes = GameTime.TotalMinutes;
            return totalMinutes > 0 ? (int)(LinesCleared / totalMinutes) : 0;
        }

        public Score ToScoreEntity()
        {
            return new Score
            {
                TotalScore = TotalScore,
                Time = GameTime,
                LinesCleared = LinesCleared,
                LevelReached = Level + 1,
                Tetrises = Tetrises,
                TSpins = TSpins,
                Combos = Combos,
                TPM = TPM,
                LPM = LPM
            };
        }
    }
}