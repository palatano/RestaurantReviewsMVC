using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace YummyTummy.Models
{
    public class RestaurantDbContext:DbContext
    {
        public RestaurantDbContext()
            :base("ResturantDb")
        {
            Database.SetInitializer<RestaurantDbContext>(new DbInitializer());
        }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantReview> Reviews { get; set; }
        public DbSet<RestaurantAddress> Addresses { get; set; }

        public System.Data.Entity.DbSet<YummyTummy.Models.TestHelpers> TestHelpers { get; set; }
    }

    class DbInitializer : CreateDatabaseIfNotExists<RestaurantDbContext> 
    {
        protected override void Seed(RestaurantDbContext context)
        {
            base.Seed(context);
        }
    }
}