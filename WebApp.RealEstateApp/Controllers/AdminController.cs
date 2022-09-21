using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Propiedades;
using RealEstateApp.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.RealEstateApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly IPropiedadesService _propService;
        

        private readonly AuthenticationResponse _userVm;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AdminController(UserManager<ApplicationUser> userManager, IUserService userService,
            IPropiedadesService propService, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _userService = userService;
            _propService = propService;
            _httpContextAccessor = httpContextAccessor;
            _userVm = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
           

        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Propiedades = await _propService.GetAllPropiedades();

            ViewBag.ActiveAgente = await _userService.ActiveAgente();
            ViewBag.InactiveAgente = await _userService.InactiveAgente();

            ViewBag.ActiveCliente = await _userService.ActiveCliente();
            ViewBag.InactiveCliente  = await _userService.InactiveCliente();

            ViewBag.ActiveDeveloper = await _userService.ActiveDeveloper();
            ViewBag.InactiveDeveloper = await _userService.InactiveDeveloper();

            return View();
        }

        public async Task<IActionResult> Agentes()
        {
            var agenteList = await _userManager.GetUsersInRoleAsync(Roles.Agent.ToString());

            //Eliminar Roles que no sea Agentes.
            for (int i = 0; i < agenteList.ToArray().Length; i++)
            {
                var list = await _userManager.GetRolesAsync(agenteList[i]);

                if (list.Contains(Roles.Developer.ToString()) || list.Contains(Roles.Admin.ToString()))
                {
                    agenteList.Remove(agenteList[i]);
                }
            }

            agenteList.OrderBy(a => a.FirstName).ToList();

            return View(agenteList);
        }

        public async Task<IActionResult> Administradores()
        {
            var adminList = await _userManager.GetUsersInRoleAsync(Roles.Admin.ToString());

            //Eliminar Roles que no sea Admin.
            for (int i = 0; i < adminList.ToArray().Length; i++)
            {
                var list = await _userManager.GetRolesAsync(adminList[i]);

                if (list.Contains(Roles.Developer.ToString()) )
                {
                    adminList.Remove(adminList[i]);
                }
            }
            adminList.OrderBy(a => a.FirstName).ToList();

            return View(adminList);
        }

        public async Task<IActionResult> Develop()
        {
            var developList = await _userManager.GetUsersInRoleAsync(Roles.Developer.ToString());

            developList.OrderBy(a => a.FirstName).ToList();

            return View(developList);
        }

        #region tipos propiedades
        public IActionResult TipoPropiedades()
        {
            return View();
        }


        #endregion


        #region tipos Ventas
        public IActionResult TipoVentas()
        {
            return View();
        }

        #endregion

        #region tipos Mejoras
        public IActionResult TipoMejoras()
        {
            return View();
        }

        #endregion


        #region Activate / Disable user Agente

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ActiveAdmin(string Id)
        {
            return View(await _userManager.FindByIdAsync(Id));
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ActiveUser(string Id)
        {
            return View(await _userManager.FindByIdAsync(Id));
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> ActiveUserPost(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);

            if (user.EmailConfirmed == true)
            {
                await _userService.DisableAccount(Id);
            }
            else
            {
                await _userService.ActiveAccount(Id);
            }

            if (_userVm.Roles.Contains(Roles.Admin.ToString()))
            {
                return RedirectToRoute(new { controller = "Admin", action = "Administradores" });
            }

            return RedirectToRoute(new { controller = "Admin", action = "Agentes" });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ActiveDevelop(string Id)
        {
            return View(await _userManager.FindByIdAsync(Id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> ActiveDevelopPost(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);

            if (user.EmailConfirmed == true)
            {
                await _userService.DisableAccount(Id);
            }
            else
            {
                await _userService.ActiveAccount(Id);
            }

            return RedirectToRoute(new { controller = "Admin", action = "Develop" });
        }
        #endregion

        #region Delete Agente

        public async Task<IActionResult> Delete(string Id)
        {
            return View(await _userManager.FindByIdAsync(Id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);

            var propiedades = await _propService.GetAllViewModelWithInclude(user.UserName);

            if (propiedades.Count() != 0)
            {
                for (int i = 0; i < propiedades.ToArray().Length; i++)
                {
                    var total = propiedades[i].Id;

                    await _propService.Delete(total);
                }

            }

            

            await _userManager.DeleteAsync(user);

            string basePath = $"/Images/User/{Id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new(path);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo folder in directory.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }

            return RedirectToRoute(new { controller = "Admin", action = "Agentes" });
        }


        #endregion




    }
}
