namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeacherAaddCID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeacherAssessments", "CID", c => c.String(maxLength: 128));
            CreateIndex("dbo.TeacherAssessments", "CID");
            AddForeignKey("dbo.TeacherAssessments", "CID", "dbo.Courses", "CID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherAssessments", "CID", "dbo.Courses");
            DropIndex("dbo.TeacherAssessments", new[] { "CID" });
            DropColumn("dbo.TeacherAssessments", "CID");
        }
    }
}
