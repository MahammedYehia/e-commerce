using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_commerce.Models
{
    public class Category
    {
        public int id { get; set; }
        [Remote("TheNameIsExit", "Category",ErrorMessage ="categoryName is exited.")]
        public String CategoryName { get; set; }
        
        public virtual ICollection<Product> product { get; set; }
    }
}