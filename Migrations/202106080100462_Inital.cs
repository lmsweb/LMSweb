namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inital : DbMigration
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
                "dbo.Groups",
                c => new
                    {
                        GName = c.String(nullable: false, maxLength: 128),
                        GID = c.Int(nullable: false),
                        Position = c.String(nullable: false),
                        MID = c.String(maxLength: 128),
                        Student_SID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.GName)
                .ForeignKey("dbo.Students", t => t.Student_SID)
                .ForeignKey("dbo.Missions", t => t.MID)
                .Index(t => t.MID)
                .Index(t => t.Student_SID);
            
            CreateTable(
                "dbo.LearningBehaviors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ActionType = c.String(nullable: false),
                        subAction = c.String(nullable: false),
                        Detail = c.String(nullable: false),
                        Time = c.String(),
                        MID = c.String(maxLength: 128),
                        SID = c.String(maxLength: 128),
                        GID = c.String(),
                        Group_GName = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Groups", t => t.Group_GName)
                .ForeignKey("dbo.Missions", t => t.MID)
                .ForeignKey("dbo.Students", t => t.SID)
                .Index(t => t.MID)
                .Index(t => t.SID)
                .Index(t => t.Group_GName);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        SID = c.String(nullable: false, maxLength: 128),
                        SName = c.String(nullable: false),
                        SPassword = c.String(nullable: false, maxLength: 18),
                        Sex = c.String(nullable: false),
                        Stage = c.String(nullable: false),
                        Grade = c.String(nullable: false),
                        Score = c.String(),
                        CID = c.String(maxLength: 128),
                        GID = c.String(),
                        Group_GName = c.String(maxLength: 128),
                        Group_GName1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.SID)
                .ForeignKey("dbo.Courses", t => t.CID)
                .ForeignKey("dbo.Groups", t => t.Group_GName)
                .ForeignKey("dbo.Groups", t => t.Group_GName1)
                .Index(t => t.CID)
                .Index(t => t.Group_GName)
                .Index(t => t.Group_GName1);
            
            CreateTable(
                "dbo.PeerAssessments",
                c => new
                    {
                        PEID = c.Int(nullable: false, identity: true),
                        PeerA = c.String(nullable: false),
                        AssessedSID = c.String(nullable: false),
                        MID = c.String(maxLength: 128),
                        SID = c.String(maxLength: 128),
                        GID = c.String(),
                        Group_GName = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PEID)
                .ForeignKey("dbo.Groups", t => t.Group_GName)
                .ForeignKey("dbo.Missions", t => t.MID)
                .ForeignKey("dbo.Students", t => t.SID)
                .Index(t => t.MID)
                .Index(t => t.SID)
                .Index(t => t.Group_GName);
            
            CreateTable(
                "dbo.SelfAssessments",
                c => new
                    {
                        SEID = c.Int(nullable: false, identity: true),
                        CooperationLevel = c.Int(nullable: false),
                        PersonalContributionLevel = c.Int(nullable: false),
                        SelfA = c.Int(nullable: false),
                        MID = c.String(maxLength: 128),
                        SID = c.String(maxLength: 128),
                        GID = c.String(),
                        Group_GName = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.SEID)
                .ForeignKey("dbo.Groups", t => t.Group_GName)
                .ForeignKey("dbo.Missions", t => t.MID)
                .ForeignKey("dbo.Students", t => t.SID)
                .Index(t => t.MID)
                .Index(t => t.SID)
                .Index(t => t.Group_GName);
            
            CreateTable(
                "dbo.TeacherAssessments",
                c => new
                    {
                        TEID = c.Int(nullable: false, identity: true),
                        TeacherA = c.String(nullable: false),
                        GroupAchievementLevel = c.Int(nullable: false),
                        MID = c.String(maxLength: 128),
                        GID = c.String(),
                        Group_GName = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TEID)
                .ForeignKey("dbo.Groups", t => t.Group_GName)
                .ForeignKey("dbo.Missions", t => t.MID)
                .Index(t => t.MID)
                .Index(t => t.Group_GName);
            
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
                "dbo.StudentGroups",
                c => new
                    {
                        SID = c.String(nullable: false, maxLength: 128),
                        MID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.SID, t.MID })
                .ForeignKey("dbo.Missions", t => t.MID, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.SID, cascadeDelete: true)
                .Index(t => t.SID)
                .Index(t => t.MID);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentGroups", "SID", "dbo.Students");
            DropForeignKey("dbo.StudentGroups", "MID", "dbo.Missions");
            DropForeignKey("dbo.Courses", "TID", "dbo.Teachers");
            DropForeignKey("dbo.Prompts", "MID", "dbo.Missions");
            DropForeignKey("dbo.KnowledgePointMissions", "Mission_MID", "dbo.Missions");
            DropForeignKey("dbo.KnowledgePointMissions", "KnowledgePoint_KID", "dbo.KnowledgePoints");
            DropForeignKey("dbo.TeacherAssessments", "MID", "dbo.Missions");
            DropForeignKey("dbo.TeacherAssessments", "Group_GName", "dbo.Groups");
            DropForeignKey("dbo.Students", "Group_GName1", "dbo.Groups");
            DropForeignKey("dbo.Groups", "MID", "dbo.Missions");
            DropForeignKey("dbo.SelfAssessments", "SID", "dbo.Students");
            DropForeignKey("dbo.SelfAssessments", "MID", "dbo.Missions");
            DropForeignKey("dbo.SelfAssessments", "Group_GName", "dbo.Groups");
            DropForeignKey("dbo.PeerAssessments", "SID", "dbo.Students");
            DropForeignKey("dbo.PeerAssessments", "MID", "dbo.Missions");
            DropForeignKey("dbo.PeerAssessments", "Group_GName", "dbo.Groups");
            DropForeignKey("dbo.LearningBehaviors", "SID", "dbo.Students");
            DropForeignKey("dbo.Groups", "Student_SID", "dbo.Students");
            DropForeignKey("dbo.Students", "Group_GName", "dbo.Groups");
            DropForeignKey("dbo.Students", "CID", "dbo.Courses");
            DropForeignKey("dbo.LearningBehaviors", "MID", "dbo.Missions");
            DropForeignKey("dbo.LearningBehaviors", "Group_GName", "dbo.Groups");
            DropForeignKey("dbo.Missions", "CID", "dbo.Courses");
            DropIndex("dbo.KnowledgePointMissions", new[] { "Mission_MID" });
            DropIndex("dbo.KnowledgePointMissions", new[] { "KnowledgePoint_KID" });
            DropIndex("dbo.StudentGroups", new[] { "MID" });
            DropIndex("dbo.StudentGroups", new[] { "SID" });
            DropIndex("dbo.Prompts", new[] { "MID" });
            DropIndex("dbo.TeacherAssessments", new[] { "Group_GName" });
            DropIndex("dbo.TeacherAssessments", new[] { "MID" });
            DropIndex("dbo.SelfAssessments", new[] { "Group_GName" });
            DropIndex("dbo.SelfAssessments", new[] { "SID" });
            DropIndex("dbo.SelfAssessments", new[] { "MID" });
            DropIndex("dbo.PeerAssessments", new[] { "Group_GName" });
            DropIndex("dbo.PeerAssessments", new[] { "SID" });
            DropIndex("dbo.PeerAssessments", new[] { "MID" });
            DropIndex("dbo.Students", new[] { "Group_GName1" });
            DropIndex("dbo.Students", new[] { "Group_GName" });
            DropIndex("dbo.Students", new[] { "CID" });
            DropIndex("dbo.LearningBehaviors", new[] { "Group_GName" });
            DropIndex("dbo.LearningBehaviors", new[] { "SID" });
            DropIndex("dbo.LearningBehaviors", new[] { "MID" });
            DropIndex("dbo.Groups", new[] { "Student_SID" });
            DropIndex("dbo.Groups", new[] { "MID" });
            DropIndex("dbo.Missions", new[] { "CID" });
            DropIndex("dbo.Courses", new[] { "TID" });
            DropTable("dbo.KnowledgePointMissions");
            DropTable("dbo.StudentGroups");
            DropTable("dbo.Teachers");
            DropTable("dbo.Prompts");
            DropTable("dbo.KnowledgePoints");
            DropTable("dbo.TeacherAssessments");
            DropTable("dbo.SelfAssessments");
            DropTable("dbo.PeerAssessments");
            DropTable("dbo.Students");
            DropTable("dbo.LearningBehaviors");
            DropTable("dbo.Groups");
            DropTable("dbo.Missions");
            DropTable("dbo.Courses");
            DropTable("dbo.Admins");
        }
    }
}
