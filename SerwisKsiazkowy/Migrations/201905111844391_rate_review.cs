namespace SerwisKsiazkowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rate_review : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Reviews", name: "ApplicationUser_Id", newName: "UserId");
            RenameIndex(table: "dbo.Reviews", name: "IX_ApplicationUser_Id", newName: "IX_UserId");
            DropPrimaryKey("dbo.Rates");
            AddColumn("dbo.Reviews", "DateAdded", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Rates", "RateId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Rates", "RateId");
            CreateIndex("dbo.Rates", "RateId");
            AddForeignKey("dbo.Rates", "RateId", "dbo.Reviews", "ReviewId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rates", "RateId", "dbo.Reviews");
            DropIndex("dbo.Rates", new[] { "RateId" });
            DropPrimaryKey("dbo.Rates");
            AlterColumn("dbo.Rates", "RateId", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Reviews", "DateAdded");
            AddPrimaryKey("dbo.Rates", "RateId");
            RenameIndex(table: "dbo.Reviews", name: "IX_UserId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Reviews", name: "UserId", newName: "ApplicationUser_Id");
        }
    }
}
