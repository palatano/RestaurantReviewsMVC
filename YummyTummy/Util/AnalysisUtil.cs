﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YummyTummy.Models;

namespace YummyTummy.Util
{
    public static class AnalysisUtil
    {
        // TODO: Should I have a static field for utilities?
        private static RestaurantDbContext db = new RestaurantDbContext();
        ///
        /// <summary>
        /// Used as a property for the index view, to find the top three rated restaurants
        /// based on their average rating.
        /// </summary>
        ///
        public static IEnumerable<Restaurant> TopThreeRatedRestaurants
        {
            get
            {
                // We need a comparer to track the ratings, as well as the size.
                SortedSet<Restaurant> avgRatingSet = new SortedSet<Restaurant>();
                int size = 0;
                var restaurants = db.Restaurants.Include("RestaurantAddress").Include("Review");
                foreach (var rest in restaurants)
                {
                    // We want to check the average rating, if it exists (not N/A). 
                    string ratingStr = rest.AvgRating;
                    if (!decimal.TryParse(ratingStr, out decimal avgRating))
                    {
                        continue; 
                    }

                    // Now, we want to check if the size is greater than three, so that 
                    // we can keep the three highest ratings.
                    if (size >= 3)
                    {
                        Restaurant restLowest = avgRatingSet.Min<Restaurant>();
                        decimal restLowestRating = decimal.Parse(restLowest.AvgRating);
                        if (avgRating >= restLowestRating)
                        {
                            avgRatingSet.Remove(restLowest);
                            avgRatingSet.Add(rest);
                        }
                    }
                    // Otherwise, just add the restaurant. 
                    else
                    {
                        avgRatingSet.Add(rest);
                        size++;
                    }
                }
                //var orderedSet = avgRatingSet.OrderBy(r => r.AvgRating);
                return avgRatingSet.Reverse<Restaurant>();
            }
        }
    }
}