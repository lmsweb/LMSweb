namespace LMSweb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddefaultQ : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DefaultOptions",
                c => new
                    {
                        DOID = c.String(nullable: false, maxLength: 128),
                        DQID = c.String(maxLength: 128),
                        DefaultOptions = c.String(),
                    })
                .PrimaryKey(t => t.DOID)
                .ForeignKey("dbo.DefaultQuestions", t => t.DQID)
                .Index(t => t.DQID);
            
            CreateTable(
                "dbo.DefaultQuestions",
                c => new
                    {
                        DQID = c.String(nullable: false, maxLength: 128),
                        Type = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.DQID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DefaultOptions", "DQID", "dbo.DefaultQuestions");
            DropIndex("dbo.DefaultOptions", new[] { "DQID" });
            DropTable("dbo.DefaultQuestions");
            DropTable("dbo.DefaultOptions");
        }
    }
}
