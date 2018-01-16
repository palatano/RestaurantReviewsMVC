using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using YummyTummy.Models;

namespace YummyTummy.Controllers
{
    [Authorize]
    public class MyReviewsController : Controller
    {
        private RestaurantDbContext db = new RestaurantDbContext();

        // GET: RestaurantReviews
        [Authorize]
        public ActionResult Index()
        {
            var id = User.Identity.GetUserId();

            // From each restaurant, we want to get the reviews
            // that belong to the name, so that we can get a collection
            // of reviews, in addition to their name.

            var reviews = (from rev in db.Reviews
                           join rest in db.Restaurants
                           on rev.Restaurant_RestaurantId equals rest.RestaurantId
                           where rev.ReviewerId == id
                           select new MyReviewsViewModel
                           {
                               Restaurant = rest,
                               RestaurantReview = rev
                          }).ToList();
            
            if (id == null)
            {
                return HttpNotFound();
            }
            return View(reviews);
        }

        // GET: RestaurantReviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantReview restaurantReview = db.Reviews.Find(id);
            Restaurant rest = db.Restaurants.Find(restaurantReview.Restaurant_RestaurantId);
            if (restaurantReview == null)
            {
                return HttpNotFound();
            }
            MyReviewsViewModel viewModel = new MyReviewsViewModel
            {
                RestaurantReview = restaurantReview,
                Restaurant = rest
            };
            return View(viewModel);
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
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantReview restaurantReview = db.Reviews.Find(id);
            Restaurant rest = db.Restaurants.Find(restaurantReview.Restaurant_RestaurantId);
            if (restaurantReview == null)
            {
                return HttpNotFound();
            }

            MyReviewsViewModel viewModel = new MyReviewsViewModel
            {
                RestaurantReview = restaurantReview,
                Restaurant = rest
            };
            return View(viewModel);
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
