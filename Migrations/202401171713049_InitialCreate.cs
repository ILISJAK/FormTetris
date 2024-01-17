namespace FormTetris.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Scores",
                c => new
                {
                    ScoreId = c.Int(nullable: false, identity: true),
                    PlayerPseudonym = c.String(maxLength: 3),
                    TotalScore = c.Int(nullable: false),
                    Time = c.Time(nullable: false, precision: 7),
                    LinesCleared = c.Int(nullable: false),
                    LevelReached = c.Int(nullable: false),
                    Tetrises = c.Int(nullable: false),
                    TSpins = c.Int(nullable: false),
                    Combos = c.Int(nullable: false),
                    TPM = c.Int(nullable: false),
                    LPM = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ScoreId);

        }

        public override void Down()
        {
            DropTable("dbo.Scores");
        }
    }
}
