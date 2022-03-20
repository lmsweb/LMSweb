namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DelQuestionResponse : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Responses", "Question_QID", "dbo.Questions");
            DropIndex("dbo.Responses", new[] { "Question_QID" });
            DropColumn("dbo.Responses", "Question_QID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Responses", "Question_QID", c => c.Int());
            CreateIndex("dbo.Responses", "Question_QID");
            AddForeignKey("dbo.Responses", "Question_QID", "dbo.Questions", "QID");
        }
    }
}
