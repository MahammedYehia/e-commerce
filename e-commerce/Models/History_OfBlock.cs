using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace e_commerce.Models
{
    public class History_OfBlock
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("customer_id")]
        public virtual ApplicationUser customer { get; set; }
        public string customer_id { get; set; }

        
        [Column(TypeName = "date")]
        public DateTime end_block { get; set; }

        public int block_duration { get; set; }
    }
}
