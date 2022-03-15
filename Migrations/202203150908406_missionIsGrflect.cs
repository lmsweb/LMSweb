namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class missionIsGrflect : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Missions", "IsGReflect", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Missions", "IsGReflect");
        }
    }
}
