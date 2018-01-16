namespace YummyTummy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RestaurantAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Street = c.String(nullable: false, maxLength: 25),
                        City = c.String(nullable: false, maxLength: 25),
                        State = c.String(nullable: false, maxLength: 10),
                        Country = c.String(nullable: false, maxLength: 5),
                        ZipCode = c.String(nullable: false, maxLength: 5),
                        Phone = c.String(nullable: false, maxLength: 25),
                        Email = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        RestaurantId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 25),
                        RestaurantAddress_Id = c.Int(),
                    })
                .PrimaryKey(t => t.RestaurantId)
                .ForeignKey("dbo.RestaurantAddresses", t => t.RestaurantAddress_Id)
                .Index(t => t.RestaurantAddress_Id);
            
            CreateTable(
                "dbo.RestaurantReviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comment = c.String(nullable: false, maxLength: 1024),
                        DateRated = c.DateTime(nullable: false),
                        ReviewerName = c.String(nullable: false, maxLength: 25),
                        Restaurant_RestaurantId = c.Int(nullable: false),
                        ReviewerId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurants", t => t.Restaurant_RestaurantId, cascadeDelete: true)
                .Index(t => t.Restaurant_RestaurantId);
            
            CreateTable(
                "dbo.TestHelpers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 25),
                        Confirm = c.Boolean(nullable: false),
                        Age = c.Int(nullable: false),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comments = c.String(),
                        DOB = c.DateTime(nullable: false),
                        Photo = c.Binary(),
                        Email = c.String(),
                        ZipCode = c.String(),
                        Phone = c.String(),
                        ssn = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RestaurantReviews", "Restaurant_RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.Restaurants", "RestaurantAddress_Id", "dbo.RestaurantAddresses");
            DropIndex("dbo.RestaurantReviews", new[] { "Restaurant_RestaurantId" });
            DropIndex("dbo.Restaurants", new[] { "RestaurantAddress_Id" });
            DropTable("dbo.TestHelpers");
            DropTable("dbo.RestaurantReviews");
            DropTable("dbo.Restaurants");
            DropTable("dbo.RestaurantAddresses");
        }
    }
}
