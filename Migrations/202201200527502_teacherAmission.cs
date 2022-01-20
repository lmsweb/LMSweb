namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teacherAmission : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeacherAssessments", "Mission_MID", c => c.String(maxLength: 128));
            CreateIndex("dbo.TeacherAssessments", "Mission_MID");
            AddForeignKey("dbo.TeacherAssessments", "Mission_MID", "dbo.Missions", "MID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherAssessments", "Mission_MID", "dbo.Missions");
            DropIndex("dbo.TeacherAssessments", new[] { "Mission_MID" });
            DropColumn("dbo.TeacherAssessments", "Mission_MID");
        }
    }
}
