namespace SerwisKsiazkowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dataAnnFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Title", c => c.String(maxLength: 100));
            AlterColumn("dbo.Books", "Author", c => c.String(maxLength: 200));
            AlterColumn("dbo.Books", "Publisher", c => c.String(maxLength: 200));
            AlterColumn("dbo.Genres", "Name", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Genres", "Name", c => c.String());
            AlterColumn("dbo.Books", "Publisher", c => c.String());
            AlterColumn("dbo.Books", "Author", c => c.String());
            AlterColumn("dbo.Books", "Title", c => c.String());
        }
    }
}
