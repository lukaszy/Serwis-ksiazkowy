namespace SerwisKsiazkowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRatings : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Rates", newName: "Ratings");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Ratings", newName: "Rates");
        }
    }
}
