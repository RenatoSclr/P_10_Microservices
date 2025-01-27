using Microsoft.AspNetCore.Identity;

namespace Identity.Data
{
    public class SeedIdentityData
    {
        public static async Task Initialize(UserManager<IdentityUser> userManager)
        {
            var user = await userManager.FindByNameAsync("UserTest");

            if (user == null)
            {
                user = new IdentityUser()
                {
                    UserName = "UserTest",
                    Email = "admin@123.com",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, "UserTest123!");
            }
        }
    }
}
