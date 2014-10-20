namespace LectioService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class threadmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Threads",
                c => new
                    {
                        ThreadId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ThreadId);
            
            AddColumn("dbo.Comments", "ThreadId", c => c.Int(nullable: false));
            AddColumn("dbo.Videos", "ThreadId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "ThreadId");
            CreateIndex("dbo.Videos", "ThreadId");
            AddForeignKey("dbo.Comments", "ThreadId", "dbo.Threads", "ThreadId", cascadeDelete: true);
            AddForeignKey("dbo.Videos", "ThreadId", "dbo.Threads", "ThreadId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Videos", "ThreadId", "dbo.Threads");
            DropForeignKey("dbo.Comments", "ThreadId", "dbo.Threads");
            DropIndex("dbo.Videos", new[] { "ThreadId" });
            DropIndex("dbo.Comments", new[] { "ThreadId" });
            DropColumn("dbo.Videos", "ThreadId");
            DropColumn("dbo.Comments", "ThreadId");
            DropTable("dbo.Threads");
        }
    }
}
