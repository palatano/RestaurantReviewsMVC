using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
        public ActionResult Create(int restId)
        {
            RestaurantReview restaurantReview = new RestaurantReview
            {
                Restaurant_RestaurantId = restId
            };
            TempData["ID"] = restId;    
            return View(restaurantReview);
        }

        // POST: RestaurantReviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Rating,Comment,DateRated")] RestaurantReview restaurantReview)
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

        // GET: RestaurantReviews/Edit/5
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
        public ActionResult Edit([Bind(Include = "Id,Rating,Comment,DateRated")] RestaurantReview restaurantReview)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurantReview).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restaurantReview);
        }

        // GET: RestaurantReviews/Delete/5
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
