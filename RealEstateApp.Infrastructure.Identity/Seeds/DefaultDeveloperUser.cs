using Microsoft.AspNetCore.Identity;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Infrastructure.Identity.Seeds
{
    public static class DefaultDeveloperUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultDeveloperUser = new();
            defaultDeveloperUser.UserName = "Developer";
            defaultDeveloperUser.Email = "Developer@gmail.com";
            defaultDeveloperUser.FirstName = "Developer";
            defaultDeveloperUser.LastName = "User";
            defaultDeveloperUser.EmailConfirmed = true;
            defaultDeveloperUser.Cedula = "402-2424248-2";
            defaultDeveloperUser.PhoneNumber = "829-999-9999";
            defaultDeveloperUser.PhoneNumberConfirmed = true;
            defaultDeveloperUser.Url = "/Images/App/Developer.png";

            if (userManager.Users.All(u => u.Id != defaultDeveloperUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultDeveloperUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultDeveloperUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultDeveloperUser, Roles.Client.ToString());
                    await userManager.AddToRoleAsync(defaultDeveloperUser, Roles.Agent.ToString());
                    await userManager.AddToRoleAsync(defaultDeveloperUser, Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultDeveloperUser, Roles.Developer.ToString());
                }
            }

        }
    }
}
