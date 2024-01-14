namespace FormTetris.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        MatchId = c.Int(nullable: false, identity: true),
                        DatePlayed = c.DateTime(nullable: false),
                        PlayerId = c.Int(nullable: false),
                        MatchScore_ScoreId = c.Int(),
                    })
                .PrimaryKey(t => t.MatchId)
                .ForeignKey("dbo.Scores", t => t.MatchScore_ScoreId)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.PlayerId)
                .Index(t => t.MatchScore_ScoreId);
            
            CreateTable(
                "dbo.Scores",
                c => new
                    {
                        ScoreId = c.Int(nullable: false, identity: true),
                        PlayerId = c.Int(nullable: false),
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
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        PlayerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.PlayerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matches", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.Matches", "MatchScore_ScoreId", "dbo.Scores");
            DropIndex("dbo.Matches", new[] { "MatchScore_ScoreId" });
            DropIndex("dbo.Matches", new[] { "PlayerId" });
            DropTable("dbo.Players");
            DropTable("dbo.Scores");
            DropTable("dbo.Matches");
        }
    }
}
