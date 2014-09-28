namespace LectioService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        VideoId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        CommentText = c.String(),
                    })
                .PrimaryKey(t => t.CommentId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CourseName = c.String(),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        VideoId = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        TimeStamp = c.DateTime(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VideoId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.ApplicationUserCourses",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Course_CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Course_CourseId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_CourseId, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Course_CourseId);
            
            AddColumn("dbo.AspNetUsers", "Role_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "Role_Id");
            AddForeignKey("dbo.AspNetUsers", "Role_Id", "dbo.AspNetRoles", "Id");

            Sql("UPDATE [dbo].[Videos] SET TimeStamp = GETUTCDATE() WHERE TimeStamp IS NULL");
            AlterColumn("dbo.Videos", "TimeStamp", c => c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Videos", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.AspNetUsers", "Role_Id", "dbo.AspNetRoles");
            DropForeignKey("dbo.ApplicationUserCourses", "Course_CourseId", "dbo.Courses");
            DropForeignKey("dbo.ApplicationUserCourses", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserCourses", new[] { "Course_CourseId" });
            DropIndex("dbo.ApplicationUserCourses", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Videos", new[] { "CourseId" });
            DropIndex("dbo.AspNetUsers", new[] { "Role_Id" });
            DropColumn("dbo.AspNetUsers", "Role_Id");
            DropTable("dbo.ApplicationUserCourses");
            DropTable("dbo.Videos");
            DropTable("dbo.Courses");
            DropTable("dbo.Comments");
            AlterColumn("dbo.Videos", "TimeStamp", c => c.DateTime());
        }
    }
}
