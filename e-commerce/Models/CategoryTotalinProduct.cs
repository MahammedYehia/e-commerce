using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_commerce.Models
{
    public class CategoryTotalinProduct
    {
        [Key]
        public string categoryname { get; set; }
        public int Total { get; set; }
    }
}