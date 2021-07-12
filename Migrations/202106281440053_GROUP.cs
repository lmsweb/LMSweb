namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GROUP : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LearningBehaviors", "Group_GName", "dbo.Groups");
            DropForeignKey("dbo.Students", "Group_GName", "dbo.Groups");
            DropForeignKey("dbo.PeerAssessments", "Group_GName", "dbo.Groups");
            DropForeignKey("dbo.SelfAssessments", "Group_GName", "dbo.Groups");
            DropForeignKey("dbo.Students", "Group_GName1", "dbo.Groups");
            DropForeignKey("dbo.TeacherAssessments", "Group_GName", "dbo.Groups");
            DropIndex("dbo.LearningBehaviors", new[] { "Group_GName" });
            DropIndex("dbo.Students", new[] { "Group_GName" });
            DropIndex("dbo.Students", new[] { "Group_GName1" });
            DropIndex("dbo.PeerAssessments", new[] { "Group_GName" });
            DropIndex("dbo.SelfAssessments", new[] { "Group_GName" });
            DropIndex("dbo.TeacherAssessments", new[] { "Group_GName" });
            RenameColumn(table: "dbo.LearningBehaviors", name: "Group_GName", newName: "Group_GID");
            RenameColumn(table: "dbo.PeerAssessments", name: "Group_GName", newName: "Group_GID");
            RenameColumn(table: "dbo.SelfAssessments", name: "Group_GName", newName: "Group_GID");
            RenameColumn(table: "dbo.Students", name: "Group_GName1", newName: "Group_GID1");
            RenameColumn(table: "dbo.TeacherAssessments", name: "Group_GName", newName: "Group_GID");
            RenameColumn(table: "dbo.Students", name: "Group_GName", newName: "Group_GID");
            DropPrimaryKey("dbo.Groups");
            AlterColumn("dbo.Groups", "GName", c => c.String(nullable: false));
            AlterColumn("dbo.Groups", "GID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Groups", "Position", c => c.String());
            AlterColumn("dbo.LearningBehaviors", "Group_GID", c => c.Int());
            AlterColumn("dbo.Students", "Group_GID", c => c.Int());
            AlterColumn("dbo.Students", "Group_GID1", c => c.Int());
            AlterColumn("dbo.PeerAssessments", "Group_GID", c => c.Int());
            AlterColumn("dbo.SelfAssessments", "Group_GID", c => c.Int());
            AlterColumn("dbo.TeacherAssessments", "Group_GID", c => c.Int());
            AddPrimaryKey("dbo.Groups", "GID");
            CreateIndex("dbo.LearningBehaviors", "Group_GID");
            CreateIndex("dbo.Students", "Group_GID");
            CreateIndex("dbo.Students", "Group_GID1");
            CreateIndex("dbo.PeerAssessments", "Group_GID");
            CreateIndex("dbo.SelfAssessments", "Group_GID");
            CreateIndex("dbo.TeacherAssessments", "Group_GID");
            AddForeignKey("dbo.LearningBehaviors", "Group_GID", "dbo.Groups", "GID");
            AddForeignKey("dbo.Students", "Group_GID", "dbo.Groups", "GID");
            AddForeignKey("dbo.PeerAssessments", "Group_GID", "dbo.Groups", "GID");
            AddForeignKey("dbo.SelfAssessments", "Group_GID", "dbo.Groups", "GID");
            AddForeignKey("dbo.Students", "Group_GID1", "dbo.Groups", "GID");
            AddForeignKey("dbo.TeacherAssessments", "Group_GID", "dbo.Groups", "GID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherAssessments", "Group_GID", "dbo.Groups");
            DropForeignKey("dbo.Students", "Group_GID1", "dbo.Groups");
            DropForeignKey("dbo.SelfAssessments", "Group_GID", "dbo.Groups");
            DropForeignKey("dbo.PeerAssessments", "Group_GID", "dbo.Groups");
            DropForeignKey("dbo.Students", "Group_GID", "dbo.Groups");
            DropForeignKey("dbo.LearningBehaviors", "Group_GID", "dbo.Groups");
            DropIndex("dbo.TeacherAssessments", new[] { "Group_GID" });
            DropIndex("dbo.SelfAssessments", new[] { "Group_GID" });
            DropIndex("dbo.PeerAssessments", new[] { "Group_GID" });
            DropIndex("dbo.Students", new[] { "Group_GID1" });
            DropIndex("dbo.Students", new[] { "Group_GID" });
            DropIndex("dbo.LearningBehaviors", new[] { "Group_GID" });
            DropPrimaryKey("dbo.Groups");
            AlterColumn("dbo.TeacherAssessments", "Group_GID", c => c.String(maxLength: 128));
            AlterColumn("dbo.SelfAssessments", "Group_GID", c => c.String(maxLength: 128));
            AlterColumn("dbo.PeerAssessments", "Group_GID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Students", "Group_GID1", c => c.String(maxLength: 128));
            AlterColumn("dbo.Students", "Group_GID", c => c.String(maxLength: 128));
            AlterColumn("dbo.LearningBehaviors", "Group_GID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Groups", "Position", c => c.String(nullable: false));
            AlterColumn("dbo.Groups", "GID", c => c.Int(nullable: false));
            AlterColumn("dbo.Groups", "GName", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Groups", "GName");
            RenameColumn(table: "dbo.Students", name: "Group_GID", newName: "Group_GName");
            RenameColumn(table: "dbo.TeacherAssessments", name: "Group_GID", newName: "Group_GName");
            RenameColumn(table: "dbo.Students", name: "Group_GID1", newName: "Group_GName1");
            RenameColumn(table: "dbo.SelfAssessments", name: "Group_GID", newName: "Group_GName");
            RenameColumn(table: "dbo.PeerAssessments", name: "Group_GID", newName: "Group_GName");
            RenameColumn(table: "dbo.LearningBehaviors", name: "Group_GID", newName: "Group_GName");
            CreateIndex("dbo.TeacherAssessments", "Group_GName");
            CreateIndex("dbo.SelfAssessments", "Group_GName");
            CreateIndex("dbo.PeerAssessments", "Group_GName");
            CreateIndex("dbo.Students", "Group_GName1");
            CreateIndex("dbo.Students", "Group_GName");
            CreateIndex("dbo.LearningBehaviors", "Group_GName");
            AddForeignKey("dbo.TeacherAssessments", "Group_GName", "dbo.Groups", "GName");
            AddForeignKey("dbo.Students", "Group_GName1", "dbo.Groups", "GName");
            AddForeignKey("dbo.SelfAssessments", "Group_GName", "dbo.Groups", "GName");
            AddForeignKey("dbo.PeerAssessments", "Group_GName", "dbo.Groups", "GName");
            AddForeignKey("dbo.Students", "Group_GName", "dbo.Groups", "GName");
            AddForeignKey("dbo.LearningBehaviors", "Group_GName", "dbo.Groups", "GName");
        }
    }
}
