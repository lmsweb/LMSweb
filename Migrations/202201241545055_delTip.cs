namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delTip : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Missions", "Tip");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Missions", "Tip", c => c.String());
        }
    }
}
