using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace e_commerce.Models
{
    public class Comment
    {
            [Key]
            public int CommentID { get; set; }
            [Required]
            public string CommentName { get; set; }

        public DateTime date { get; set; }

    }
}