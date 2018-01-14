using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YummyTummy.Util
{
    public enum RestOrderEnum
    {
        [Display(Name = "Name Ascending")]
        NameAscending,
        [Display(Name = "Name Descending")]
        NameDescending,
        [Display(Name = "Average Rating Ascending")]
        AvgRatingAscending,
        [Display(Name = "Average Rating Descending")]
        AvgRatingDescending
    }
}