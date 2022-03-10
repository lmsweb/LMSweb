namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MissionAddIsAssess : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Missions", "IsAssess", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Missions", "IsAssess");
        }
    }
}
