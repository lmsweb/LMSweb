namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDefaultQuestionKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DefaultOptions", "DQID", "dbo.DefaultQuestions");
            DropIndex("dbo.DefaultOptions", new[] { "DQID" });
            DropPrimaryKey("dbo.DefaultOptions");
            DropPrimaryKey("dbo.DefaultQuestions");
            AddColumn("dbo.DefaultOptions", "DefaultQuestion_DQID", c => c.Int());
            AlterColumn("dbo.DefaultOptions", "DOID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.DefaultOptions", "DQID", c => c.String());
            AlterColumn("dbo.DefaultQuestions", "DQID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.DefaultOptions", "DOID");
            AddPrimaryKey("dbo.DefaultQuestions", "DQID");
            CreateIndex("dbo.DefaultOptions", "DefaultQuestion_DQID");
            AddForeignKey("dbo.DefaultOptions", "DefaultQuestion_DQID", "dbo.DefaultQuestions", "DQID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DefaultOptions", "DefaultQuestion_DQID", "dbo.DefaultQuestions");
            DropIndex("dbo.DefaultOptions", new[] { "DefaultQuestion_DQID" });
            DropPrimaryKey("dbo.DefaultQuestions");
            DropPrimaryKey("dbo.DefaultOptions");
            AlterColumn("dbo.DefaultQuestions", "DQID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.DefaultOptions", "DQID", c => c.String(maxLength: 128));
            AlterColumn("dbo.DefaultOptions", "DOID", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.DefaultOptions", "DefaultQuestion_DQID");
            AddPrimaryKey("dbo.DefaultQuestions", "DQID");
            AddPrimaryKey("dbo.DefaultOptions", "DOID");
            CreateIndex("dbo.DefaultOptions", "DQID");
            AddForeignKey("dbo.DefaultOptions", "DQID", "dbo.DefaultQuestions", "DQID");
        }
    }
}
