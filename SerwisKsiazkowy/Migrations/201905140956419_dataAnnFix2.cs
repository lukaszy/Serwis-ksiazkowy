namespace SerwisKsiazkowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dataAnnFix2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Title", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Books", "Author", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Books", "Publisher", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Books", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "CoverFileName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "CoverFileName", c => c.String());
            AlterColumn("dbo.Books", "Description", c => c.String());
            AlterColumn("dbo.Books", "Publisher", c => c.String(maxLength: 200));
            AlterColumn("dbo.Books", "Author", c => c.String(maxLength: 200));
            AlterColumn("dbo.Books", "Title", c => c.String(maxLength: 100));
        }
    }
}
