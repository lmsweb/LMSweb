namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delMKP : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MissionKnowledgePoints", "KnowledgePoint_KID", "dbo.KnowledgePoints");
            DropForeignKey("dbo.MissionKnowledgePoints", "MID", "dbo.Missions");
            DropIndex("dbo.MissionKnowledgePoints", new[] { "MID" });
            DropIndex("dbo.MissionKnowledgePoints", new[] { "KnowledgePoint_KID" });
            DropTable("dbo.MissionKnowledgePoints");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MissionKnowledgePoints",
                c => new
                    {
                        MID = c.String(nullable: false, maxLength: 128),
                        KID = c.String(nullable: false, maxLength: 128),
                        KnowledgePoint_KID = c.Int(),
                    })
                .PrimaryKey(t => new { t.MID, t.KID });
            
            CreateIndex("dbo.MissionKnowledgePoints", "KnowledgePoint_KID");
            CreateIndex("dbo.MissionKnowledgePoints", "MID");
            AddForeignKey("dbo.MissionKnowledgePoints", "MID", "dbo.Missions", "MID", cascadeDelete: true);
            AddForeignKey("dbo.MissionKnowledgePoints", "KnowledgePoint_KID", "dbo.KnowledgePoints", "KID");
        }
    }
}
