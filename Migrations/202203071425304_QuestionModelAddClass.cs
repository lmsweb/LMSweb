namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionModelAddClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "Class", c => c.String());
            AddColumn("dbo.DefaultQuestions", "Class", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DefaultQuestions", "Class");
            DropColumn("dbo.Questions", "Class");
        }
    }
}
