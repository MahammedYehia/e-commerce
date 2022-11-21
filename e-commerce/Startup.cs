using e_commerce.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(e_commerce.Startup))]
namespace e_commerce
{

    public partial class Startup
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public void Configuration(IAppBuilder app)
        {
            //app.MapSignalR();
            ConfigureAuth(app);
           // CreateRoleAndUser();
        }
        public void CreateRoleAndUser()
        {
            var rolemanager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role = new IdentityRole();
            if (!rolemanager.RoleExists("admin"))
            {

                role.Name = "admin";
                rolemanager.Create(role);
                ApplicationUser user = new ApplicationUser();
                user.UserName = "admin";
                user.gender = "male";
                user.UserType = "admin";
                user.Email = "admin@gmail.com";
                var check = usermanager.Create(user, "123456789");
                if (check.Succeeded)
                {
                    usermanager.AddToRole(user.Id, "admin");
                }

            }

        }
    }
}
