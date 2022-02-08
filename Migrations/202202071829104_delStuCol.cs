namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delStuCol : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Students", "Grade");
            DropColumn("dbo.Students", "Stage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "Stage", c => c.String(nullable: false));
            AddColumn("dbo.Students", "Grade", c => c.String(nullable: false));
        }
    }
}
