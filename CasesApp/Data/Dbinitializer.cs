using CasesApp.Models;
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
            await SeedCases(dbContext, userManager);
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            IdentityResult roleResult;
            var roleExist = await roleManager.RoleExistsAsync(Roles.Worker);
            
            if (!roleExist)
            {
                roleResult = await roleManager.CreateAsync(new IdentityRole(Roles.Worker));
            }

            roleExist = await roleManager.RoleExistsAsync(Roles.Reviewer);

            if (!roleExist)
            {
                roleResult = await roleManager.CreateAsync(new IdentityRole(Roles.Reviewer));
            }

            roleExist = await roleManager.RoleExistsAsync(Roles.Approver);

            if (!roleExist)
            {
                roleResult = await roleManager.CreateAsync(new IdentityRole(Roles.Approver));
            }
        }

        private static async Task SeedUsers(UserManager<IdentityUser> userManager)
        {
            await SeedUser(userManager, "worker1@hotmail.com", "worker1@hotmail.com", Roles.Worker);
            await SeedUser(userManager, "worker2@hotmail.com", "worker2@hotmail.com", Roles.Worker);
            await SeedUser(userManager, "worker3@hotmail.com", "worker3@hotmail.com", Roles.Worker);

            await SeedUser(userManager, "reviewer1@hotmail.com", "reviewer1@hotmail.com", Roles.Reviewer);
            await SeedUser(userManager, "reviewer2@hotmail.com", "reviewer2@hotmail.com", Roles.Reviewer);
            await SeedUser(userManager, "reviewer3@hotmail.com", "reviewer3@hotmail.com", Roles.Reviewer);

            await SeedUser(userManager, "approver1@hotmail.com", "approver1@hotmail.com", Roles.Approver);
            await SeedUser(userManager, "approver2@hotmail.com", "approver2@hotmail.com", Roles.Approver);
            await SeedUser(userManager, "approver3@hotmail.com", "approver3@hotmail.com", Roles.Approver);
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

        private static async Task SeedCases(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            if (!context.Case.Any())
            {
                IdentityUser worker1 = await userManager.FindByEmailAsync("worker1@hotmail.com");
                IdentityUser worker2 = await userManager.FindByEmailAsync("worker2@hotmail.com");
                IdentityUser worker3 = await userManager.FindByEmailAsync("worker3@hotmail.com");
                IdentityUser reviewer1 = await userManager.FindByEmailAsync("reviewer1@hotmail.com");
                IdentityUser reviewer2 = await userManager.FindByEmailAsync("reviewer2@hotmail.com");
                IdentityUser reviewer3 = await userManager.FindByEmailAsync("reviewer3@hotmail.com");
                IdentityUser approver1 = await userManager.FindByEmailAsync("approver1@hotmail.com");
                IdentityUser approver2 = await userManager.FindByEmailAsync("approver2@hotmail.com");
                IdentityUser approver3 = await userManager.FindByEmailAsync("approver3@hotmail.com");

               

                Case testCase1 = new Case { Title = "Test Case 1", Details = "Case Details", WorkerID = worker1.Id, ReviewerID = reviewer1.Id, ApproverID = approver1.Id, DateCreated = new DateTime(2018, 08, 20), DateReviewed = null, DateApproved = null, Status = CaseStatus.Pending };
                Case testCase2 = new Case { Title = "Test Case 2", Details = "Case Details", WorkerID = worker3.Id, ReviewerID = reviewer1.Id, ApproverID = approver3.Id, DateCreated = new DateTime(2018, 08, 22), DateReviewed = null, DateApproved = null, Status = CaseStatus.PendingReview };
                Case testCase3 = new Case { Title = "Test Case 3", Details = "Case Details", WorkerID = worker2.Id, ReviewerID = reviewer2.Id, ApproverID = approver2.Id, DateCreated = new DateTime(2018, 08, 18), DateReviewed = new DateTime(2018, 08, 22), DateApproved = null, Status = CaseStatus.PendingApproval };
                Case testCase4 = new Case { Title = "Test Case 4", Details = "Case Details", WorkerID = worker2.Id, ReviewerID = reviewer3.Id, ApproverID = approver3.Id, DateCreated = new DateTime(2018, 08, 20), DateReviewed = new DateTime(2018, 08, 27), DateApproved = null, Status = CaseStatus.PendingApproval };
                Case testCase5 = new Case { Title = "Test Case 5", Details = "Case Details", WorkerID = worker1.Id, ReviewerID = reviewer3.Id, ApproverID = approver2.Id, DateCreated = new DateTime(2018, 08, 20), DateReviewed = new DateTime(2018, 08, 24), DateApproved = new DateTime(2018, 08, 25), Status = CaseStatus.Approved };
                Case testCase6 = new Case { Title = "Test Case 6", Details = "Case Details", WorkerID = worker3.Id, ReviewerID = reviewer1.Id, ApproverID = approver1.Id, DateCreated = new DateTime(2018, 08, 21), DateReviewed = new DateTime(2018, 08, 22), DateApproved = new DateTime(2018, 08, 22), Status = CaseStatus.Approved };

                context.Case.Add(testCase1);
                context.Case.Add(testCase2);
                context.Case.Add(testCase3);
                context.Case.Add(testCase4);
                context.Case.Add(testCase5);
                context.Case.Add(testCase6);

                context.SaveChanges();
            }

        }
    }
}
