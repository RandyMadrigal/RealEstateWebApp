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
    public static class DefaultAgentUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultAgenttUser = new();
            defaultAgenttUser.UserName = "AgentUser";
            defaultAgenttUser.Email = "Agent@email.com";
            defaultAgenttUser.FirstName = "John";
            defaultAgenttUser.LastName = "Doe";
            defaultAgenttUser.EmailConfirmed = true;
            defaultAgenttUser.Cedula = "402-2424248-2";
            defaultAgenttUser.PhoneNumber = "829-999-9999";
            defaultAgenttUser.PhoneNumberConfirmed = true;
            defaultAgenttUser.Url = "/Images/App/AgenteLogo.png";

            if (userManager.Users.All(u => u.Id != defaultAgenttUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultAgenttUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultAgenttUser, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultAgenttUser, Roles.Agent.ToString());
                }
            }
        }
    }
}
