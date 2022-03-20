namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddResponseRequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Responses", "SID", "dbo.Students");
            DropIndex("dbo.Responses", new[] { "SID" });
            AlterColumn("dbo.Responses", "Answer", c => c.String(nullable: false));
            AlterColumn("dbo.Responses", "SID", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Responses", "SID");
            AddForeignKey("dbo.Responses", "SID", "dbo.Students", "SID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Responses", "SID", "dbo.Students");
            DropIndex("dbo.Responses", new[] { "SID" });
            AlterColumn("dbo.Responses", "SID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Responses", "Answer", c => c.String());
            CreateIndex("dbo.Responses", "SID");
            AddForeignKey("dbo.Responses", "SID", "dbo.Students", "SID");
        }
    }
}
