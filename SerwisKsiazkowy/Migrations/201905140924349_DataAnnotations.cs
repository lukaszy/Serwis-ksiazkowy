namespace SerwisKsiazkowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "Content", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.Reviews", "Content", c => c.String(nullable: false, maxLength: 2000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "Content", c => c.String());
            AlterColumn("dbo.Comments", "Content", c => c.String());
        }
    }
}
