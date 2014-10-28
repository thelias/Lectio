namespace LectioService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "TimeStamp", c => c.DateTime(nullable: false));
            Sql("UPDATE [dbo].[Comments] SET TimeStamp = GETUTCDATE() WHERE TimeStamp IS NULL");
            AlterColumn("dbo.Comments", "TimeStamp", c => c.DateTime(nullable: false, defaultValueSql: "GETUTCDATE()"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "TimeStamp", c => c.DateTime());
            DropColumn("dbo.Comments", "TimeStamp");
        }
    }
}
