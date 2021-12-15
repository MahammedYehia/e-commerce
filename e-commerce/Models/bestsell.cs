using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_commerce.Models
{
    public class bestsell
    {    [Key]
        public int ItemId { get; set; }

        public int TotalUnitsInStock { get; set; }
        public IEnumerable<Product> Items { get; set; }


    }
}