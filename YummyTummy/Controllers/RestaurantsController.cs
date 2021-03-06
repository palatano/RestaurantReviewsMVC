﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YummyTummy.Models;
using YummyTummy.ActionFilters;
using System.Reflection;
using System.Data.Entity.SqlServer;

namespace YummyTummy.Controllers
{
    public class RestaurantsController : Controller
    {
        IYummyTummy testDb;
        private RestaurantDbContext db = new RestaurantDbContext();

        public RestaurantsController(IYummyTummy testDb)
        {
            this.testDb = testDb;
        }

        public RestaurantsController()
        {

        }

        // GET: Restaurants
        public ActionResult Index()
        {
            var restaurants = db.Restaurants.Include(r => r.RestaurantAddress).Include(r => r.Review);
            return View(restaurants.Include("RestaurantAddress").Include("Review").ToList());
        }

        public int TestAction([Bind(Include = "RestaurantId,Name, RestaurantAddress")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                testDb.Add<Restaurant>(restaurant);
                return db.SaveChanges();
            }
            return 0;
        }


        // GET: Restaurants/OrderBy
        [ActionName("OrderBy")]
        public ActionResult OrderBy(string orderResult)
        {
            // We want to find the right way to order our restaurants, so
            // we can use L2E to get all the results with the ORDER BY clause.
            var restaurants = db.Restaurants.Include("RestaurantAddress").Include("Review");
            IQueryable<Restaurant> resultSet = null;
            if (orderResult == "Name Ascending") {
                resultSet = from rest in restaurants
                            orderby rest.Name
                            select rest;
            } else if (orderResult == "Name Descending")
            {
                resultSet = from rest in restaurants
                            orderby rest.Name descending
                            select rest;
            } else if (orderResult == "Average Rating Ascending")
            {
                resultSet = restaurants.Select(rest => rest).OrderBy(rest => rest.Review.Average(rev => rev.Rating));
            } else if (orderResult == "Average Rating Descending")
            {
                resultSet = restaurants.Select(rest => rest).OrderByDescending(rest => rest.Review.Average(rev => rev.Rating));
            } else
            {
                return View("Index", restaurants);
            }
            return View("Index", resultSet.ToList());
        }

        // POST: Restaurants
        [HttpPost] 
        public ActionResult Index(string search)
        {
            // Treat query search as case insensitive.
            search = search.ToLower();

            // From our database, we need to get the restaurants and their 
            // addresses and reviews (which are child tables).
            var restaurants = db.Restaurants.Include(r => r.RestaurantAddress)
                .Include(r => r.Review);

            // By using reflection, we can simply get the properties for each restaurant.
            IList<Restaurant> filteredList = new List<Restaurant>();
            foreach (var field in restaurants)
            {
                var props = field.GetType().GetProperties();
                foreach (PropertyInfo prop in props)
                {
                    var propVal = prop.GetValue(field);
                    // Since the restaurant address is a property containing an address object,
                    // we need to iterate over each of those properties and their values.
                    if (prop.Name == "RestaurantAddress")
                    {
                        var restProps = propVal.GetType().GetProperties();
                        foreach (var restProp in restProps)
                        {
                            var restPropVal = restProp.GetValue(propVal);
                            string valStr = Convert.ToString(restPropVal).ToLower();
                            // Check if the search query is contained, as a substring,
                            // inside one of the values as a string.
                            if (valStr.Contains(search) && !filteredList.Contains(field))
                            {
                                filteredList.Add(field);
                            }
                        }
                    }
                    // Otherwise, check the restauranat's property and it's value if 
                    // the search query is inside one of the values as a string.
                    else
                    {
                        string propValStr = Convert.ToString(propVal).ToLower();
                        if (propValStr.Contains(search) && !filteredList.Contains(field))
                        {
                            filteredList.Add(field);
                        }
                    }
                }
            }
            return View(filteredList);
        }

        // GET: RestaurantsReview
        
        public ActionResult ReviewsRedir(int id)
        {
            return RedirectToAction("Index", "RestaurantReviews", new {restId = id});
        }

        

        // GET: Restaurants/Details/5
        [Log] // custom filter.
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants
                .Include("RestaurantAddress")
                .Include("Review")
                .Single(rest => rest.RestaurantId == id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // GET: Restaurants/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.RestaurantId = new SelectList(db.Addresses, "Id", "Street");
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "RestaurantId,Name, RestaurantAddress")] Restaurant restaurant)
        {
            
            if (ModelState.IsValid)
            {
                db.Restaurants.Add(restaurant);
                db.Addresses.Add(restaurant.RestaurantAddress);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RestaurantId = new SelectList(db.Addresses, "Id", "Street", restaurant.RestaurantId);
            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants
                .Include("RestaurantAddress")
                .Include("Review")
                .Single(rest => rest.RestaurantId == id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            ViewBag.RestaurantId = new SelectList(db.Addresses, "Id", "Street", restaurant.RestaurantId);
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "RestaurantId,Name, RestaurantAddress")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                db.Entry(restaurant.RestaurantAddress).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RestaurantId = new SelectList(db.Addresses, "Id", "Street", restaurant.RestaurantId);
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants
                .Include("RestaurantAddress")
                .Include("Review")
                .Single(rest => rest.RestaurantId == id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            db.Restaurants.Remove(restaurant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
