using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YummyTummy.Models
{
    public class RestaurantReview
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal Rating { get; set; }
        [DataType(DataType.MultilineText)]// text Area
        public string Comment { get; set; }
        [DataType(DataType.Date)]// calender
        public DateTime DateRated { get; set; } = DateTime.Now;


    }
}