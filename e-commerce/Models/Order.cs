using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_commerce.Models
{
    [Bind(Exclude = "OrderId")]
    public partial class Order
    {
        [Key]
        public int OrderId { get; set; }

        public System.DateTime OrderDate { get; set; }
        public string Username { get; set; }
       

        [Required(ErrorMessage = "Address is required")]
        [StringLength(70)]
        public string Address { get; set; }


        [Required(ErrorMessage = "City is required")]
        [StringLength(40)]
        public string City { get; set; }


        [Required(ErrorMessage = "State is required")]
        [StringLength(40)]
        public string State { get; set; }

        [Required(ErrorMessage = "Postal Code is required")]
        [DisplayName("Postal Code")]
        [StringLength(10)]
        public string PostalCode { get; set; }


        [Required(ErrorMessage = "Country is required")]
        [StringLength(40)]
        public string Country { get; set; }



        [Required(ErrorMessage = "Phone is required")]
        [StringLength(24)]
        [Phone]
        public string Phone { get; set; }
       

        //[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
        //    ErrorMessage = "Email is is not valid.")]
        //[DataType(DataType.EmailAddress)]
        //public string Email { get; set; }
        
        [DisplayName("Total")]
        [Required(ErrorMessage = "Total is required")]
        public decimal Total { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

    }
}