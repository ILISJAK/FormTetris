using System.Linq;

namespace FormTetris.Data.Repositories
{
    public class MatchRepository
    {
        private readonly TetrisDbContext _context;

        public MatchRepository(TetrisDbContext context)
        {
            _context = context;
        }

        public void AddMatch(Match match)
        {
            _context.Matches.Add(match);
            _context.SaveChanges();
        }

        public Match GetMatch(int id)
        {
            return _context.Matches.FirstOrDefault(m => m.MatchId == id);
        }

        // Additional methods for update, delete, etc.
    }
}
