namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DOIDint : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DefaultOptions", "DefaultQuestion_DQID", "dbo.DefaultQuestions");
            DropIndex("dbo.DefaultOptions", new[] { "DefaultQuestion_DQID" });
            DropColumn("dbo.DefaultOptions", "DQID");
            RenameColumn(table: "dbo.DefaultOptions", name: "DefaultQuestion_DQID", newName: "DQID");
            AlterColumn("dbo.DefaultOptions", "DQID", c => c.Int(nullable: false));
            AlterColumn("dbo.DefaultOptions", "DQID", c => c.Int(nullable: false));
            CreateIndex("dbo.DefaultOptions", "DQID");
            AddForeignKey("dbo.DefaultOptions", "DQID", "dbo.DefaultQuestions", "DQID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DefaultOptions", "DQID", "dbo.DefaultQuestions");
            DropIndex("dbo.DefaultOptions", new[] { "DQID" });
            AlterColumn("dbo.DefaultOptions", "DQID", c => c.Int());
            AlterColumn("dbo.DefaultOptions", "DQID", c => c.String());
            RenameColumn(table: "dbo.DefaultOptions", name: "DQID", newName: "DefaultQuestion_DQID");
            AddColumn("dbo.DefaultOptions", "DQID", c => c.String());
            CreateIndex("dbo.DefaultOptions", "DefaultQuestion_DQID");
            AddForeignKey("dbo.DefaultOptions", "DefaultQuestion_DQID", "dbo.DefaultQuestions", "DQID");
        }
    }
}
