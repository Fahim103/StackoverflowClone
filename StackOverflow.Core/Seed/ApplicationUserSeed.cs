using Microsoft.AspNet.Identity;
using StackOverflow.Core.Entities;

namespace StackOverflow.Core.Seed
{
    public class ApplicationUserSeed
    {
        private readonly UserManager<ApplicationUser> _manager;

        public ApplicationUserSeed(UserManager<ApplicationUser> manager)
        {
            _manager = manager;
        }

        public void SeedUser()
        {
            var admin = _manager.FindByEmail("admin@demo.com");
            if (admin == null)
            {
                var adminUser = new ApplicationUser()
                {
                    Email = "admin@demo.com",
                    UserName = "admin@demo.com"
                };

                var result = _manager.Create(adminUser, "Admin@1234");
                if (result.Succeeded)
                {
                    admin = _manager.FindByEmail("admin@demo.com");
                    _manager.AddToRole(admin.Id, "ADMIN");
                }
            }
        }
    }
}
