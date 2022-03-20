namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Responses", "SID", "dbo.Students");
            DropIndex("dbo.Responses", new[] { "SID" });
            AlterColumn("dbo.Responses", "Answer", c => c.String());
            AlterColumn("dbo.Responses", "SID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Responses", "SID");
            AddForeignKey("dbo.Responses", "SID", "dbo.Students", "SID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Responses", "SID", "dbo.Students");
            DropIndex("dbo.Responses", new[] { "SID" });
            AlterColumn("dbo.Responses", "SID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Responses", "Answer", c => c.String(nullable: false));
            CreateIndex("dbo.Responses", "SID");
            AddForeignKey("dbo.Responses", "SID", "dbo.Students", "SID", cascadeDelete: true);
        }
    }
}
