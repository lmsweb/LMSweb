namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCID : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Questions", name: "Course_CID", newName: "CID");
            RenameIndex(table: "dbo.Questions", name: "IX_Course_CID", newName: "IX_CID");
            AddColumn("dbo.PeerAssessments", "CID", c => c.String(maxLength: 128));
            AddColumn("dbo.Responses", "CID", c => c.String(maxLength: 128));
            CreateIndex("dbo.PeerAssessments", "CID");
            CreateIndex("dbo.Responses", "CID");
            AddForeignKey("dbo.PeerAssessments", "CID", "dbo.Courses", "CID");
            AddForeignKey("dbo.Responses", "CID", "dbo.Courses", "CID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Responses", "CID", "dbo.Courses");
            DropForeignKey("dbo.PeerAssessments", "CID", "dbo.Courses");
            DropIndex("dbo.Responses", new[] { "CID" });
            DropIndex("dbo.PeerAssessments", new[] { "CID" });
            DropColumn("dbo.Responses", "CID");
            DropColumn("dbo.PeerAssessments", "CID");
            RenameIndex(table: "dbo.Questions", name: "IX_CID", newName: "IX_Course_CID");
            RenameColumn(table: "dbo.Questions", name: "CID", newName: "Course_CID");
        }
    }
}
