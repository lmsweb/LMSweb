namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseAddTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "CreateTime", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "CreateTime");
        }
    }
}
