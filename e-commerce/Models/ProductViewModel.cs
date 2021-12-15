using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_commerce.Models
{
    public class ProductViewModel
    {
        [Key]
        public string title { get; set; }
        public IEnumerable<OrderDetail> Items { get; set; }
    }
}