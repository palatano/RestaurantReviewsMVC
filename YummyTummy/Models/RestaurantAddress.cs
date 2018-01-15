using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YummyTummy.Models
{

    public class RestaurantAddress
    {
        public int Id { get; set; }
        [Required, MaxLength(25, ErrorMessage ="Up to 25 characters allowed for street.")]
        public string Street { get; set; }
        [Required, MaxLength(25, ErrorMessage = "Up to 25 characters allowed for city.")]
        public string City { get; set; }
        [Required, MaxLength(10, ErrorMessage = "Up to 10 characters allowed for state.")]
        public string State { get; set; }
        [Required, MaxLength(5, ErrorMessage = "Up to 5 characters allowed for country.")]
        public string Country { get; set; }
        [Required, DataType(DataType.PostalCode),
            MaxLength(5, ErrorMessage = "Up to 5 characters allowed for zipcode."),
            DisplayFormat(DataFormatString = @"d{5}")]
        public string ZipCode { get; set; }
        [Required, Column("Phone"), StringLength(15, ErrorMessage = "Up to 15 characters allowed.")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required, DataType(DataType.EmailAddress), MaxLength(15, ErrorMessage = "Up to 15 characters allowed.")]
        public string Email { get; set; }

        [NotMapped]
        public string AddressWOStreet
        {
            get
            {
                return String.Format("{0}, {1} {2}, {3}",  City, State, ZipCode, Country);
            }
        }
    }
}