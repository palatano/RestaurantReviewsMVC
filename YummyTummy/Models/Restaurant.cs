using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;// for 
using System.Linq;
using System.Web;
using YummyTummy.Util;

namespace YummyTummy.Models
{
    public class Restaurant
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RestaurantId { get; set; }
        [DisplayName("Restaurant Name")]
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

        [NotMapped]
        public static RestOrderEnum RestOrderEnum
        {
            get;
        }
    }
}