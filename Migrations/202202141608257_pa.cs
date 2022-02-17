namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PeerAssessments", "MID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PeerAssessments", "MID");
        }
    }
}
