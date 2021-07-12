namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "CID", "dbo.Courses");
            DropForeignKey("dbo.LearningBehaviors", "SID", "dbo.Students");
            DropForeignKey("dbo.Groups", "Student_SID", "dbo.Students");
            DropForeignKey("dbo.PeerAssessments", "SID", "dbo.Students");
            DropForeignKey("dbo.SelfAssessments", "SID", "dbo.Students");
            DropForeignKey("dbo.StudentGroups", "SID", "dbo.Students");
            DropIndex("dbo.Groups", new[] { "Student_SID" });
            DropIndex("dbo.LearningBehaviors", new[] { "SID" });
            DropIndex("dbo.Students", new[] { "CID" });
            DropIndex("dbo.PeerAssessments", new[] { "SID" });
            DropIndex("dbo.SelfAssessments", new[] { "SID" });
            DropIndex("dbo.StudentGroups", new[] { "SID" });
            DropPrimaryKey("dbo.Students");
            AddColumn("dbo.Groups", "Student_CID", c => c.String(maxLength: 128));
            AddColumn("dbo.LearningBehaviors", "Student_SID", c => c.String(maxLength: 128));
            AddColumn("dbo.LearningBehaviors", "Student_CID", c => c.String(maxLength: 128));
            AddColumn("dbo.PeerAssessments", "Student_SID", c => c.String(maxLength: 128));
            AddColumn("dbo.PeerAssessments", "Student_CID", c => c.String(maxLength: 128));
            AddColumn("dbo.SelfAssessments", "Student_SID", c => c.String(maxLength: 128));
            AddColumn("dbo.SelfAssessments", "Student_CID", c => c.String(maxLength: 128));
            AddColumn("dbo.StudentGroups", "Student_SID", c => c.String(maxLength: 128));
            AddColumn("dbo.StudentGroups", "Student_CID", c => c.String(maxLength: 128));
            AlterColumn("dbo.LearningBehaviors", "SID", c => c.String());
            AlterColumn("dbo.Students", "CID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.PeerAssessments", "SID", c => c.String());
            AlterColumn("dbo.SelfAssessments", "SID", c => c.String());
            AddPrimaryKey("dbo.Students", new[] { "SID", "CID" });
            CreateIndex("dbo.Groups", new[] { "Student_SID", "Student_CID" });
            CreateIndex("dbo.LearningBehaviors", new[] { "Student_SID", "Student_CID" });
            CreateIndex("dbo.Students", "CID");
            CreateIndex("dbo.PeerAssessments", new[] { "Student_SID", "Student_CID" });
            CreateIndex("dbo.SelfAssessments", new[] { "Student_SID", "Student_CID" });
            CreateIndex("dbo.StudentGroups", new[] { "Student_SID", "Student_CID" });
            AddForeignKey("dbo.Students", "CID", "dbo.Courses", "CID", cascadeDelete: true);
            AddForeignKey("dbo.LearningBehaviors", new[] { "Student_SID", "Student_CID" }, "dbo.Students", new[] { "SID", "CID" });
            AddForeignKey("dbo.Groups", new[] { "Student_SID", "Student_CID" }, "dbo.Students", new[] { "SID", "CID" });
            AddForeignKey("dbo.PeerAssessments", new[] { "Student_SID", "Student_CID" }, "dbo.Students", new[] { "SID", "CID" });
            AddForeignKey("dbo.SelfAssessments", new[] { "Student_SID", "Student_CID" }, "dbo.Students", new[] { "SID", "CID" });
            AddForeignKey("dbo.StudentGroups", new[] { "Student_SID", "Student_CID" }, "dbo.Students", new[] { "SID", "CID" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentGroups", new[] { "Student_SID", "Student_CID" }, "dbo.Students");
            DropForeignKey("dbo.SelfAssessments", new[] { "Student_SID", "Student_CID" }, "dbo.Students");
            DropForeignKey("dbo.PeerAssessments", new[] { "Student_SID", "Student_CID" }, "dbo.Students");
            DropForeignKey("dbo.Groups", new[] { "Student_SID", "Student_CID" }, "dbo.Students");
            DropForeignKey("dbo.LearningBehaviors", new[] { "Student_SID", "Student_CID" }, "dbo.Students");
            DropForeignKey("dbo.Students", "CID", "dbo.Courses");
            DropIndex("dbo.StudentGroups", new[] { "Student_SID", "Student_CID" });
            DropIndex("dbo.SelfAssessments", new[] { "Student_SID", "Student_CID" });
            DropIndex("dbo.PeerAssessments", new[] { "Student_SID", "Student_CID" });
            DropIndex("dbo.Students", new[] { "CID" });
            DropIndex("dbo.LearningBehaviors", new[] { "Student_SID", "Student_CID" });
            DropIndex("dbo.Groups", new[] { "Student_SID", "Student_CID" });
            DropPrimaryKey("dbo.Students");
            AlterColumn("dbo.SelfAssessments", "SID", c => c.String(maxLength: 128));
            AlterColumn("dbo.PeerAssessments", "SID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Students", "CID", c => c.String(maxLength: 128));
            AlterColumn("dbo.LearningBehaviors", "SID", c => c.String(maxLength: 128));
            DropColumn("dbo.StudentGroups", "Student_CID");
            DropColumn("dbo.StudentGroups", "Student_SID");
            DropColumn("dbo.SelfAssessments", "Student_CID");
            DropColumn("dbo.SelfAssessments", "Student_SID");
            DropColumn("dbo.PeerAssessments", "Student_CID");
            DropColumn("dbo.PeerAssessments", "Student_SID");
            DropColumn("dbo.LearningBehaviors", "Student_CID");
            DropColumn("dbo.LearningBehaviors", "Student_SID");
            DropColumn("dbo.Groups", "Student_CID");
            AddPrimaryKey("dbo.Students", "SID");
            CreateIndex("dbo.StudentGroups", "SID");
            CreateIndex("dbo.SelfAssessments", "SID");
            CreateIndex("dbo.PeerAssessments", "SID");
            CreateIndex("dbo.Students", "CID");
            CreateIndex("dbo.LearningBehaviors", "SID");
            CreateIndex("dbo.Groups", "Student_SID");
            AddForeignKey("dbo.StudentGroups", "SID", "dbo.Students", "SID", cascadeDelete: true);
            AddForeignKey("dbo.SelfAssessments", "SID", "dbo.Students", "SID");
            AddForeignKey("dbo.PeerAssessments", "SID", "dbo.Students", "SID");
            AddForeignKey("dbo.Groups", "Student_SID", "dbo.Students", "SID");
            AddForeignKey("dbo.LearningBehaviors", "SID", "dbo.Students", "SID");
            AddForeignKey("dbo.Students", "CID", "dbo.Courses", "CID");
        }
    }
}
