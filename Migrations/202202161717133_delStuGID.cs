namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delStuGID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Students", "GID", "dbo.Groups");
            DropIndex("dbo.Students", new[] { "GID" });
            RenameColumn(table: "dbo.Students", name: "GID", newName: "group_GID");
            AlterColumn("dbo.Students", "group_GID", c => c.Int());
            CreateIndex("dbo.Students", "group_GID");
            AddForeignKey("dbo.Students", "group_GID", "dbo.Groups", "GID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "group_GID", "dbo.Groups");
            DropIndex("dbo.Students", new[] { "group_GID" });
            AlterColumn("dbo.Students", "group_GID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Students", name: "group_GID", newName: "GID");
            CreateIndex("dbo.Students", "GID");
            AddForeignKey("dbo.Students", "GID", "dbo.Groups", "GID", cascadeDelete: true);
        }
    }
}
