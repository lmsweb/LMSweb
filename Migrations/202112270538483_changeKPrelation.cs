namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeKPrelation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.KnowledgePointMissions", "KnowledgePoint_KID", "dbo.KnowledgePoints");
            DropForeignKey("dbo.KnowledgePointMissions", "Mission_MID", "dbo.Missions");
            DropIndex("dbo.KnowledgePointMissions", new[] { "KnowledgePoint_KID" });
            DropIndex("dbo.KnowledgePointMissions", new[] { "Mission_MID" });
            AddColumn("dbo.Courses", "KnowledgePoint_KID", c => c.Int());
            AddColumn("dbo.KnowledgePoints", "Mission_MID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Courses", "KnowledgePoint_KID");
            CreateIndex("dbo.KnowledgePoints", "Mission_MID");
            AddForeignKey("dbo.Courses", "KnowledgePoint_KID", "dbo.KnowledgePoints", "KID");
            AddForeignKey("dbo.KnowledgePoints", "Mission_MID", "dbo.Missions", "MID");
            DropTable("dbo.KnowledgePointMissions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.KnowledgePointMissions",
                c => new
                    {
                        KnowledgePoint_KID = c.Int(nullable: false),
                        Mission_MID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.KnowledgePoint_KID, t.Mission_MID });
            
            DropForeignKey("dbo.KnowledgePoints", "Mission_MID", "dbo.Missions");
            DropForeignKey("dbo.Courses", "KnowledgePoint_KID", "dbo.KnowledgePoints");
            DropIndex("dbo.KnowledgePoints", new[] { "Mission_MID" });
            DropIndex("dbo.Courses", new[] { "KnowledgePoint_KID" });
            DropColumn("dbo.KnowledgePoints", "Mission_MID");
            DropColumn("dbo.Courses", "KnowledgePoint_KID");
            CreateIndex("dbo.KnowledgePointMissions", "Mission_MID");
            CreateIndex("dbo.KnowledgePointMissions", "KnowledgePoint_KID");
            AddForeignKey("dbo.KnowledgePointMissions", "Mission_MID", "dbo.Missions", "MID", cascadeDelete: true);
            AddForeignKey("dbo.KnowledgePointMissions", "KnowledgePoint_KID", "dbo.KnowledgePoints", "KID", cascadeDelete: true);
        }
    }
}
