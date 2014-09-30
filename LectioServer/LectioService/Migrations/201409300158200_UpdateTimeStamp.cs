namespace LectioService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTimeStamp : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE [dbo].[Videos] SET TimeStamp = GETUTCDATE() WHERE TimeStamp IS NULL");
            AlterColumn("dbo.Videos", "TimeStamp", c => c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Videos", "TimeStamp", c => c.DateTime());
        }
    }
}
