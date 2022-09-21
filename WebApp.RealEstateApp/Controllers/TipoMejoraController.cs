using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.MejorasCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.RealEstateApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TipoMejoraController : Controller
    {
        private readonly ITipoMejoraService _tipoMejoraService;


        public TipoMejoraController(ITipoMejoraService tipoMejoraService)
        {
            _tipoMejoraService = tipoMejoraService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _tipoMejoraService.GetAllViewModelWithInclude());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View("SaveTipoMejora", new SaveMejorasVm());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(SaveMejorasVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveTipoMejora", vm);
            }

            await _tipoMejoraService.Add(vm);
            return RedirectToRoute(new { controller = "TipoMejora", action = "Index" });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            return View("SaveTipoMejora", await _tipoMejoraService.GetByIdSaveViewModel(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(SaveMejorasVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveTipoMejora", vm);
            }

            await _tipoMejoraService.Update(vm, vm.Id);
            return RedirectToRoute(new { controller = "TipoMejora", action = "Index" });
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _tipoMejoraService.GetByIdSaveViewModel(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _tipoMejoraService.Delete(id);
            return RedirectToRoute(new { controller = "TipoMejora", action = "Index" });
        }
    }
}
