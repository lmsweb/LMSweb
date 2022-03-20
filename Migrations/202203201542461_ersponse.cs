namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ersponse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EvalutionResponses", "MID", c => c.String(maxLength: 128));
            AddColumn("dbo.EvalutionResponses", "DefaultQuestion_DQID", c => c.Int());
            CreateIndex("dbo.EvalutionResponses", "MID");
            CreateIndex("dbo.EvalutionResponses", "DefaultQuestion_DQID");
            AddForeignKey("dbo.EvalutionResponses", "MID", "dbo.Missions", "MID");
            AddForeignKey("dbo.EvalutionResponses", "DefaultQuestion_DQID", "dbo.DefaultQuestions", "DQID");
            DropColumn("dbo.EvalutionResponses", "Comments");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EvalutionResponses", "Comments", c => c.String());
            DropForeignKey("dbo.EvalutionResponses", "DefaultQuestion_DQID", "dbo.DefaultQuestions");
            DropForeignKey("dbo.EvalutionResponses", "MID", "dbo.Missions");
            DropIndex("dbo.EvalutionResponses", new[] { "DefaultQuestion_DQID" });
            DropIndex("dbo.EvalutionResponses", new[] { "MID" });
            DropColumn("dbo.EvalutionResponses", "DefaultQuestion_DQID");
            DropColumn("dbo.EvalutionResponses", "MID");
        }
    }
}
