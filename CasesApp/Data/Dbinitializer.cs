using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CasesApp.Constants;

namespace CasesApp.Data
{
    public static class Dbinitializer
    {
        public static async Task SeedData(IServiceProvider serviceProvider)
        {
            ApplicationDbContext dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            UserManager<IdentityUser> userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await SeedRoles(roleManager);
            await SeedUsers(userManager);
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            IdentityResult roleResult;
            var roleExist = await roleManager.RoleExistsAsync(Roles.Worker);
            
            if (!roleExist)
            {
                //create the roles and seed them to the database: 
                roleResult = await roleManager.CreateAsync(new IdentityRole(Roles.Worker));
            }

            roleExist = await roleManager.RoleExistsAsync(Roles.Reviewer);

            if (!roleExist)
            {
                //create the roles and seed them to the database: 
                roleResult = await roleManager.CreateAsync(new IdentityRole(Roles.Reviewer));
            }

            roleExist = await roleManager.RoleExistsAsync(Roles.Approver);

            if (!roleExist)
            {
                //create the roles and seed them to the database: 
                roleResult = await roleManager.CreateAsync(new IdentityRole(Roles.Approver));
            }
        }

        private static async Task SeedUsers(UserManager<IdentityUser> userManager)
        {
            await SeedUser(userManager, "worker1", "worker1@hotmail.com", Roles.Worker);
            await SeedUser(userManager, "worker2", "worker2@hotmail.com", Roles.Worker);
            await SeedUser(userManager, "worker3", "worker3@hotmail.com", Roles.Worker);

            await SeedUser(userManager, "reviewer1", "reviewer1@hotmail.com", Roles.Reviewer);
            await SeedUser(userManager, "reviewer2", "reviewer2@hotmail.com", Roles.Reviewer);
            await SeedUser(userManager, "reviewer3", "reviewer3@hotmail.com", Roles.Reviewer);

            await SeedUser(userManager, "approver1", "approver1@hotmail.com", Roles.Approver);
            await SeedUser(userManager, "approver2", "approver2@hotmail.com", Roles.Approver);
            await SeedUser(userManager, "approver3", "approver3@hotmail.com", Roles.Approver);
        }

        private static async Task SeedUser(UserManager<IdentityUser> userManager, string userName, string email, string role)
        {
            if (userManager.FindByEmailAsync(email).Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = userName;
                user.Email = email;
                user.EmailConfirmed = true;

                IdentityResult result = userManager.CreateAsync(user, "P@ssw0rd").Result;

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }


            }
        }
    }
}
