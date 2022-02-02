namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class groupassessment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TeacherAssessments", "Group_GID", "dbo.Groups");
            DropIndex("dbo.TeacherAssessments", new[] { "Group_GID" });
            RenameColumn(table: "dbo.TeacherAssessments", name: "Group_GID", newName: "GID");
            AddColumn("dbo.TeacherAssessments", "MID", c => c.String(maxLength: 128));
            AlterColumn("dbo.TeacherAssessments", "GID", c => c.Int(nullable: false));
            CreateIndex("dbo.TeacherAssessments", "GID");
            CreateIndex("dbo.TeacherAssessments", "MID");
            AddForeignKey("dbo.TeacherAssessments", "MID", "dbo.Missions", "MID");
            AddForeignKey("dbo.TeacherAssessments", "GID", "dbo.Groups", "GID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherAssessments", "GID", "dbo.Groups");
            DropForeignKey("dbo.TeacherAssessments", "MID", "dbo.Missions");
            DropIndex("dbo.TeacherAssessments", new[] { "MID" });
            DropIndex("dbo.TeacherAssessments", new[] { "GID" });
            AlterColumn("dbo.TeacherAssessments", "GID", c => c.Int());
            DropColumn("dbo.TeacherAssessments", "MID");
            RenameColumn(table: "dbo.TeacherAssessments", name: "GID", newName: "Group_GID");
            CreateIndex("dbo.TeacherAssessments", "Group_GID");
            AddForeignKey("dbo.TeacherAssessments", "Group_GID", "dbo.Groups", "GID");
        }
    }
}
