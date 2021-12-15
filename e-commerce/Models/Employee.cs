using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace e_commerce.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Name { get; set; }       
        public string Position { get; set; }
        public string Office { get; set; }
        public Nullable<int> Salary { get; set; }
        [DisplayName("Image")]
        public string ImagePath { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }
        public Employee()
        {
            ImagePath = "~/AppFiles/Images/default.png";
        }


    }
}