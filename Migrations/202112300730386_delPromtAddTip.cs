namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delPromtAddTip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Missions", "Tip", c => c.String());
            AddColumn("dbo.KnowledgePoints", "CID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.KnowledgePoints", "CID");
            DropColumn("dbo.Missions", "Tip");
        }
    }
}
