namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class missionAddIs_Journey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Missions", "Is_Journey", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Missions", "Is_Journey");
        }
    }
}
