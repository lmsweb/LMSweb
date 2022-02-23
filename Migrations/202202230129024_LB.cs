namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LB : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.LearningBehaviors", new[] { "Student_SID" });
            RenameColumn(table: "dbo.LearningBehaviors", name: "StudentMissions_SID", newName: "StudentMission_SID");
            RenameColumn(table: "dbo.LearningBehaviors", name: "StudentMissions_MID", newName: "StudentMission_MID");
            RenameIndex(table: "dbo.LearningBehaviors", name: "IX_StudentMissions_SID_StudentMissions_MID", newName: "IX_StudentMission_SID_StudentMission_MID");
            AddColumn("dbo.LearningBehaviors", "mission_MID", c => c.String(maxLength: 128));
            AddColumn("dbo.LearningBehaviors", "group_GID", c => c.Int());
            CreateIndex("dbo.LearningBehaviors", "mission_MID");
            CreateIndex("dbo.LearningBehaviors", "student_SID");
            CreateIndex("dbo.LearningBehaviors", "group_GID");
            AddForeignKey("dbo.LearningBehaviors", "mission_MID", "dbo.Missions", "MID");
            AddForeignKey("dbo.LearningBehaviors", "group_GID", "dbo.Groups", "GID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LearningBehaviors", "group_GID", "dbo.Groups");
            DropForeignKey("dbo.LearningBehaviors", "mission_MID", "dbo.Missions");
            DropIndex("dbo.LearningBehaviors", new[] { "group_GID" });
            DropIndex("dbo.LearningBehaviors", new[] { "student_SID" });
            DropIndex("dbo.LearningBehaviors", new[] { "mission_MID" });
            DropColumn("dbo.LearningBehaviors", "group_GID");
            DropColumn("dbo.LearningBehaviors", "mission_MID");
            RenameIndex(table: "dbo.LearningBehaviors", name: "IX_StudentMission_SID_StudentMission_MID", newName: "IX_StudentMissions_SID_StudentMissions_MID");
            RenameColumn(table: "dbo.LearningBehaviors", name: "StudentMission_MID", newName: "StudentMissions_MID");
            RenameColumn(table: "dbo.LearningBehaviors", name: "StudentMission_SID", newName: "StudentMissions_SID");
            CreateIndex("dbo.LearningBehaviors", "Student_SID");
        }
    }
}
