namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class responseAddMID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Responses", "QID", "dbo.Questions");
            DropIndex("dbo.Responses", new[] { "QID" });
            RenameColumn(table: "dbo.Responses", name: "QID", newName: "Question_QID");
            AddColumn("dbo.Responses", "DQID", c => c.Int(nullable: false));
            AddColumn("dbo.Responses", "MID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Responses", "Question_QID", c => c.Int());
            CreateIndex("dbo.Responses", "DQID");
            CreateIndex("dbo.Responses", "MID");
            CreateIndex("dbo.Responses", "Question_QID");
            AddForeignKey("dbo.Responses", "DQID", "dbo.DefaultQuestions", "DQID", cascadeDelete: true);
            AddForeignKey("dbo.Responses", "MID", "dbo.Missions", "MID");
            AddForeignKey("dbo.Responses", "Question_QID", "dbo.Questions", "QID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Responses", "Question_QID", "dbo.Questions");
            DropForeignKey("dbo.Responses", "MID", "dbo.Missions");
            DropForeignKey("dbo.Responses", "DQID", "dbo.DefaultQuestions");
            DropIndex("dbo.Responses", new[] { "Question_QID" });
            DropIndex("dbo.Responses", new[] { "MID" });
            DropIndex("dbo.Responses", new[] { "DQID" });
            AlterColumn("dbo.Responses", "Question_QID", c => c.Int(nullable: false));
            DropColumn("dbo.Responses", "MID");
            DropColumn("dbo.Responses", "DQID");
            RenameColumn(table: "dbo.Responses", name: "Question_QID", newName: "QID");
            CreateIndex("dbo.Responses", "QID");
            AddForeignKey("dbo.Responses", "QID", "dbo.Questions", "QID", cascadeDelete: true);
        }
    }
}
