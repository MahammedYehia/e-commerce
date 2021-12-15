using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_commerce.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public string userId { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public virtual Product Item { get; set; }
        public virtual Order Order { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}