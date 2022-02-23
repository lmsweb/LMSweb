namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentLBmission : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Mission_MID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Students", "SPassword", c => c.String(nullable: false));
            CreateIndex("dbo.Students", "Mission_MID");
            AddForeignKey("dbo.Students", "Mission_MID", "dbo.Missions", "MID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "Mission_MID", "dbo.Missions");
            DropIndex("dbo.Students", new[] { "Mission_MID" });
            AlterColumn("dbo.Students", "SPassword", c => c.String(nullable: false, maxLength: 18));
            DropColumn("dbo.Students", "Mission_MID");
        }
    }
}
