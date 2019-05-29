namespace SerwisKsiazkowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AvgRatingInBook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "AvgRating", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "AvgRating");
        }
    }
}
