using Microsoft.AspNet.Identity;
using NHibernate.AspNet.Identity;

namespace StackOverflow.Core.Seed
{
    public class ApplicationUserRoleSeed : IdentityRole
    {
        public virtual void GenerateUserRoles()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(NHibernateDbContext.GetSession()));
            if (!roleManager.RoleExists(StringConstants.ADMIN_ROLE))
            {
                var role = new IdentityRole(StringConstants.ADMIN_ROLE);
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists(StringConstants.USER_ROLE))
            {
                var role = new IdentityRole(StringConstants.USER_ROLE);
                roleManager.Create(role);
            }
        }
    }
}
