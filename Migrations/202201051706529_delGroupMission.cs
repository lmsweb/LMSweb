namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delGroupMission : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Missions", "Group_GID", "dbo.Groups");
            DropIndex("dbo.Missions", new[] { "Group_GID" });
            DropColumn("dbo.Missions", "Group_GID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Missions", "Group_GID", c => c.Int());
            CreateIndex("dbo.Missions", "Group_GID");
            AddForeignKey("dbo.Missions", "Group_GID", "dbo.Groups", "GID");
        }
    }
}
