using Microsoft.Owin;
using Owin;
using System;
using YummyTummy.Models;

[assembly: OwinStartupAttribute(typeof(YummyTummy.Startup))]
namespace YummyTummy
{
    

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

    
           
            using (var ctx = new RestaurantDbContext())
            {

                ConfigureAuth(app);
            }
        }
    }
}
