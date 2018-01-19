using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using YummyTummy.Models;

namespace YummyTummy.Controllers
{
    public class RestaurantReviewsController : Controller
    {
        private RestaurantDbContext db = new RestaurantDbContext();

        // GET: RestaurantReviews
        public ActionResult Index(int restId)
        {
            Restaurant restaurant = db.Restaurants
                .Include("Review")
                .Where(res => res.RestaurantId == restId)
                .FirstOrDefault();
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants
        [HttpPost]
        public ActionResult Index(string search, int restId)
        {
            // Treat query search as case insensitive.
            search = search.ToLower();

            // From our database, we need to get the restaurants and their 
            // addresses and reviews (which are child tables).
            var reviews = db.Reviews.Where(rev => rev.Restaurant_RestaurantId == restId);

            // By using reflection, we can simply get the properties for each restaurant.
            IList<RestaurantReview> filteredList = new List<RestaurantReview>();
            foreach (var field in reviews)
            {
                var props = field.GetType().GetProperties();
                foreach (PropertyInfo prop in props)
                {
                    var propVal = prop.GetValue(field);
                    // Otherwise, check the restauranat's property and it's value if 
                    // the search query is inside one of the values as a string.
                    string propValStr = Convert.ToString(propVal).ToLower();
                    if (propValStr.Contains(search) && !filteredList.Contains(field))
                    {
                        filteredList.Add(field);
                    }
                }
            }
            ViewData["reviews"] = filteredList;
            Restaurant rest = db.Restaurants.Find(restId);
            return View(rest);
        }

        // GET: RestaurantReviews/Details/5
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantReview restaurantReview = db.Reviews.Find(id);
            if (restaurantReview == null)
            {
                return HttpNotFound(); 
            }
            return View(restaurantReview);
        }

        // GET: RestaurantReviews/Create
        [Authorize]
        public ActionResult Create(int restId)
        {
            RestaurantReview restaurantReview = new RestaurantReview
            {
                Restaurant_RestaurantId = restId,
                ReviewerId = User.Identity.GetUserId(),
                ReviewerName = User.Identity.GetFullName()
                
            };
            TempData["ID"] = restId;
            return View(restaurantReview);
        }

        // POST: RestaurantReviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ReviewerName, Rating,Comment,DateRated, Restaurant_RestaurantId, ReviewerId")] RestaurantReview restaurantReview)
        {
            int restaurId = (int)TempData["ID"];

            if (ModelState.IsValid)
            {
                restaurantReview.Restaurant_RestaurantId = restaurId;
                db.Reviews.Add(restaurantReview);
                db.SaveChanges();
                return RedirectToAction("Index", new { restId = restaurId });
            }

            return View(restaurantReview);
        }

        // GET: Restaurants/OrderBy
        [ActionName("OrderBy")]
        public ActionResult OrderBy(string orderResult, int restId)
        {
            // We want to find the right way to order our restaurants, so
            // we can use L2E to get all the results with the ORDER BY clause.
            var restaurant = db.Restaurants
                .Include("RestaurantAddress")
                .Include("Review")
                .First(rest => rest.RestaurantId == restId);
            IOrderedEnumerable<RestaurantReview> resultSet = null;
            if (orderResult == "Date Ascending")
            {
                resultSet = from rev in restaurant.Review
                            orderby rev.DateRated
                            select rev;
            }
            else if (orderResult == "Date Descending")
            {
                resultSet = from rev in restaurant.Review
                            orderby rev.DateRated descending
                            select rev;
            }
            else if (orderResult == "Rating Ascending")
            {
                resultSet = restaurant.Review.Select(rev => rev).OrderBy(rev => rev.Rating);
            }
            else if (orderResult == "Rating Descending")
            {
                resultSet = restaurant.Review.Select(rev => rev).OrderByDescending(rev => rev.Rating);
            }
            else
            {
                
                return View("Index", restaurant);
            }
            ViewData["reviews"] = resultSet;
            return View("Index", restaurant);
        }

        // GET: RestaurantReviews/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantReview restaurantReview = db.Reviews.Find(id);
            if (restaurantReview == null)
            {
                return HttpNotFound();
            }
            return View(restaurantReview);
        }

        // POST: RestaurantReviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,ReviewerName, Rating,Comment,DateRated, Restaurant_RestaurantId, ReviewerId ")] RestaurantReview restaurantReview)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurantReview).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { restId = restaurantReview.Restaurant_RestaurantId });
            }
            return View(restaurantReview);
        }

        // GET: RestaurantReviews/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantReview restaurantReview = db.Reviews.Find(id);
            if (restaurantReview == null)
            {
                return HttpNotFound();
            }
            return View(restaurantReview);
        }

        // POST: RestaurantReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RestaurantReview restaurantReview = db.Reviews.Find(id);
            db.Reviews.Remove(restaurantReview);
            db.SaveChanges();
            return RedirectToAction("Index", new { restId = restaurantReview.Restaurant_RestaurantId });
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
