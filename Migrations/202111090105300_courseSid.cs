namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class courseSid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "SID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "SID");
        }
    }
}
