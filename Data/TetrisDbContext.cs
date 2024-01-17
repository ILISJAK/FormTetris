using System.Data.Entity;

namespace FormTetris
{
    public class TetrisDbContext : DbContext
    {
        public TetrisDbContext()
            : base("name=TetrisDbContext") // The name of the connection string
        {
        }
        public DbSet<Score> Scores { get; set; }

    }

}
