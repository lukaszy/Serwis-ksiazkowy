namespace SerwisKsiazkowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dataAnnFix22 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "CoverFileName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "CoverFileName", c => c.String(nullable: false));
        }
    }
}
