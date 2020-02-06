using Microsoft.AspNetCore.Identity;

namespace VideoGamesRepository.Data
{
    public static class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByEmailAsync("ash@test.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "admin123",
                    Email = "admin@vgr.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "123qwe").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (roleManager.FindByNameAsync("admin").Result == null)
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = "Admin",
                    NormalizedName = "Admin"
                };

                IdentityResult result = roleManager.CreateAsync(role).Result;
            }
        }
    }
}