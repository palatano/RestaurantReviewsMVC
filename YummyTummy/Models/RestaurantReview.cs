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
        [Required, Range(0.0, 10.0, ErrorMessage ="Enter a rating between 0 and 10.")]
        public decimal Rating { get; set; }
        [Required, DataType(DataType.MultilineText), MaxLength(1024, ErrorMessage = "Up to 1024 characters allowed.")]// text Area
        public string Comment { get; set; }
        [Required, DataType(DataType.Date)]// calender
        public DateTime DateRated { get; set; } = DateTime.Now;

        [Required, MaxLength(25, ErrorMessage = "Up to a name of 25 characters allowed.")]
        public string ReviewerName { get; set; }

        public int Restaurant_RestaurantId { get; set; }
        [ForeignKey("Restaurant_RestaurantId")]
        public Restaurant Restaurant { get; set; }

        public string ReviewerId { get; set; }


    }
}