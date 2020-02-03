using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CandyShop.Models
{
    public class Order
    {
        [BindingNever]
        public int OrderId { get; set; }
        [Required(ErrorMessage ="Please enter your first name.")]
        [Display(Name = "First Name")]
        [StringLength(25)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter your last name.")]
        [Display(Name = "Last Name")]
        [StringLength(35)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter your  street address.")]
        [Display(Name = "Address")]
        [StringLength(100)]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter your  city.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter your   state.")]
        [StringLength(2,MinimumLength =2)]
        public string State { get; set; }
        [Required(ErrorMessage = "Please enter your   zipcode.")]
        [StringLength(5, MinimumLength = 5)]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "Please enter your  phone number.")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
        [BindingNever]
        public decimal OrderTotal { get; set; }
        [BindingNever]
        public DateTime OrderPlaced { get; set; }
    }
}
