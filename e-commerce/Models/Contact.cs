using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_commerce.Models
{

    public class Contact
    {
        public int ContactID { get; set; }
        public string ContactName { get; set; }
        public string ContactNo { get; set; }
        public System.DateTime AddedOn { get; set; }
    }
}
