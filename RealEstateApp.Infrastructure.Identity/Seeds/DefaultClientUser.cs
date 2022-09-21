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
    public static class DefaultClientUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaulClienttUser = new();
            defaulClienttUser.UserName = "ClientUser";
            defaulClienttUser.Email = "Client@email.com";
            defaulClienttUser.FirstName = "John";
            defaulClienttUser.LastName = "Doe";
            defaulClienttUser.EmailConfirmed = true;
            defaulClienttUser.Cedula= "402-2424248-2";
            defaulClienttUser.PhoneNumber = "829-999-9999";
            defaulClienttUser.PhoneNumberConfirmed = true;
            defaulClienttUser.Url = "/Images/App/cliente.png";

            if (userManager.Users.All(u => u.Id != defaulClienttUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaulClienttUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaulClienttUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaulClienttUser, Roles.Client.ToString());
                }
            }

        }
    }
}
