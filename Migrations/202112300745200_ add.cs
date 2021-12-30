namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Prompts", "MID", "dbo.Missions");
            DropIndex("dbo.Prompts", new[] { "MID" });
            DropTable("dbo.Prompts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Prompts",
                c => new
                    {
                        PID = c.Int(nullable: false, identity: true),
                        PContent = c.String(nullable: false),
                        MID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PID);
            
            CreateIndex("dbo.Prompts", "MID");
            AddForeignKey("dbo.Prompts", "MID", "dbo.Missions", "MID");
        }
    }
}
