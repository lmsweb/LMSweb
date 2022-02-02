namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class peerassessmentGID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PeerAssessments", "Group_GID", c => c.Int());
            CreateIndex("dbo.PeerAssessments", "Group_GID");
            AddForeignKey("dbo.PeerAssessments", "Group_GID", "dbo.Groups", "GID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PeerAssessments", "Group_GID", "dbo.Groups");
            DropIndex("dbo.PeerAssessments", new[] { "Group_GID" });
            DropColumn("dbo.PeerAssessments", "Group_GID");
        }
    }
}
