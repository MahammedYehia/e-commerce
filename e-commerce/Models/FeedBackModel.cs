using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_commerce.Models
{
    public class FeedBackModel
    {
        [Key]
        public string title { get; set; }
        public IEnumerable<like> Items { get; set; }
    }
}