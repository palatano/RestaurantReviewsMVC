namespace YummyTummy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class willthiswork1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RestaurantReviews", "RestaurantId", "dbo.Restaurants");
            DropIndex("dbo.RestaurantReviews", new[] { "RestaurantId" });
            RenameColumn(table: "dbo.RestaurantReviews", name: "RestaurantId", newName: "Restaurant_RestaurantId");
            AlterColumn("dbo.RestaurantReviews", "Restaurant_RestaurantId", c => c.Int());
            CreateIndex("dbo.RestaurantReviews", "Restaurant_RestaurantId");
            AddForeignKey("dbo.RestaurantReviews", "Restaurant_RestaurantId", "dbo.Restaurants", "RestaurantId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RestaurantReviews", "Restaurant_RestaurantId", "dbo.Restaurants");
            DropIndex("dbo.RestaurantReviews", new[] { "Restaurant_RestaurantId" });
            AlterColumn("dbo.RestaurantReviews", "Restaurant_RestaurantId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.RestaurantReviews", name: "Restaurant_RestaurantId", newName: "RestaurantId");
            CreateIndex("dbo.RestaurantReviews", "RestaurantId");
            AddForeignKey("dbo.RestaurantReviews", "RestaurantId", "dbo.Restaurants", "RestaurantId", cascadeDelete: true);
        }
    }
}
