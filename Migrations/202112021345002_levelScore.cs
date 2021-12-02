namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class levelScore : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentMissions", "total_score", c => c.Int(nullable: false));
            AddColumn("dbo.Missions", "discuss_k", c => c.Int(nullable: false));
            AddColumn("dbo.Missions", "chart_k", c => c.Int(nullable: false));
            AddColumn("dbo.Missions", "code_k", c => c.Int(nullable: false));
            AddColumn("dbo.Missions", "eva_k", c => c.Int(nullable: false));
            AddColumn("dbo.Missions", "per_k", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Missions", "per_k");
            DropColumn("dbo.Missions", "eva_k");
            DropColumn("dbo.Missions", "code_k");
            DropColumn("dbo.Missions", "chart_k");
            DropColumn("dbo.Missions", "discuss_k");
            DropColumn("dbo.StudentMissions", "total_score");
        }
    }
}
