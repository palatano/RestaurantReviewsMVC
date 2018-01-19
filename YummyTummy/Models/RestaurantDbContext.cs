using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YummyTummy.Models
{

    public interface IYummyTummy : IDisposable
    {
        IQueryable<T> Query<T>() where T : class;
        void Add<T>(T Entity) where T : class;
        void Update<T>(T Entity) where T : class;
        void Remove<T>(T Entity) where T : class;
        void SaveChanges();
    }

    public class RestaurantDbContext:DbContext, IYummyTummy
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

        void IYummyTummy.Add<T>(T Entity)
        {
            Set<T>().Add(Entity);
        }

        IQueryable<T> IYummyTummy.Query<T>()
        {
            return Set<T>();
        }


        void IYummyTummy.Remove<T>(T entity)
        {
            Set<T>().Remove(entity);
        }

        void IYummyTummy.SaveChanges()
        {
            SaveChanges();
        }

        void IYummyTummy.Update<T>(T entity)
        {
            Entry(entity).State = EntityState.Modified;
        }
    }

    class DbInitializer : CreateDatabaseIfNotExists<RestaurantDbContext> 
    {
        protected override void Seed(RestaurantDbContext context)
        {
            base.Seed(context);
        }
    }
}