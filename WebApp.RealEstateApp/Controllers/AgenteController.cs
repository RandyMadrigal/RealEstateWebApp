using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Filter;
using RealEstateApp.Core.Application.ViewModels.Propiedades;
using RealEstateApp.Core.Application.ViewModels.User;
using RealEstateApp.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.RealEstateApp.Controllers
{
    public class AgenteController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly IPropiedadesService _propService;
        private readonly ITipoPropiedadService _tipoPropiedadService;
        private readonly ITipoVentaService _ventaService;
        private readonly ITipoMejoraService _mejoraService;

        public AgenteController(UserManager<ApplicationUser> userManager, IUserService userService, 
            IPropiedadesService propService, ITipoPropiedadService tipoPropiedadService, ITipoVentaService ventaCategoryService,
            ITipoMejoraService mejoraCategoryService)
        {
            _userManager = userManager;
            _userService = userService;
            _propService = propService;
            _tipoPropiedadService = tipoPropiedadService;
            _ventaService = ventaCategoryService;
            _mejoraService = mejoraCategoryService;
        }

        public async Task<IActionResult> Index(FilterVm vm)
        {
            ViewBag.TipoPropiedad = await _tipoPropiedadService.GetAllViewModel();
            ViewBag.TipoVenta = await _ventaService.GetAllViewModel();
            ViewBag.MejorasPropiedades = await _mejoraService.GetAllViewModel();

            return View( await _propService.GetAllFilterAgente(vm));
        }

        public async Task<IActionResult> Error()
        {
            ErrorVm error = new();

            ViewBag.TipoPropiedad = await _tipoPropiedadService.GetAllViewModel();
            ViewBag.TipoVenta = await _ventaService.GetAllViewModel();
            ViewBag.MejorasPropiedades = await _mejoraService.GetAllViewModel();

            return View("Error", error );
        }

        #region Edit perfil

        public async Task<IActionResult> Perfil(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);

            EditUser edit = new();

            edit.FirstName = user.FirstName;
            edit.LastName = user.LastName;
            edit.PhoneNumber = user.PhoneNumber;
            edit.Url = user.Url;

            return View("Perfil", edit);
        }
        
        [HttpPost]
        public async Task<IActionResult> Perfil(EditUser vm)
        {
            var user = await _userManager.FindByIdAsync(vm.Id);

            vm.Url = UploadFile(vm.File, vm.Id, true, user.Url);

            var info = await _userService.UpdateAgentInfoAsync(vm);

            string message = info.Error;

            return View("Succeeded", message);

        }

        #endregion

        #region Create nueva propiedad

        public async Task<IActionResult> Create()
        {
            SavePropiedadesVm vm = new();

            vm.TiposPropiedades = await _tipoPropiedadService.GetAllViewModel();
            vm.TiposVentas = await _ventaService.GetAllViewModel();
            ViewBag.MejorasPropiedades = await _mejoraService.GetAllViewModel();
           
            return View("SavePropiedad", vm);

        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePropiedadesVm vm)
        {
            if (!ModelState.IsValid)
            {
                vm.TiposPropiedades = await _tipoPropiedadService.GetAllViewModel();
                vm.TiposVentas = await _ventaService.GetAllViewModel();
                ViewBag.MejorasPropiedades = await _mejoraService.GetAllViewModel();
                return View("SavePropiedad", vm);
            }

            SavePropiedadesVm PropVm = await _propService.Add(vm);

            if (PropVm.Id != 0 && PropVm != null)
            {
                PropVm.ImgMain = UploadFile(vm.FileMain, PropVm.Id.ToString());     
                PropVm.Img2 = UploadFile(vm.File2, PropVm.Id.ToString());             
                PropVm.Img3 = UploadFile(vm.File3, PropVm.Id.ToString());
                PropVm.Img4 = UploadFile(vm.File4, PropVm.Id.ToString());
                          
                
                await _propService.Update(PropVm, PropVm.Id);

            }


            return RedirectToRoute(new { controller = "Agente", action = "Index" });
        }

        #endregion

        #region Editar

        public async Task<IActionResult> Edit(int Codigo)
        {
            SavePropiedadesVm vm = await _propService.GetByCode(Codigo);
                      
            vm.TiposPropiedades = await _tipoPropiedadService.GetAllViewModel();

            vm.TiposVentas = await _ventaService.GetAllViewModel();

            ViewBag.MejorasPropiedades = await _mejoraService.GetAllViewModel();

            return View("SavePropiedad", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePropiedadesVm vm)
        {
            if (!ModelState.IsValid)
            {
                vm.TiposPropiedades = await _tipoPropiedadService.GetAllViewModel();
                vm.TiposVentas = await _ventaService.GetAllViewModel();
                ViewBag.MejorasPropiedades = await _mejoraService.GetAllViewModel();

                return View("SaveProduct", vm);
            }

            SavePropiedadesVm propiedadVm = await _propService.GetByIdSaveViewModel(vm.Id);

            vm.ImgMain = UploadFile(vm.FileMain, vm.Id.ToString(), true, propiedadVm.ImgMain);
            vm.Img2 = UploadFile(vm.File2, vm.Id.ToString(), true, propiedadVm.Img2);
            vm.Img3 = UploadFile(vm.File3, vm.Id.ToString(), true, propiedadVm.Img3);
            vm.Img4 = UploadFile(vm.File4, vm.Id.ToString(), true, propiedadVm.Img4);
                      
            await _propService.Update(vm, vm.Id);

            return RedirectToRoute(new { controller = "Agente", action = "Index" });
        }

        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int Codigo)
        {
            return View(await _propService.GetByCode(Codigo));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _propService.Delete(id);

            string basePath = $"/Images/Products/{id}";
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

            return RedirectToRoute(new { controller = "Agente", action = "Index" });
        }
        #endregion

        #region propiedades Categories
        public IActionResult Mantenimiento()
        {
            return View();
        }

        public async Task<IActionResult> TipoPropiedades()
        {
            return View(await _tipoPropiedadService.GetAllViewModel());
        }

        public async Task<IActionResult> VentasPropiedades()
        {
            return View(await _ventaService.GetAllViewModel());
        }

        public async Task<IActionResult> MejorasPropiedades()
        {
            return View(await _mejoraService.GetAllViewModel());
        }

        #endregion

        #region Manejo de imagen // PENDIENTE VOLVER ESTE METODO UN HELPER
        private string UploadFile(IFormFile file, string id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }
            string basePath = $"/Images/User/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //get file extension
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{fileName}";
        }
        #endregion



    }
}
