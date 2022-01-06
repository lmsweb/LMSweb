namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentMission : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "Mission_MID", "dbo.Missions");
            DropIndex("dbo.Students", new[] { "Mission_MID" });
            CreateIndex("dbo.StudentMissions", "SID");
            CreateIndex("dbo.StudentMissions", "MID");
            AddForeignKey("dbo.StudentMissions", "MID", "dbo.Missions", "MID", cascadeDelete: true);
            AddForeignKey("dbo.StudentMissions", "SID", "dbo.Students", "SID", cascadeDelete: true);
            DropColumn("dbo.Students", "Mission_MID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "Mission_MID", c => c.String(maxLength: 128));
            DropForeignKey("dbo.StudentMissions", "SID", "dbo.Students");
            DropForeignKey("dbo.StudentMissions", "MID", "dbo.Missions");
            DropIndex("dbo.StudentMissions", new[] { "MID" });
            DropIndex("dbo.StudentMissions", new[] { "SID" });
            CreateIndex("dbo.Students", "Mission_MID");
            AddForeignKey("dbo.Students", "Mission_MID", "dbo.Missions", "MID");
        }
    }
}
