using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using e_commerce.Models;
using System.ComponentModel.DataAnnotations;

namespace e_commerce.Models
{
    public class Cart
    {
     
        [Key]
        public int RecordId { get; set; }
        public string CartId { get; set; }
        public int ItemId { get; set; }
        public string userId { get; set; }
        public int Count { get; set; }
        public System.DateTime DateCreated { get; set; }
        public virtual Product Item { get; set; }
        public virtual ApplicationUser User { get; set; }




    }

}