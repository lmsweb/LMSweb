namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QOculName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Options", "OptionName", c => c.String());
            AddColumn("dbo.DefaultOptions", "DOptions", c => c.String());
            DropColumn("dbo.Options", "Options");
            DropColumn("dbo.DefaultOptions", "DefaultOptions");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DefaultOptions", "DefaultOptions", c => c.String());
            AddColumn("dbo.Options", "Options", c => c.String());
            DropColumn("dbo.DefaultOptions", "DOptions");
            DropColumn("dbo.Options", "OptionName");
        }
    }
}
