namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentLB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LearningBehaviors", "Student_SID", c => c.String(maxLength: 128));
            CreateIndex("dbo.LearningBehaviors", "Student_SID");
            AddForeignKey("dbo.LearningBehaviors", "Student_SID", "dbo.Students", "SID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LearningBehaviors", "Student_SID", "dbo.Students");
            DropIndex("dbo.LearningBehaviors", new[] { "Student_SID" });
            DropColumn("dbo.LearningBehaviors", "Student_SID");
        }
    }
}
