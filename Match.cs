using System;

namespace FormTetris
{
    public class Match
    {
        public int MatchId { get; set; }
        public DateTime DatePlayed { get; set; }
        public virtual Score MatchScore { get; set; }
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }
    }
}
