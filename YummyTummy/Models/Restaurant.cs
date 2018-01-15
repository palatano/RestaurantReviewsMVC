
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;// for 
using System.Linq;
using System.Web;
using YummyTummy.Util;

namespace YummyTummy.Models
{
    public class Restaurant : IComparable
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RestaurantId { get; set; }
        [Required, DisplayName("Restaurant Name"), MaxLength(25, ErrorMessage = "Up to 25 characters allowed")]
        public string Name { get; set; }
        
        public RestaurantAddress RestaurantAddress { get; set; }
        public ICollection<RestaurantReview> Review { get; set; }
        
        [NotMapped]
        public string AvgRating {
            get
            {
                decimal result = 0;
                decimal numRatings = 0;

                foreach (var rev in Review)
                {
                    result += rev.Rating;
                    numRatings++;
                }
                return numRatings == 0 ? "N/A" : Convert.ToString(result / numRatings);
            }
        }
        

        public int CompareTo(object obj)
        {
            // Check if they are of the type, Restaurant.
            if (!(obj is Restaurant))
            {
                return -4;
            }

            Restaurant r1 = this;
            Restaurant r2 = (Restaurant) obj;
            // Next, check if null.
            if (r2 == null)
            {
                return -3;
            }

            // Lastly, check if either of them have a N/A rating.
            if (!decimal.TryParse(r1.AvgRating, out decimal rate1) ||
                !decimal.TryParse(r2.AvgRating, out decimal rate2))
            {
                return -2;
            }
            return decimal.Compare(rate1, rate2);
        }
    }
}