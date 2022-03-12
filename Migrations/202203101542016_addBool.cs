namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addBool : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Missions", "IsGoalSetting", c => c.Boolean(nullable: false));
            AddColumn("dbo.Missions", "IsReflect", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Missions", "IsReflect");
            DropColumn("dbo.Missions", "IsGoalSetting");
        }
    }
}
