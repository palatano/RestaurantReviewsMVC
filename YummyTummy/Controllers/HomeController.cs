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