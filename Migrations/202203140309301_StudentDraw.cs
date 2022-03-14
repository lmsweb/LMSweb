namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentDraw : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentDraws",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DrawingImgPath = c.String(),
                        Course_CID = c.String(maxLength: 128),
                        Group_GID = c.Int(),
                        Mission_MID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_CID)
                .ForeignKey("dbo.Groups", t => t.Group_GID)
                .ForeignKey("dbo.Missions", t => t.Mission_MID)
                .Index(t => t.Course_CID)
                .Index(t => t.Group_GID)
                .Index(t => t.Mission_MID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentDraws", "Mission_MID", "dbo.Missions");
            DropForeignKey("dbo.StudentDraws", "Group_GID", "dbo.Groups");
            DropForeignKey("dbo.StudentDraws", "Course_CID", "dbo.Courses");
            DropIndex("dbo.StudentDraws", new[] { "Mission_MID" });
            DropIndex("dbo.StudentDraws", new[] { "Group_GID" });
            DropIndex("dbo.StudentDraws", new[] { "Course_CID" });
            DropTable("dbo.StudentDraws");
        }
    }
}
