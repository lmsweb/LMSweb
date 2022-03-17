namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addGroupQ : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GroupERs", "QID", "dbo.Questions");
            DropIndex("dbo.GroupERs", new[] { "QID" });
            CreateTable(
                "dbo.GroupQuestions",
                c => new
                    {
                        GQID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.GQID);
            
            CreateTable(
                "dbo.GroupOptions",
                c => new
                    {
                        GOID = c.Int(nullable: false, identity: true),
                        GQID = c.Int(nullable: false),
                        OptionNum = c.String(),
                    })
                .PrimaryKey(t => t.GOID)
                .ForeignKey("dbo.GroupQuestions", t => t.GQID, cascadeDelete: true)
                .Index(t => t.GQID);
            
            AddColumn("dbo.GroupERs", "GQID", c => c.Int(nullable: false));
            AddColumn("dbo.GroupERs", "MID", c => c.String(maxLength: 128));
            CreateIndex("dbo.GroupERs", "GQID");
            CreateIndex("dbo.GroupERs", "MID");
            AddForeignKey("dbo.GroupERs", "GQID", "dbo.GroupQuestions", "GQID", cascadeDelete: true);
            AddForeignKey("dbo.GroupERs", "MID", "dbo.Missions", "MID");
            DropColumn("dbo.GroupERs", "QID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GroupERs", "QID", c => c.Int(nullable: false));
            DropForeignKey("dbo.GroupERs", "MID", "dbo.Missions");
            DropForeignKey("dbo.GroupOptions", "GQID", "dbo.GroupQuestions");
            DropForeignKey("dbo.GroupERs", "GQID", "dbo.GroupQuestions");
            DropIndex("dbo.GroupOptions", new[] { "GQID" });
            DropIndex("dbo.GroupERs", new[] { "MID" });
            DropIndex("dbo.GroupERs", new[] { "GQID" });
            DropColumn("dbo.GroupERs", "MID");
            DropColumn("dbo.GroupERs", "GQID");
            DropTable("dbo.GroupOptions");
            DropTable("dbo.GroupQuestions");
            CreateIndex("dbo.GroupERs", "QID");
            AddForeignKey("dbo.GroupERs", "QID", "dbo.Questions", "QID", cascadeDelete: true);
        }
    }
}
