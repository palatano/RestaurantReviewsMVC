using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YummyTummy.Models
{
    public class TestHelpers
    {
        public int Id { get; set; }
        [StringLength(25, ErrorMessage = "Name should be between 10 to 25 characters.")]
        [Required]
        public string Name { get; set; }
        public bool Confirm { get; set; }
        [Range(18, 60, ErrorMessage ="Age should be between 18 to 60 for drivery's license")]
        public int Age { get; set; }
        public decimal Rate { get; set; }
        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }
        public DateTime DOB { get; set; }
        public byte[] Photo { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Please use valid email format")]
        public string Email { get; set; }
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }
        [DataType(DataType.PhoneNumber, ErrorMessage = "ENTER A PHONE NUMBER MAN")]
        public string Phone { get; set; }
        [RegularExpression("[0-9]{3}-[0-9]{2}-[0-9]{4}", ErrorMessage = "Invalid SSN")]
        public string ssn { get; set; }
    }
}