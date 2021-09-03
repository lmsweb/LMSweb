namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AID = c.String(nullable: false, maxLength: 128),
                        APassword = c.String(),
                        AName = c.String(),
                    })
                .PrimaryKey(t => t.AID);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CID = c.String(nullable: false, maxLength: 128),
                        CName = c.String(nullable: false),
                        TID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CID)
                .ForeignKey("dbo.Teachers", t => t.TID)
                .Index(t => t.TID);
            
            CreateTable(
                "dbo.Missions",
                c => new
                    {
                        MID = c.String(nullable: false, maxLength: 128),
                        Start = c.String(nullable: false),
                        End = c.String(nullable: false),
                        MName = c.String(nullable: false),
                        MDetail = c.String(nullable: false),
                        CID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MID)
                .ForeignKey("dbo.Courses", t => t.CID)
                .Index(t => t.CID);
            
            CreateTable(
                "dbo.KnowledgePoints",
                c => new
                    {
                        KID = c.Int(nullable: false, identity: true),
                        KContent = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.KID);
            
            CreateTable(
                "dbo.Prompts",
                c => new
                    {
                        PID = c.Int(nullable: false, identity: true),
                        PContent = c.String(nullable: false),
                        MID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PID)
                .ForeignKey("dbo.Missions", t => t.MID)
                .Index(t => t.MID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        SID = c.String(nullable: false, maxLength: 128),
                        CID = c.String(nullable: false, maxLength: 128),
                        SName = c.String(nullable: false),
                        Grade = c.String(nullable: false),
                        SPassword = c.String(nullable: false, maxLength: 18),
                        Sex = c.String(nullable: false),
                        Stage = c.String(nullable: false),
                        Score = c.String(),
                        group_GID = c.Int(),
                    })
                .PrimaryKey(t => new { t.SID, t.CID })
                .ForeignKey("dbo.Courses", t => t.CID, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.group_GID)
                .Index(t => t.CID)
                .Index(t => t.group_GID);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GID = c.Int(nullable: false, identity: true),
                        GName = c.String(nullable: false),
                        Position = c.String(),
                        MID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.GID)
                .ForeignKey("dbo.Missions", t => t.MID)
                .Index(t => t.MID);
            
            CreateTable(
                "dbo.TeacherAssessments",
                c => new
                    {
                        TEID = c.Int(nullable: false, identity: true),
                        TeacherA = c.String(nullable: false),
                        GroupAchievementLevel = c.Int(nullable: false),
                        GID = c.String(),
                        group_GID = c.Int(),
                    })
                .PrimaryKey(t => t.TEID)
                .ForeignKey("dbo.Groups", t => t.group_GID)
                .Index(t => t.group_GID);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TID = c.String(nullable: false, maxLength: 128),
                        TName = c.String(nullable: false),
                        TPassword = c.String(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.TID);
            
            CreateTable(
                "dbo.LearningBehaviors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ActionType = c.String(nullable: false),
                        subAction = c.String(nullable: false),
                        Detail = c.String(nullable: false),
                        Time = c.String(),
                        StudentMissions_SID = c.String(maxLength: 128),
                        StudentMissions_MID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.StudentMissions", t => new { t.StudentMissions_SID, t.StudentMissions_MID })
                .Index(t => new { t.StudentMissions_SID, t.StudentMissions_MID });
            
            CreateTable(
                "dbo.StudentMissions",
                c => new
                    {
                        SID = c.String(nullable: false, maxLength: 128),
                        MID = c.String(nullable: false, maxLength: 128),
                        selfAssessment_SEID = c.Int(),
                    })
                .PrimaryKey(t => new { t.SID, t.MID })
                .ForeignKey("dbo.SelfAssessments", t => t.selfAssessment_SEID)
                .Index(t => t.selfAssessment_SEID);
            
            CreateTable(
                "dbo.PeerAssessments",
                c => new
                    {
                        PEID = c.Int(nullable: false, identity: true),
                        PeerA = c.String(nullable: false),
                        AssessedSID = c.String(nullable: false),
                        StudentMissions_SID = c.String(maxLength: 128),
                        StudentMissions_MID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PEID)
                .ForeignKey("dbo.StudentMissions", t => new { t.StudentMissions_SID, t.StudentMissions_MID })
                .Index(t => new { t.StudentMissions_SID, t.StudentMissions_MID });
            
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
            
            CreateTable(
                "dbo.KnowledgePointMissions",
                c => new
                    {
                        KnowledgePoint_KID = c.Int(nullable: false),
                        Mission_MID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.KnowledgePoint_KID, t.Mission_MID })
                .ForeignKey("dbo.KnowledgePoints", t => t.KnowledgePoint_KID, cascadeDelete: true)
                .ForeignKey("dbo.Missions", t => t.Mission_MID, cascadeDelete: true)
                .Index(t => t.KnowledgePoint_KID)
                .Index(t => t.Mission_MID);
            
            CreateTable(
                "dbo.StudentMission1",
                c => new
                    {
                        Student_SID = c.String(nullable: false, maxLength: 128),
                        Student_CID = c.String(nullable: false, maxLength: 128),
                        Mission_MID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Student_SID, t.Student_CID, t.Mission_MID })
                .ForeignKey("dbo.Students", t => new { t.Student_SID, t.Student_CID }, cascadeDelete: true)
                .ForeignKey("dbo.Missions", t => t.Mission_MID, cascadeDelete: true)
                .Index(t => new { t.Student_SID, t.Student_CID })
                .Index(t => t.Mission_MID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentMissions", "selfAssessment_SEID", "dbo.SelfAssessments");
            DropForeignKey("dbo.PeerAssessments", new[] { "StudentMissions_SID", "StudentMissions_MID" }, "dbo.StudentMissions");
            DropForeignKey("dbo.LearningBehaviors", new[] { "StudentMissions_SID", "StudentMissions_MID" }, "dbo.StudentMissions");
            DropForeignKey("dbo.Courses", "TID", "dbo.Teachers");
            DropForeignKey("dbo.StudentMission1", "Mission_MID", "dbo.Missions");
            DropForeignKey("dbo.StudentMission1", new[] { "Student_SID", "Student_CID" }, "dbo.Students");
            DropForeignKey("dbo.TeacherAssessments", "group_GID", "dbo.Groups");
            DropForeignKey("dbo.Students", "group_GID", "dbo.Groups");
            DropForeignKey("dbo.Groups", "MID", "dbo.Missions");
            DropForeignKey("dbo.Students", "CID", "dbo.Courses");
            DropForeignKey("dbo.Prompts", "MID", "dbo.Missions");
            DropForeignKey("dbo.KnowledgePointMissions", "Mission_MID", "dbo.Missions");
            DropForeignKey("dbo.KnowledgePointMissions", "KnowledgePoint_KID", "dbo.KnowledgePoints");
            DropForeignKey("dbo.Missions", "CID", "dbo.Courses");
            DropIndex("dbo.StudentMission1", new[] { "Mission_MID" });
            DropIndex("dbo.StudentMission1", new[] { "Student_SID", "Student_CID" });
            DropIndex("dbo.KnowledgePointMissions", new[] { "Mission_MID" });
            DropIndex("dbo.KnowledgePointMissions", new[] { "KnowledgePoint_KID" });
            DropIndex("dbo.PeerAssessments", new[] { "StudentMissions_SID", "StudentMissions_MID" });
            DropIndex("dbo.StudentMissions", new[] { "selfAssessment_SEID" });
            DropIndex("dbo.LearningBehaviors", new[] { "StudentMissions_SID", "StudentMissions_MID" });
            DropIndex("dbo.TeacherAssessments", new[] { "group_GID" });
            DropIndex("dbo.Groups", new[] { "MID" });
            DropIndex("dbo.Students", new[] { "group_GID" });
            DropIndex("dbo.Students", new[] { "CID" });
            DropIndex("dbo.Prompts", new[] { "MID" });
            DropIndex("dbo.Missions", new[] { "CID" });
            DropIndex("dbo.Courses", new[] { "TID" });
            DropTable("dbo.StudentMission1");
            DropTable("dbo.KnowledgePointMissions");
            DropTable("dbo.SelfAssessments");
            DropTable("dbo.PeerAssessments");
            DropTable("dbo.StudentMissions");
            DropTable("dbo.LearningBehaviors");
            DropTable("dbo.Teachers");
            DropTable("dbo.TeacherAssessments");
            DropTable("dbo.Groups");
            DropTable("dbo.Students");
            DropTable("dbo.Prompts");
            DropTable("dbo.KnowledgePoints");
            DropTable("dbo.Missions");
            DropTable("dbo.Courses");
            DropTable("dbo.Admins");
        }
    }
}
