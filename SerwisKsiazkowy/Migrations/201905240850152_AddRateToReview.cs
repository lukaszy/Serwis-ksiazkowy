namespace SerwisKsiazkowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRateToReview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "Rate_RateId", c => c.Int());
            CreateIndex("dbo.Reviews", "Rate_RateId");
            AddForeignKey("dbo.Reviews", "Rate_RateId", "dbo.Ratings", "RateId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "Rate_RateId", "dbo.Ratings");
            DropIndex("dbo.Reviews", new[] { "Rate_RateId" });
            DropColumn("dbo.Reviews", "Rate_RateId");
        }
    }
}
