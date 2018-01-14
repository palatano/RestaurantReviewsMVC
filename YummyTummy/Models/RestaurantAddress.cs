using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YummyTummy.Models
{
    /*
     * @model YummyTummy.Models.Address
@{
    ViewBag.Title = "Contact";
}
<h2>@ViewBag.Title.</h2>
<h3>@ViewBag.Message</h3>

<address>
    @Model.Street<br />
   @Model.City, @Model.State @Model.Country, @Model.ZipCode<br />
    <abbr title="Phone">P:</abbr>
    @Model.Phone
</address>

<address>
    <strong>Support:</strong>   <a href=@Model.Email>@Model.Email</a><br />   
</address>
     */

    public class RestaurantAddress
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        [StringLength(10)]
        public string State { get; set; }
        [StringLength(15)]
        public string Country { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Column("Phone"), StringLength(15)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [NotMapped]
        public string FullAddress
        {
            get
            {
                return String.Format("{0}, {1}, {2} {3}, {4}", Street, City, State, ZipCode, Country);
            }
        }
    }
}