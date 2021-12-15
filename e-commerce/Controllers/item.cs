using e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace e_commerce.Controllers
{
    public class item
    {
        private Product pr = new Product();

        public Product Pr
        {
            get { return pr; }
            set { pr = value; }
        }

        private ApplicationUser user = new ApplicationUser();

        public ApplicationUser User
        {
            get { return user; }
            set { user = value; }
        }
        private int quentity;

        public int Quentity
        {
            get { return quentity; }
            set { quentity = value; }
        }
      
        public item()
        {

        }
        public item(Product product, int quentity)
        {
            this.pr = product;
            this.quentity = quentity;
          
        }


    }
}