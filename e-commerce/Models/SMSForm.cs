using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_commerce.Models
{
    public class SMSForm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Number { get; set; }
    }
}