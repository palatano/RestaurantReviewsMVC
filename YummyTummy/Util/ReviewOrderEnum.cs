using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YummyTummy.Util
{
    public enum ReviewOrderEnum
    {
        [Display(Name = "Date Ascending")]
        NameAscending,
        [Display(Name = "Date Descending")]
        NameDescending,
        [Display(Name = "Rating Ascending")]
        RatingAscending,
        [Display(Name = "Rating Descending")]
        RatingDescending
    }
}