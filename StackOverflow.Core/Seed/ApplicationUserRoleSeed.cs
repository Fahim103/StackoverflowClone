using Microsoft.AspNet.Identity;
using NHibernate.AspNet.Identity;

namespace StackOverflow.Core.Seed
{
    public class ApplicationUserRoleSeed : IdentityRole
    {
        public virtual void GenerateUserRoles()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(NHibernateDbContext.GetSession()));
            if (!roleManager.RoleExists("ADMIN"))
            {
                var role = new IdentityRole("ADMIN");
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("USER"))
            {
                var role = new IdentityRole("USER");
                roleManager.Create(role);
            }
        }
    }
}
