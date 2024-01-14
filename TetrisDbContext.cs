using System.Data.Entity;

namespace FormTetris
{
    public class TetrisDbContext : DbContext
    {
        public TetrisDbContext()
            : base("name=TetrisDbContext") // The name of the connection string
        {
        }
        public DbSet<Player> Players { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Score> Scores { get; set; }

    }

}
