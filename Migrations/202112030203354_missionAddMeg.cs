namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class missionAddMeg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Missions", "AddMetacognition", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Missions", "AddMetacognition");
        }
    }
}
