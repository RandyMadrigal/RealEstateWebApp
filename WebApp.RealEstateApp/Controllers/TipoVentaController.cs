using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.VentaCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.RealEstateApp.Controllers
{
    public class TipoVentaController : Controller
    {
        private readonly ITipoVentaService _tipoVentaService;

        public TipoVentaController(ITipoVentaService tipoVentaService)
        {
            _tipoVentaService = tipoVentaService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _tipoVentaService.GetAllViewModelWithInclude());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View("SaveTipoVenta", new SaveVentaVm());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(SaveVentaVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveTipoVenta", vm);
            }

            await _tipoVentaService.Add(vm);
            return RedirectToRoute(new { controller = "TipoVenta", action = "Index" });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            return View("SaveTipoVenta", await _tipoVentaService.GetByIdSaveViewModel(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(SaveVentaVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveTipoVenta", vm);
            }

            await _tipoVentaService.Update(vm, vm.Id);
            return RedirectToRoute(new { controller = "TipoVenta", action = "Index" });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _tipoVentaService.GetByIdSaveViewModel(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _tipoVentaService.Delete(id);
            return RedirectToRoute(new { controller = "TipoVenta", action = "Index" });
        }
    }
}



