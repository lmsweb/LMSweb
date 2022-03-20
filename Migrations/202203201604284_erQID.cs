namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class erQID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EvalutionResponses", "QID", "dbo.Questions");
            DropForeignKey("dbo.EvalutionResponses", "DefaultQuestion_DQID", "dbo.DefaultQuestions");
            DropIndex("dbo.EvalutionResponses", new[] { "QID" });
            DropIndex("dbo.EvalutionResponses", new[] { "DefaultQuestion_DQID" });
            RenameColumn(table: "dbo.EvalutionResponses", name: "DefaultQuestion_DQID", newName: "DQID");
            AlterColumn("dbo.EvalutionResponses", "DQID", c => c.Int(nullable: false));
            CreateIndex("dbo.EvalutionResponses", "DQID");
            AddForeignKey("dbo.EvalutionResponses", "DQID", "dbo.DefaultQuestions", "DQID", cascadeDelete: true);
            DropColumn("dbo.EvalutionResponses", "QID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EvalutionResponses", "QID", c => c.Int(nullable: false));
            DropForeignKey("dbo.EvalutionResponses", "DQID", "dbo.DefaultQuestions");
            DropIndex("dbo.EvalutionResponses", new[] { "DQID" });
            AlterColumn("dbo.EvalutionResponses", "DQID", c => c.Int());
            RenameColumn(table: "dbo.EvalutionResponses", name: "DQID", newName: "DefaultQuestion_DQID");
            CreateIndex("dbo.EvalutionResponses", "DefaultQuestion_DQID");
            CreateIndex("dbo.EvalutionResponses", "QID");
            AddForeignKey("dbo.EvalutionResponses", "DefaultQuestion_DQID", "dbo.DefaultQuestions", "DQID");
            AddForeignKey("dbo.EvalutionResponses", "QID", "dbo.Questions", "QID", cascadeDelete: true);
        }
    }
}
