namespace SerwisKsiazkowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ratings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rates",
                c => new
                    {
                        RateId = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.RateId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.BookId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Reviews", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Reviews", "ApplicationUser_Id");
            AddForeignKey("dbo.Reviews", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Rates", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Rates", "BookId", "dbo.Books");
            DropIndex("dbo.Reviews", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Rates", new[] { "UserId" });
            DropIndex("dbo.Rates", new[] { "BookId" });
            DropColumn("dbo.Reviews", "ApplicationUser_Id");
            DropTable("dbo.Rates");
        }
    }
}
