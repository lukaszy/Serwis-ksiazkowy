namespace SerwisKsiazkowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userId_comments : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Comments", name: "ApplicationUser_Id", newName: "UserId");
            RenameIndex(table: "dbo.Comments", name: "IX_ApplicationUser_Id", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Comments", name: "IX_UserId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Comments", name: "UserId", newName: "ApplicationUser_Id");
        }
    }
}
