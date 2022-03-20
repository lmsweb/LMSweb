namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class courseAddcheckfunction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "IsAddMetacognition", c => c.Boolean(nullable: false));
            AddColumn("dbo.Courses", "IsAddPeerAssessmemt", c => c.Boolean(nullable: false));
            DropColumn("dbo.Missions", "AddMetacognition");
            DropColumn("dbo.Missions", "discuss_k");
            DropColumn("dbo.Missions", "per_k");
            DropColumn("dbo.Missions", "group_k");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Missions", "group_k", c => c.Int(nullable: false));
            AddColumn("dbo.Missions", "per_k", c => c.Int(nullable: false));
            AddColumn("dbo.Missions", "discuss_k", c => c.Int(nullable: false));
            AddColumn("dbo.Missions", "AddMetacognition", c => c.Boolean(nullable: false));
            DropColumn("dbo.Courses", "IsAddPeerAssessmemt");
            DropColumn("dbo.Courses", "IsAddMetacognition");
        }
    }
}
