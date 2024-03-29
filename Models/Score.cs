﻿using System;
using System.ComponentModel.DataAnnotations;

namespace FormTetris
{
    public class Score
    {
        public int ScoreId { get; set; }

        [MaxLength(3)] // Set maximum length to 3 characters
        public string PlayerPseudonym { get; set; }

        public int TotalScore { get; set; }
        public TimeSpan Time { get; set; }
        public int LinesCleared { get; set; }
        public int LevelReached { get; set; }
        public int Tetrises { get; set; }
        public int TSpins { get; set; }
        public int Combos { get; set; }
        public int TPM { get; set; }
        public int LPM { get; set; }
    }
}
