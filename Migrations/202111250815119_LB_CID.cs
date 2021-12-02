namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LB_CID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LearningBehaviors", "CID", c => c.String(maxLength: 128));
            CreateIndex("dbo.LearningBehaviors", "CID");
            AddForeignKey("dbo.LearningBehaviors", "CID", "dbo.Courses", "CID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LearningBehaviors", "CID", "dbo.Courses");
            DropIndex("dbo.LearningBehaviors", new[] { "CID" });
            DropColumn("dbo.LearningBehaviors", "CID");
        }
    }
}
