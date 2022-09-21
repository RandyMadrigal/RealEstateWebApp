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
    public static class DefaultAdminUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultAdminUser = new();
            defaultAdminUser.UserName = "AdminUser";
            defaultAdminUser.Email = "Admin@email.com";
            defaultAdminUser.FirstName = "Admin";
            defaultAdminUser.LastName = "User";
            defaultAdminUser.Cedula = "402-2424248-2";
            defaultAdminUser.EmailConfirmed = true;
            defaultAdminUser.PhoneNumber = "829-999-9999";
            defaultAdminUser.PhoneNumberConfirmed = true;
            defaultAdminUser.Url = "/Images/App/admin.png";

            if (userManager.Users.All(u => u.Id != defaultAdminUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultAdminUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultAdminUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultAdminUser, Roles.Admin.ToString());
                }
            }
        }
    }
}
