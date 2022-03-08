namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MissionAddBool : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Missions", "IsDrawing", c => c.Boolean(nullable: false));
            AddColumn("dbo.Missions", "IsCoding", c => c.Boolean(nullable: false));
            AddColumn("dbo.Missions", "IsDiscuss", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Missions", "IsDiscuss");
            DropColumn("dbo.Missions", "IsCoding");
            DropColumn("dbo.Missions", "IsDrawing");
        }
    }
}
