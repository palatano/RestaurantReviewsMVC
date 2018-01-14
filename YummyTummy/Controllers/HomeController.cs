using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YummyTummy.Models;

namespace YummyTummy.Controllers
{

    public class HomeController : Controller
    {
        
        
        public ActionResult Index()//Action Methods
        {
            ViewBag.Name = "Chik-fil-e";// C# 4.0
            ViewData["Location"] = "Reston VA";
            TempData["Country"] = "USA";
            return View();
        }
        //public string Index()
        //{
        //    return "Hello World";
        // }

        public ContentResult About()
        {
            ViewBag.Message = "Your application description page.";

            return Content("This Application gives restuarants with best reviews");
        }
      

        public ViewResult Contact()
        {
            ViewBag.Message = "Contact Whole Foods";
            RestaurantAddress restaurantAddress = new RestaurantAddress()
            {
                City = "Reston",
                Country = "US",
                ZipCode = "10594",
                Email = "reston@va.org",
                Phone = "92391082309",
                State = "VA",
                Street = "Market St"
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