using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_commerce.Models
{
    public class favourite
    {
        [Key]
        public int favId { get; set; }
        public string userId { get; set; }
        public DateTime data { get; set; }
        public int productId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Product Product { get; set; }
    }
}