namespace LectioService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedModel : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApplicationUserCourses", newName: "CourseApplicationUsers");
            DropPrimaryKey("dbo.CourseApplicationUsers");
            AddColumn("dbo.Videos", "VideoName", c => c.String());
            AddColumn("dbo.Videos", "VideoUrl", c => c.String());
            AddColumn("dbo.Videos", "ThumbnailUrl", c => c.String());
            AddColumn("dbo.Videos", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Comments", "UserId", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.CourseApplicationUsers", new[] { "Course_CourseId", "ApplicationUser_Id" });
            CreateIndex("dbo.Comments", "VideoId");
            CreateIndex("dbo.Comments", "UserId");
            CreateIndex("dbo.Videos", "UserId");
            AddForeignKey("dbo.Videos", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Comments", "VideoId", "dbo.Videos", "VideoId", cascadeDelete: true);
            DropColumn("dbo.Videos", "Url");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Videos", "Url", c => c.String());
            DropForeignKey("dbo.Comments", "VideoId", "dbo.Videos");
            DropForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Videos", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Videos", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "VideoId" });
            DropPrimaryKey("dbo.CourseApplicationUsers");
            AlterColumn("dbo.Comments", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.Videos", "UserId");
            DropColumn("dbo.Videos", "ThumbnailUrl");
            DropColumn("dbo.Videos", "VideoUrl");
            DropColumn("dbo.Videos", "VideoName");
            AddPrimaryKey("dbo.CourseApplicationUsers", new[] { "ApplicationUser_Id", "Course_CourseId" });
            RenameTable(name: "dbo.CourseApplicationUsers", newName: "ApplicationUserCourses");
        }
    }
}
