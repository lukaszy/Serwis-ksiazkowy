namespace SerwisKsiazkowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Review_addRateValue : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rates", "RateId", "dbo.Reviews");
            DropIndex("dbo.Rates", new[] { "RateId" });
            DropPrimaryKey("dbo.Rates");
            AddColumn("dbo.Reviews", "RateValue", c => c.Int(nullable: false));
            AlterColumn("dbo.Rates", "RateId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Rates", "RateId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Rates");
            AlterColumn("dbo.Rates", "RateId", c => c.Int(nullable: false));
            DropColumn("dbo.Reviews", "RateValue");
            AddPrimaryKey("dbo.Rates", "RateId");
            CreateIndex("dbo.Rates", "RateId");
            AddForeignKey("dbo.Rates", "RateId", "dbo.Reviews", "ReviewId");
        }
    }
}
