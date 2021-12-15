using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_commerce.Models
{
    public class ContactUs
    {
        [Required]
        public String Name { get; set; }
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        [Required]
        public String Message { get; set; }
        [Required]
        public String Subject { get; set; }
    }
}