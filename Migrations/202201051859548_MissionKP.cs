namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MissionKP : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MissionKnowledgePoints",
                c => new
                    {
                        MID = c.String(nullable: false, maxLength: 128),
                        KID = c.String(nullable: false, maxLength: 128),
                        KnowledgePoint_KID = c.Int(),
                    })
                .PrimaryKey(t => new { t.MID, t.KID })
                .ForeignKey("dbo.KnowledgePoints", t => t.KnowledgePoint_KID)
                .ForeignKey("dbo.Missions", t => t.MID, cascadeDelete: true)
                .Index(t => t.MID)
                .Index(t => t.KnowledgePoint_KID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MissionKnowledgePoints", "MID", "dbo.Missions");
            DropForeignKey("dbo.MissionKnowledgePoints", "KnowledgePoint_KID", "dbo.KnowledgePoints");
            DropIndex("dbo.MissionKnowledgePoints", new[] { "KnowledgePoint_KID" });
            DropIndex("dbo.MissionKnowledgePoints", new[] { "MID" });
            DropTable("dbo.MissionKnowledgePoints");
        }
    }
}
