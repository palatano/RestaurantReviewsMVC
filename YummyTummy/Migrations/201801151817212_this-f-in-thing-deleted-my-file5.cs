namespace YummyTummy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thisfinthingdeletedmyfile5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RestaurantReviews", "ReviewerName", c => c.String(nullable: false, maxLength: 25));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RestaurantReviews", "ReviewerName");
        }
    }
}
