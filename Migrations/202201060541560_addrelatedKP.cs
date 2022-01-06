namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrelatedKP : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Missions", "relatedKP", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Missions", "relatedKP");
        }
    }
}
