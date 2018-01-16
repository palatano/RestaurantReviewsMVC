using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YummyTummy.Models
{
    [NotMapped]
    public class MyReviewsViewModel
    {
        public Restaurant Restaurant { get; set; }
        public RestaurantReview RestaurantReview { get; set; }
    }
}