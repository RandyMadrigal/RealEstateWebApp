using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.User;
using RealEstateApp.Infrastructure.Identity.Entities;
using System;
using System.IO;
using System.Threading.Tasks;
using WebApp.RealEstateApp.Middlewares;

namespace WebApp.RealEstateApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, UserManager<ApplicationUser> userManager
            , IMapper mapper)
        {
            _userService = userService;
            _userManager = userManager;
            _mapper = mapper;

        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AuthenticationResponse userVm = await _userService.LoginAsync(vm);
            if (userVm != null && userVm.HasError != true)
            {
                HttpContext.Session.Set<AuthenticationResponse>("user", userVm);

                if (userVm.Roles.Contains(Roles.Agent.ToString()))
                {
                    return RedirectToRoute(new { controller = "Agente", action = "Index" });
                }

                if (userVm.Roles.Contains(Roles.Client.ToString()))
                {
                    return RedirectToRoute(new { controller = "Cliente", action = "Index" });
                }

                if (userVm.Roles.Contains(Roles.Admin.ToString()))
                {
                    return RedirectToRoute(new { controller = "Admin", action = "Index" });
                }

                return RedirectToRoute(new { controller = "Home", action = "Index" });

            }
            else
            {
                vm.HasError = userVm.HasError;
                vm.Error = userVm.Error;
                return View(vm);
            }
        }


        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
            
        public IActionResult Register()
        {
            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (vm.Role == "Agent")
            {
                var origin = Request.Headers["origin"];

                RegisterResponse response = await _userService.RegisterAgentAsync(vm, origin);

                if (response.HasError)
                {
                    vm.HasError = response.HasError;
                    vm.Error = response.Error;
                    return View(vm);
                }

                if (!string.IsNullOrWhiteSpace(response.Id) && response != null)
                {
                    vm.Url = UploadFile(vm.File, response.Id);

                    await _userService.UpdateAgentAsync(vm);

                    return RedirectToRoute(new { controller = "Agente", action = "Index" });
                }
            }
            if (vm.Role == "Client")
            {
                var origin = Request.Headers["origin"];

                RegisterResponse response = await _userService.RegisterClientAsync(vm, origin);

                if (response.HasError)
                {
                    vm.HasError = response.HasError;
                    vm.Error = response.Error;
                    return View(vm);
                }

                if (!string.IsNullOrWhiteSpace(response.Id) && response != null)
                {
                    vm.Url = UploadFile(vm.File, response.Id);

                    await _userService.UpdateAgentAsync(vm);

                    return RedirectToRoute(new { controller = "User", action = "Index" });
                }

            }
            if (vm.Role == "Admin")
            {
                var origin = Request.Headers["origin"];

                RegisterResponse response = await _userService.RegisterAdminAsync(vm, origin);

                if (response.HasError)
                {
                    vm.HasError = response.HasError;
                    vm.Error = response.Error;
                    return View(vm);
                }

                if (!string.IsNullOrWhiteSpace(response.Id) && response != null)
                {
                    vm.Url = UploadFile(vm.File, response.Id);

                    await _userService.UpdateAgentAsync(vm);

                    return RedirectToRoute(new { controller = "User", action = "Index" });
                }

            }

            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await _userService.ConfirmEmailAsync(userId, token);
            return View("ConfirmEmail", response);
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var origin = Request.Headers["origin"];
            ForgotPasswordResponse response = await _userService.ForgotPasswordAsync(vm, origin);
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult ResetPassword(string token)
        {
            return View(new ResetPasswordViewModel { Token = token });
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            ResetPasswordResponse response = await _userService.ResetPasswordAsync(vm);
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        #region Edit Admin

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);

            EditAdmin vm = new();
            vm.FirstName = user.FirstName;
            vm.LastName = user.LastName;
            vm.Email = user.Email;
            vm.Cedula = user.Cedula;
            vm.Username = user.UserName;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditAdmin vm)
        {
            if (!ModelState.IsValid)
            {   
                return View("Edit",vm);
            }
            EditAdmin admin = new();
           

            ApplicationUser user = await _userManager.FindByIdAsync(vm.Id);

            user.FirstName = vm.FirstName;
            user.LastName = vm.LastName;
            user.UserName = vm.Username;
            user.Email = vm.Email;
            user.Cedula = vm.Cedula;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, vm.Password);

           var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                try
                {
                    await _userManager.ChangePasswordAsync(user, user.PasswordHash, vm.Password);

                    admin.HasError = false;
                    admin.Error = "La edicion de su infomacion fue realizada con exito.";
                    return View("Succeeded", admin.Error);

                }
                catch (Exception)
                {

                    admin.HasError = false;
                    admin.Error = "Error al realizar la edicion";

                    return View("Succeeded", admin.Error);
                }  
            }

            admin.HasError = false;
            admin.Error = "Error al realizar la edicion";

            return View("Succeeded", admin.Error);
        }

        #endregion

        #region Developer
        public IActionResult RegisterDevelop()
        {
            return View(new SaveUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> RegisterDevelop(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (vm.Role == "Developer")
            {
                var origin = Request.Headers["origin"];

                RegisterResponse response = await _userService.RegisterDeveloperAsync(vm, origin);

                if (response.HasError)
                {
                    vm.HasError = response.HasError;
                    vm.Error = response.Error;
                    return View(vm);
                }

                if (!string.IsNullOrWhiteSpace(response.Id) && response != null)
                {
                    vm.Url = UploadFile(vm.File, response.Id);

                    await _userService.UpdateAgentAsync(vm);

                    return RedirectToRoute(new { controller = "User", action = "Index" });
                }

            }
            return RedirectToRoute(new { controller = "Admin", action = "Develop" });
        }
        #endregion

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

    }
}
