using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedadCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.RealEstateApp.Controllers
{
    public class TipoPropiedadController : Controller
    {

        private readonly ITipoPropiedadService _tipoPropiedadService;

        public TipoPropiedadController(ITipoPropiedadService tipoPropiedadService)
        {
            _tipoPropiedadService = tipoPropiedadService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _tipoPropiedadService.GetAllViewModelWithInclude());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View("SaveTipoPropiedad", new SaveTipoPropiedadVm());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(SaveTipoPropiedadVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveTipoPropiedad", vm);
            }

            await _tipoPropiedadService.Add(vm);
            return RedirectToRoute(new { controller = "TipoPropiedad", action = "Index" });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            return View("SaveTipoPropiedad", await _tipoPropiedadService.GetByIdSaveViewModel(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(SaveTipoPropiedadVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveTipoPropiedad", vm);
            }

            await _tipoPropiedadService.Update(vm, vm.Id);
            return RedirectToRoute(new { controller = "TipoPropiedad", action = "Index" });
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _tipoPropiedadService.GetByIdSaveViewModel(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _tipoPropiedadService.Delete(id);
            return RedirectToRoute(new { controller = "TipoPropiedad", action = "Index" });
        }
    }
}
