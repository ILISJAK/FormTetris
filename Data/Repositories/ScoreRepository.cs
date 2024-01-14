using System.Linq;

namespace FormTetris.Data.Repositories
{
    public class ScoreRepository
    {
        private readonly TetrisDbContext _context;

        public ScoreRepository(TetrisDbContext context)
        {
            _context = context;
        }

        public void AddScore(Score score)
        {
            _context.Scores.Add(score);
            _context.SaveChanges();
        }

        public Score GetScore(int id)
        {
            return _context.Scores.FirstOrDefault(s => s.ScoreId == id);
        }

        // Additional methods for update, delete, etc.
    }
}
