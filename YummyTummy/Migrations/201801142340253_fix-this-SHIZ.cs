namespace YummyTummy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixthisSHIZ : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RestaurantReviews", "Restaurant_RestaurantId", "dbo.Restaurants");
            DropIndex("dbo.RestaurantReviews", new[] { "Restaurant_RestaurantId" });
            AlterColumn("dbo.RestaurantReviews", "Restaurant_RestaurantId", c => c.Int(nullable: false));
            CreateIndex("dbo.RestaurantReviews", "Restaurant_RestaurantId");
            AddForeignKey("dbo.RestaurantReviews", "Restaurant_RestaurantId", "dbo.Restaurants", "RestaurantId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RestaurantReviews", "Restaurant_RestaurantId", "dbo.Restaurants");
            DropIndex("dbo.RestaurantReviews", new[] { "Restaurant_RestaurantId" });
            AlterColumn("dbo.RestaurantReviews", "Restaurant_RestaurantId", c => c.Int());
            CreateIndex("dbo.RestaurantReviews", "Restaurant_RestaurantId");
            AddForeignKey("dbo.RestaurantReviews", "Restaurant_RestaurantId", "dbo.Restaurants", "RestaurantId");
        }
    }
}
