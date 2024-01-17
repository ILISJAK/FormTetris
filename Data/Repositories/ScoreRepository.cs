using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FormTetris.Data.Repositories
{
    public class ScoreRepository
    {
        private readonly TetrisDbContext _context;

        public ScoreRepository(TetrisDbContext context)
        {
            _context = context;
        }
        public struct ScoreWithRank
        {
            public int Rank { get; set; }
            public string PlayerPseudonym { get; set; }
            public int TotalScore { get; set; }
        }

        public void AddScore(Score score)
        {
            try
            {
                DebugForm.Instance.Log($"Adding score: {score.TotalScore}");
                _context.Scores.Add(score);
                _context.SaveChanges();
                DebugForm.Instance.Log("Score added successfully.");
            }
            catch (Exception ex)
            {
                DebugForm.Instance.Log($"Error adding score to database: {ex.Message}");
                throw;
            }
        }

        public Score GetScore(int id)
        {
            try
            {
                DebugForm.Instance.Log($"Retrieving score with ID: {id}");
                var score = _context.Scores.FirstOrDefault(s => s.ScoreId == id);
                if (score != null)
                {
                    DebugForm.Instance.Log($"Score retrieved: {score.TotalScore}");
                }
                else
                {
                    DebugForm.Instance.Log($"No score found with ID: {id}");
                }
                return score;
            }
            catch (Exception ex)
            {
                DebugForm.Instance.Log($"Error retrieving score with ID: {id}: {ex.Message}");
                throw;
            }
        }
        public async Task<List<ScoreWithRank>> GetAllScoresWithRanks()
        {
            var scores = await _context.Scores
                .OrderByDescending(s => s.TotalScore)
                .ToListAsync();

            var scoresWithRanks = scores.Select((score, index) => new ScoreWithRank
            {
                Rank = index + 1,
                PlayerPseudonym = score.PlayerPseudonym,
                TotalScore = score.TotalScore
                // ... assign other properties if needed
            }).ToList();

            return scoresWithRanks;
        }
    }

}
