namespace YummyTummy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thisfinthingdeletedmyfile6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Restaurants", "Name", c => c.String(nullable: false, maxLength: 25));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Restaurants", "Name", c => c.String(nullable: false));
        }
    }
}
