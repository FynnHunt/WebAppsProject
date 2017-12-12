using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAppsProject.Data;
using WebAppsProject.Models;

namespace WebAppsProject
{
    public class DbInitializer
    {
        public static async Task SeedUsers(ApplicationDbContext context, UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, ILogger<DbInitializer> logger)
        {

            string username1 = "Member1@gmail.com";
            string username2 = "Customer1@gmail.com";
            string username3 = "Customer2@gmail.com";
            string username4 = "Customer3@gmail.com";
            string username5 = "Customer4@gmai.com";
            string username6 = "Customer5@gmail.com";

            string adminRole = "Admin";

            await CreateRole(roleManager, adminRole);

            IdentityRole Admin = context.Roles.FirstOrDefault(x => x.Name == "Admin");
            await roleManager.AddClaimAsync(Admin, new Claim("CanEdit", "CanEdit"));

            ApplicationUser user1 = await userManager.FindByNameAsync(username1);
            var createdUser1 = await MakeUser(user1, username1, userManager);
            await userManager.AddToRoleAsync(createdUser1, adminRole);
            await SetPasswordForUser(userManager, logger, username1, createdUser1);

            ApplicationUser user2 = await userManager.FindByNameAsync(username2);
            var createdUser2 = await MakeUser(user2, username2, userManager);
            await SetPasswordForUser(userManager, logger, username2, createdUser2);

            ApplicationUser user3 = await userManager.FindByNameAsync(username3);
            var createdUser3 = await MakeUser(user3, username3, userManager);
            await SetPasswordForUser(userManager, logger, username3, createdUser3);

            ApplicationUser user4 = await userManager.FindByNameAsync(username4);
            var createdUser4 = await MakeUser(user4, username4, userManager);
            await SetPasswordForUser(userManager, logger, username4, createdUser4);

            ApplicationUser user5 = await userManager.FindByNameAsync(username5);
            var createdUser5 = await MakeUser(user5, username5, userManager);
            await SetPasswordForUser(userManager, logger, username5, createdUser5);

            ApplicationUser user6 = await userManager.FindByNameAsync(username6);
            var createdUser6 = await MakeUser(user6, username6, userManager);
            await SetPasswordForUser(userManager, logger, username6, createdUser6);

        }

        private static async Task<ApplicationUser> MakeUser(ApplicationUser user, string userName, UserManager<ApplicationUser> userManager)
        {
            if (user == null)
            {
                var newUser = new ApplicationUser() { UserName = userName, Email = userName };
                await userManager.CreateAsync(newUser);
                var createdUser = await userManager.FindByEmailAsync(userName);
                return createdUser;
            } else
            {
                return null;
            }
        }

        private static async Task CreateRole(RoleManager<IdentityRole> rm, string roleName)
        {
            IdentityRole role = new IdentityRole(roleName);
            if (!(await rm.RoleExistsAsync(roleName)))
            {
                await rm.CreateAsync(role);
            }
        }

        private static async Task SetPasswordForUser(UserManager<ApplicationUser> userManager, 
            ILogger<DbInitializer> logger, string userName, ApplicationUser user)
        {
            logger.LogInformation($"Set password for default user `{userName}`");
            const string password = "Password1!";
            var ir = await userManager.AddPasswordAsync(user, password);
            if (ir.Succeeded)
            {
                logger.LogTrace($"Set password `{password}` for default user `{userName}` successfully");
            }
            else
            {
                var exception = new ApplicationException($"Password for the user `{userName}` cannot be set");
                throw exception;
            }
        }
    }
}
