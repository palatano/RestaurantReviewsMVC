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
            RegularExpression(@"\d{5}", ErrorMessage = "5 digit zipcode only.")]
        public string ZipCode { get; set; }
        [Required, Column("Phone"), StringLength(25, ErrorMessage = "Up to 15 characters allowed.")]
        [DataType(DataType.PhoneNumber),
         RegularExpression(@"^([0-9]( |-)?)?(\(?[0-9]{3}\)?|[0-9]{3})( |-)?([0-9]{3}( |-)?[0-9]{4}|[a-zA-Z0-9]{7})$", ErrorMessage = "Invalid Phone Number.")]
        public string Phone { get; set; }
        [Required, DataType(DataType.EmailAddress), MaxLength(30, ErrorMessage = "Up to 15 characters allowed.")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "Invalid Email Format.")]
        public string Email { get; set; }

        [NotMapped]
        public string AddressWOStreet
        {
            get
            {
                return String.Format("{0}, {1} {2}, {3}",  City, State, ZipCode, Country);
            }
        }

        [NotMapped]
        public string AddressString
        {
            get
            {
                return String.Format("{0}, {1}, {2} {3}, {4}", Street, City, State, ZipCode, Country);
            }
        }
    }
}