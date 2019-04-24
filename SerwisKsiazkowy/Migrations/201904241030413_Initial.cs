namespace SerwisKsiazkowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                {
                    BookId = c.Int(nullable: false, identity: true),
                    GenreId = c.Int(nullable: false),
                    Title = c.String(),
                    Author = c.String(),
                    Pages = c.Int(nullable: false),
                    YearPublished = c.DateTime(nullable: false),
                    Publisher = c.String(),
                    Description = c.String(),
                    Rate = c.Int(nullable: false),
                    CoverFileName = c.String(),
                })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.GenreId);

            CreateTable(
                "dbo.Comments",
                c => new
                {
                    CommentId = c.Int(nullable: false, identity: true),
                    Content = c.String(),
                    DateAdded = c.DateTime(nullable: false),
                    BookId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId);

            CreateTable(
                "dbo.Genres",
                c => new
                {
                    GenreId = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.GenreId);

            CreateTable(
                "dbo.Reviews",
                c => new
                {
                    ReviewId = c.Int(nullable: false, identity: true),
                    Content = c.String(),
                    BookId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ReviewId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId);

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "BookId", "dbo.Books");
            DropForeignKey("dbo.Books", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Comments", "BookId", "dbo.Books");
            DropIndex("dbo.Reviews", new[] { "BookId" });
            DropIndex("dbo.Comments", new[] { "BookId" });
            DropIndex("dbo.Books", new[] { "GenreId" });
            DropTable("dbo.Reviews");
            DropTable("dbo.Genres");
            DropTable("dbo.Comments");
            DropTable("dbo.Books");
        }
    }
}
