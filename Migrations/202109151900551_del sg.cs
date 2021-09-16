namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delsg : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Groups", new[] { "StudentGroup_SID", "StudentGroup_GID" }, "dbo.StudentGroups");
            DropForeignKey("dbo.Students", new[] { "StudentGroup_SID", "StudentGroup_GID" }, "dbo.StudentGroups");
            DropIndex("dbo.Students", new[] { "StudentGroup_SID", "StudentGroup_GID" });
            DropIndex("dbo.Groups", new[] { "StudentGroup_SID", "StudentGroup_GID" });
            DropColumn("dbo.Students", "StudentGroup_SID");
            DropColumn("dbo.Students", "StudentGroup_GID");
            DropColumn("dbo.Groups", "StudentGroup_SID");
            DropColumn("dbo.Groups", "StudentGroup_GID");
            DropTable("dbo.StudentGroups");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.StudentGroups",
                c => new
                    {
                        SID = c.String(nullable: false, maxLength: 128),
                        GID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.SID, t.GID });
            
            AddColumn("dbo.Groups", "StudentGroup_GID", c => c.String(maxLength: 128));
            AddColumn("dbo.Groups", "StudentGroup_SID", c => c.String(maxLength: 128));
            AddColumn("dbo.Students", "StudentGroup_GID", c => c.String(maxLength: 128));
            AddColumn("dbo.Students", "StudentGroup_SID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Groups", new[] { "StudentGroup_SID", "StudentGroup_GID" });
            CreateIndex("dbo.Students", new[] { "StudentGroup_SID", "StudentGroup_GID" });
            AddForeignKey("dbo.Students", new[] { "StudentGroup_SID", "StudentGroup_GID" }, "dbo.StudentGroups", new[] { "SID", "GID" });
            AddForeignKey("dbo.Groups", new[] { "StudentGroup_SID", "StudentGroup_GID" }, "dbo.StudentGroups", new[] { "SID", "GID" });
        }
    }
}
