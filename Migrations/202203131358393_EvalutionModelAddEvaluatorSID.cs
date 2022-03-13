namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EvalutionModelAddEvaluatorSID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EvalutionResponses", "EvaluatorSID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EvalutionResponses", "EvaluatorSID");
        }
    }
}
