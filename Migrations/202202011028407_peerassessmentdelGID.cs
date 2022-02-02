namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class peerassessmentdelGID : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PeerAssessments", "GID", "dbo.Groups");
            DropIndex("dbo.PeerAssessments", new[] { "GID" });
            DropColumn("dbo.PeerAssessments", "GID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PeerAssessments", "GID", c => c.Int(nullable: false));
            CreateIndex("dbo.PeerAssessments", "GID");
            AddForeignKey("dbo.PeerAssessments", "GID", "dbo.Groups", "GID", cascadeDelete: true);
        }
    }
}
