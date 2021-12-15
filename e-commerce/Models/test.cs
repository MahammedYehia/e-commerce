using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_commerce.Models
{
    public class test
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide first name")]

        public string FristName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide last name")]
        public string LastName { get; set; }
        public string EmailID { get; set; }

        public string City { get; set; }
        public string Country { get; set; }



    }
}