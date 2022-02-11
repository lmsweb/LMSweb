namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class del_k : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Missions", "chart_k");
            DropColumn("dbo.Missions", "code_k");
            DropColumn("dbo.Missions", "eva_k");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Missions", "eva_k", c => c.Int(nullable: false));
            AddColumn("dbo.Missions", "code_k", c => c.Int(nullable: false));
            AddColumn("dbo.Missions", "chart_k", c => c.Int(nullable: false));
        }
    }
}
