using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YummyTummy.Models;
using YummyTummy.Util;

namespace YummyTummy.Controllers
{

    public class HomeController : Controller
    {
        private RestaurantDbContext db = new RestaurantDbContext();

        public ActionResult Index()//Action Methods
        {
            var rests = AnalysisUtil.TopThreeRatedRestaurants(db);
            return View(rests);
        }

        public ActionResult About()
        {
            ViewBag.Message = "This restaurant site is purely made for demonstration purposes.";

            return View();
        }
      

        public ViewResult Contact()
        {
            ViewBag.Message = "Contact Restauranteview At: ";
            RestaurantAddress restaurantAddress = new RestaurantAddress()
            {
                City = "Reston",
                Country = "US",
                ZipCode = "20190",
                Email = "reston@va.org",
                Phone = "92391082309",
                State = "VA",
                Street = "11730 Plaza America Dr #205"
            };
            return View("Contact", restaurantAddress);
        }

        public ViewResult Support()
        {
            return View();
        }

        public ViewResult Offers()
        {
            return View();
        }

        public ViewResult TestView()
        {
            return View();
        }

    }
}