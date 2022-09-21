using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Filter;
using RealEstateApp.Core.Application.ViewModels.Propiedades;
using RealEstateApp.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApp.RealEstateApp.Models;

namespace WebApp.RealEstateApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPropiedadesService _propService;
        private readonly ITipoPropiedadService _tipoPropiedadService;
        private readonly ITipoVentaService _ventaService;
        private readonly ITipoMejoraService _mejoraService;

        public HomeController(UserManager<ApplicationUser> userManager, IPropiedadesService propService, ITipoPropiedadService tipoPropiedadService,
            ITipoVentaService ventaService, ITipoMejoraService mejoraService)
        {
            _userManager = userManager;
            _propService = propService;
            _tipoPropiedadService = tipoPropiedadService;
            _ventaService = ventaService;
            _mejoraService = mejoraService;
        }

        public async Task<IActionResult> Index(FilterVm vm)
        {

            ViewBag.TipoPropiedad = await _tipoPropiedadService.GetAllViewModel();
            ViewBag.TipoVenta = await _ventaService.GetAllViewModel();
            ViewBag.MejorasPropiedades = await _mejoraService.GetAllViewModel();

            return View(await _propService.GetAllWithFilter(vm) );
        }

        public async Task <IActionResult> Details(int codigo)
        {
            SavePropiedadesVm code = await _propService.GetByCode(codigo);

            var User = await _userManager.FindByNameAsync(code.LastModifiedBy);

            ViewBag.Propiedad = await _propService.GetPropiedadByCode(codigo);
            
            return View("Details", User);
        }


        public async Task<IActionResult> Agentes(FilterAgenteVm vm)
        {
            var user = await _userManager.GetUsersInRoleAsync(Roles.Agent.ToString());

            //Eliminar Roles que no sea Agentes.
            for (int i = 0; i < user.ToArray().Length; i++)
            {
                var list = await _userManager.GetRolesAsync(user[i]);

                if (list.Contains(Roles.Developer.ToString()) || list.Contains(Roles.Admin.ToString()))
                {
                    user.Remove(user[i]);
                }
            }

            //Filtro
            if (vm.FirstName != null)
            {
                var filter = user.Where(x => x.FirstName == vm.FirstName);

                if (filter != null)
                {
                    ViewBag.propiedadesVm = filter.OrderBy(x => x.FirstName);
                    return View();
                }

            }            

                ViewBag.propiedadesVm = user.OrderBy(x => x.FirstName);
                return View();         
        }

        public async Task<IActionResult> Propiedades(string UserName)
        {
            var propiedades = await _propService.GetAllViewModelWithInclude(UserName);

            return View("Propiedades", propiedades);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
