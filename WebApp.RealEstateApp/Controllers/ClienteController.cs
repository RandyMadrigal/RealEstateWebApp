using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Favorite;
using RealEstateApp.Core.Application.ViewModels.Filter;
using RealEstateApp.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.RealEstateApp.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IFavoriteService _favoriteService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPropiedadesService _propService;
        private readonly ITipoPropiedadService _tipoPropiedadService;
        private readonly ITipoVentaService _ventaService;
        private readonly ITipoMejoraService _mejoraService;
        private readonly AuthenticationResponse _userVm;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClienteController(UserManager<ApplicationUser> userManager, IPropiedadesService propService, ITipoPropiedadService tipoPropiedadService,
            ITipoVentaService ventaService, ITipoMejoraService mejoraService, IFavoriteService favoriteService,
            IHttpContextAccessor httpContextAccessor)
        {
            _favoriteService = favoriteService;
            _userManager = userManager;
            _propService = propService;
            _tipoPropiedadService = tipoPropiedadService;
            _ventaService = ventaService;
            _mejoraService = mejoraService;
            _httpContextAccessor = httpContextAccessor;
            _userVm = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task<IActionResult> Index(FilterVm vm)
        {

            ViewBag.TipoPropiedad = await _tipoPropiedadService.GetAllViewModel();
            ViewBag.TipoVenta = await _ventaService.GetAllViewModel();
            ViewBag.MejorasPropiedades = await _mejoraService.GetAllViewModel();
            ViewBag.Favorite = await _favoriteService.GetAllViewModel();

            return View(await _propService.GetAllWithFilter(vm));
        }

        public async Task<IActionResult> AddFav(int Codigo)
        {
            var propiedad = await _propService.GetByCode(Codigo);

            SaveFavoriteVm favorite = new();
            favorite.Codigo = propiedad.Codigo;            
            
           var vm = await _favoriteService.Add(favorite);

            return View("Succeeded", vm);
        }

        public async Task<IActionResult> Propiedades(FilterVm vm)
        {
            ViewBag.TipoPropiedad = await _tipoPropiedadService.GetAllViewModel();

            ViewBag.Favorite = await _favoriteService.GetAllViewModel();

            return View(await _favoriteService.GetAllFavListFilter(vm));
        }



        public async Task<IActionResult> Delete(int Codigo)
        {
           
            return View(await _favoriteService.GetByCodeDelete(Codigo));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _favoriteService.Delete(id);
            return RedirectToRoute(new { controller = "Cliente", action = "Propiedades" });
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
      }
    }
