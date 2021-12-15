using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_commerce.Models
{
    public class Product
    {
        public int id { get; set; }
        [Required]
        public String title { get; set; }
        [Required]
        public String MyProperty { get; set; }
        [Required]
        public int price { get; set; }
        public DateTime data { get; set; }
        public String Image { get; set; }
        public int Quentity { get; set; }

        
        public string userId  { get; set; }
        public int Categoryid { get; set; }
        public virtual Category Category { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }




    }
}