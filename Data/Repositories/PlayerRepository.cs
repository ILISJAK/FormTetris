using System.Linq;

namespace FormTetris.Data.Repositories
{
    public class PlayerRepository
    {
        private readonly TetrisDbContext _context;

        public PlayerRepository(TetrisDbContext context)
        {
            _context = context;
        }

        public void AddPlayer(Player player)
        {
            _context.Players.Add(player);
            _context.SaveChanges();
        }

        public Player GetPlayer(int id)
        {
            return _context.Players.FirstOrDefault(p => p.PlayerId == id);
        }

        // Additional methods for update, delete, etc.
    }
}
