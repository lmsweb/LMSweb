namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class score : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StudentMissions", "selfAssessment_SEID", "dbo.SelfAssessments");
            DropForeignKey("dbo.TeacherAssessments", "Mission_MID", "dbo.Missions");
            DropIndex("dbo.StudentMissions", new[] { "selfAssessment_SEID" });
            DropIndex("dbo.TeacherAssessments", new[] { "group_GID" });
            DropIndex("dbo.TeacherAssessments", new[] { "Mission_MID" });
            AddColumn("dbo.StudentMissions", "PersonalScore", c => c.Int(nullable: false));
            AddColumn("dbo.StudentMissions", "SelfA", c => c.Int(nullable: false));
            AddColumn("dbo.StudentMissions", "TeacherAssessment_TEID", c => c.Int());
            AddColumn("dbo.PeerAssessments", "CooperationScore", c => c.Int(nullable: false));
            AddColumn("dbo.TeacherAssessments", "GroupAchievementScore", c => c.Int(nullable: false));
            CreateIndex("dbo.StudentMissions", "TeacherAssessment_TEID");
            CreateIndex("dbo.TeacherAssessments", "Group_GID");
            AddForeignKey("dbo.StudentMissions", "TeacherAssessment_TEID", "dbo.TeacherAssessments", "TEID");
            DropColumn("dbo.StudentMissions", "selfAssessment_SEID");
            DropColumn("dbo.TeacherAssessments", "GroupAchievementLevel");
            DropColumn("dbo.TeacherAssessments", "GID");
            DropColumn("dbo.TeacherAssessments", "Mission_MID");
            DropTable("dbo.SelfAssessments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SelfAssessments",
                c => new
                    {
                        SEID = c.Int(nullable: false, identity: true),
                        CooperationLevel = c.Int(nullable: false),
                        PersonalContributionLevel = c.Int(nullable: false),
                        SelfA = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SEID);
            
            AddColumn("dbo.TeacherAssessments", "Mission_MID", c => c.String(maxLength: 128));
            AddColumn("dbo.TeacherAssessments", "GID", c => c.String());
            AddColumn("dbo.TeacherAssessments", "GroupAchievementLevel", c => c.Int(nullable: false));
            AddColumn("dbo.StudentMissions", "selfAssessment_SEID", c => c.Int());
            DropForeignKey("dbo.StudentMissions", "TeacherAssessment_TEID", "dbo.TeacherAssessments");
            DropIndex("dbo.TeacherAssessments", new[] { "Group_GID" });
            DropIndex("dbo.StudentMissions", new[] { "TeacherAssessment_TEID" });
            DropColumn("dbo.TeacherAssessments", "GroupAchievementScore");
            DropColumn("dbo.PeerAssessments", "CooperationScore");
            DropColumn("dbo.StudentMissions", "TeacherAssessment_TEID");
            DropColumn("dbo.StudentMissions", "SelfA");
            DropColumn("dbo.StudentMissions", "PersonalScore");
            CreateIndex("dbo.TeacherAssessments", "Mission_MID");
            CreateIndex("dbo.TeacherAssessments", "group_GID");
            CreateIndex("dbo.StudentMissions", "selfAssessment_SEID");
            AddForeignKey("dbo.TeacherAssessments", "Mission_MID", "dbo.Missions", "MID");
            AddForeignKey("dbo.StudentMissions", "selfAssessment_SEID", "dbo.SelfAssessments", "SEID");
        }
    }
}
