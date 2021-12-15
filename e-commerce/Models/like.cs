using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_commerce.Models
{
    public class like
    {

        [Key]
        public int postId { get; set; }
        public string post_txt { get; set; }
        public DateTime data { get; set; }
        public string userId { get; set; }
        public int productId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Product Product { get; set; }
    }
}