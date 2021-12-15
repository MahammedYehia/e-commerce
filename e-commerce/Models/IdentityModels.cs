using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace e_commerce.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string UserType { get; set; }

        public virtual ICollection<Product> product { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<like> like { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual ICollection<History_OfBlock> History_OfBlock { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        public string Image { get; set; }

        public string gender { get; set; }
        public int Isblocked { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public System.Data.Entity.DbSet<e_commerce.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<e_commerce.Models.Product> Products { get; set; }

        public System.Data.Entity.DbSet<e_commerce.Models.Cart> Carts { get; set; }
        public System.Data.Entity.DbSet<e_commerce.Models.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<e_commerce.Models.OrderDetail> OrderDetails { get; set; }
        public System.Data.Entity.DbSet<e_commerce.Models.favourite> favourites { get; set; }
        public System.Data.Entity.DbSet<e_commerce.Models.like> Likes { get; set; }
        public System.Data.Entity.DbSet<e_commerce.Models.Employee> Employees { get; set; }
        public System.Data.Entity.DbSet<e_commerce.Models.Comment> Comments { get; set; }
        public System.Data.Entity.DbSet<e_commerce.Models.test> tests { get; set; }
        public System.Data.Entity.DbSet<e_commerce.Models.Contact> Contacts { get; set; }
        public System.Data.Entity.DbSet<e_commerce.Models.History_OfBlock> History_OfBlocks { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public System.Data.Entity.DbSet<e_commerce.Models.CategoryTotalinProduct> CategoryTotalinProducts { get; set; }

        public System.Data.Entity.DbSet<e_commerce.Models.bestsell> bestsells { get; set; }

        //public System.Data.Entity.DbSet<e_commerce.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}