namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditMissionKAssessment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Missions", "group_k", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Missions", "group_k");
        }
    }
}
