using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public AccountService(UserManager<ApplicationUser> userManager, IMapper mapper,
            SignInManager<ApplicationUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {request.Email}";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid credentials for {request.Email}";
                return response;
            }
            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Account no confirmed for {request.Email}";
                return response;
            }

            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;

            return response;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<RegisterResponse> RegisterClientUserAsync(RegisterRequest request, string origin)
        {
            RegisterResponse response = new()
            {
                HasError = false
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"username '{request.UserName}' is already taken.";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email '{request.Email}' is already registered.";
                return response;
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Url = request.Url,
                PhoneNumber = request.PhoneNumber,
                Cedula = request.Cedula
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Client.ToString());
                var verificationUri = await SendVerificationEmailUri(user, origin);
                await _emailService.SendAsync(new Core.Application.Dtos.Email.EmailRequest()
                {
                    To = user.Email,
                    Body = $"Please confirm your account visiting this URL {verificationUri}",
                    Subject = "Confirm registration"
                });

                var username = await GetByEmail(user.Email);

                response.Id = username.Id;
            }
            else
            {
                response.HasError = true;
                response.Error = $"An error occurred trying to register the user.";
                return response;
            }

            return response;
        }

        public async Task<RegisterResponse> RegisterAgentUserAsync(RegisterRequest request, string origin)
        {
            RegisterResponse response = new()
            {
                HasError = false
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"username '{request.UserName}' is already taken.";
                return response;
            }

            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email '{request.Email}' is already registered.";
                return response;
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Url = request.Url,
                PhoneNumber = request.PhoneNumber,
                Cedula = request.Cedula
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Agent.ToString());
                var verificationUri = await SendVerificationEmailUri(user, origin);
                await _emailService.SendAsync(new Core.Application.Dtos.Email.EmailRequest()
                {
                    To = user.Email,
                    Body = $"Please confirm your account visiting this URL {verificationUri}",
                    Subject = "Confirm registration"
                });

                var username = await GetByEmail(user.Email);

                response.Id = username.Id;
            }
            else
            {
                response.HasError = true;
                response.Error = $"An error occurred trying to register the user.";
                return response;
            }

            return response;
        }
               
        public async Task<RegisterResponse> RegisterAdminUserAsync(RegisterRequest request, string origin)
        {
            RegisterResponse response = new()
            {
                HasError = false,
            };

            //Validar que los usuarios no se repitan
            var userName = await _userManager.FindByNameAsync(request.UserName);
            if (userName != null)
            {
                response.HasError = true;
                response.Error = $"El Usuario: '{request.UserName}' no esta disponible.";
                return response;
            }

            //Verificar que los correos no se repitan 
            var userEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userEmail != null)
            {
                response.HasError = true;
                response.Error = $"El correo: '{request.Email}' no esta disponible.";
                return response;
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Cedula = request.Cedula,
                PhoneNumber = request.PhoneNumber,
                Url = "/Images/App/admin.png",
                EmailConfirmed = true

            };

            //Crear usuario
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());
                
            }
            else
            {
                response.HasError = true;
                response.Error = $"Upsss ha ocurrido un error al registrarse!";
                return response;
            }

            return response;
        }

        public async Task<RegisterResponse> RegisterDeveloperUserAsync(RegisterRequest request, string origin)
        {
            RegisterResponse response = new()
            {
                HasError = false,
            };

            //Validar que los usuarios no se repitan
            var userName = await _userManager.FindByNameAsync(request.UserName);
            if (userName != null)
            {
                response.HasError = true;
                response.Error = $"El Usuario: '{request.UserName}' no esta disponible.";
                return response;
            }

            //Verificar que los correos no se repitan 
            var userEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userEmail != null)
            {
                response.HasError = true;
                response.Error = $"El correo: '{request.Email}' no esta disponible.";
                return response;
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Cedula = request.Cedula,
                PhoneNumber = request.PhoneNumber,
                Url = "/Images/App/developer.png",
                EmailConfirmed = true

            };

            //Crear usuario
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Developer.ToString());

            }
            else
            {
                response.HasError = true;
                response.Error = $"Upsss ha ocurrido un error al registrarse!";
                return response;
            }

            return response;
        }
       
        public async Task<RegisterResponse> UpdateAgentAsync(RegisterRequest resquest)
        {
            RegisterResponse rps = new();

            ApplicationUser user = await _userManager.FindByEmailAsync(resquest.Email);

            user.Url = resquest.Url;

            await _userManager.UpdateAsync(user);

            await _userManager.RemovePasswordAsync(user);

            var result = await _userManager.AddPasswordAsync(user, resquest.Password);

            if (!result.Succeeded)
            {
                rps.HasError = true;
                rps.Error = result.Errors.First().Code;

                return rps;
            }

            rps.HasError = false;

            user = await _userManager.FindByEmailAsync(resquest.Email);

            return rps;
        }

        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return $"No accounts registered with this user";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Account confirmed for {user.Email}. You can now use the app";
            }
            else
            {
                return $"An error occurred while confirming {user.Email}.";
            }
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
        {
            ForgotPasswordResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {request.Email}";
                return response;
            }

            var verificationUri = await SendForgotPasswordUri(user, origin);

            await _emailService.SendAsync(new Core.Application.Dtos.Email.EmailRequest()
            {
                To = user.Email,
                Body = $"Please reset your account visiting this URL {verificationUri}",
                Subject = "reset password"
            });


            return response;
        }

        public async Task<ApplicationUser> GetByEmail(string email)
        {
            return await _userManager.Users.Where(p => p.Email == email).FirstOrDefaultAsync();
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {request.Email}";
                return response;
            }

            request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error occurred while reset password";
                return response;
            }

            return response;
        }

        #region Private method

        private async Task<string> SendVerificationEmailUri(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            var route = "User/ConfirmEmail";

            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", code);

            return verificationUri;
        }
        private async Task<string> SendForgotPasswordUri(ApplicationUser user, string origin)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ResetPassword";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "token", code);

            return verificationUri;
        }

        #endregion

        private string UploadFile(IFormFile file, int id, string imagePath = "")
        {
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

            return $"{basePath}/{fileName}";
        }


        #region Randy

        #region Update Agent Info

        public async Task<RegisterResponse> UpdateAgentInfoAsync(RegisterRequest resquest)
        {
            RegisterResponse rps = new();

            ApplicationUser user = await _userManager.FindByIdAsync(resquest.Id);

            user.Url = resquest.Url;
            user.FirstName = resquest.FirstName;
            user.LastName = resquest.LastName;
            user.PhoneNumber = resquest.PhoneNumber;

            await _userManager.UpdateAsync(user);

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                rps.HasError = true;
                rps.Error = "Ha ocurrido un error al Actualizar su informacion";
                return rps;
            }
            else
            {
                rps.HasError = false;
                rps.Error = "Operacion realizada con exito :v ";
                return rps;
            }

        }

        #endregion

        #region //Get All Active / Inactive User

        public async Task<int> GetAgenteActive()
        {
            var user = await _userManager.GetUsersInRoleAsync(Roles.Agent.ToString());

            int Active = 0;

            //Eliminar Roles que no sea Agentes.
            for (int i = 0; i < user.ToArray().Length; i++)
            {
                var list = await _userManager.GetRolesAsync(user[i]);

                if (list.Contains(Roles.Developer.ToString()) || list.Contains(Roles.Admin.ToString()))
                {
                    user.Remove(user[i]);
                }
            }

            foreach (ApplicationUser item in user)
            {
                if (await _userManager.IsEmailConfirmedAsync(item) == true)
                {
                    Active++;
                }
            }
            return Active;
        }

        public async Task<int> GetAgenteInactive()
        {
            var user = await _userManager.GetUsersInRoleAsync(Roles.Agent.ToString());

            int Inactive = 0;

            //Eliminar Roles que no sea Agentes.
            for (int i = 0; i < user.ToArray().Length; i++)
            {
                var list = await _userManager.GetRolesAsync(user[i]);

                if (list.Contains(Roles.Developer.ToString()) || list.Contains(Roles.Admin.ToString()))
                {
                    user.Remove(user[i]);
                }
            }

            foreach (ApplicationUser item in user)
            {
                if (await _userManager.IsEmailConfirmedAsync(item) == false)
                {
                    Inactive++;
                }
            }
            return Inactive;
        }

        public async Task<int> GetClienteActive()
        {
            var user = await _userManager.GetUsersInRoleAsync(Roles.Client.ToString());

            int Active = 0;

            //Eliminar Roles que no sea Clientes.
            for (int i = 0; i < user.ToArray().Length; i++)
            {
                var list = await _userManager.GetRolesAsync(user[i]);

                if (list.Contains(Roles.Developer.ToString()) || list.Contains(Roles.Admin.ToString()) || list.Contains(Roles.Agent.ToString()) )
                {
                    user.Remove(user[i]);
                }
            }

            foreach (ApplicationUser item in user)
            {
                if (await _userManager.IsEmailConfirmedAsync(item) == true)
                {
                    Active++;
                }
            }
            return Active;
        }

        public async Task<int> GetClienteInactive()
        {
            var user = await _userManager.GetUsersInRoleAsync(Roles.Client.ToString());

            int Inactive = 0;

            //Eliminar Roles que no sea Clientes.
            for (int i = 0; i < user.ToArray().Length; i++)
            {
                var list = await _userManager.GetRolesAsync(user[i]);

                if (list.Contains(Roles.Developer.ToString()) || list.Contains(Roles.Admin.ToString()) || list.Contains(Roles.Agent.ToString()))
                {
                    user.Remove(user[i]);
                }
            }

            foreach (ApplicationUser item in user)
            {
                if (await _userManager.IsEmailConfirmedAsync(item) == false)
                {
                    Inactive++;
                }
            }
            return Inactive;
        }

        public async Task<int> GetDeveloperActive()
        {
            var user = await _userManager.GetUsersInRoleAsync(Roles.Developer.ToString());

            int Active = 0;

            foreach (ApplicationUser item in user)
            {
                if (await _userManager.IsEmailConfirmedAsync(item) == true)
                {
                    Active++;
                }
            }
            return Active;
        }

        public async Task<int> GetDeveloperInactive()
        {
            var user = await _userManager.GetUsersInRoleAsync(Roles.Developer.ToString());

            int Inactive = 0;


            foreach (ApplicationUser item in user)
            {
                if (await _userManager.IsEmailConfirmedAsync(item) == false)
                {
                    Inactive++;
                }
            }
            return Inactive;
        }

        #endregion


        #region //Deshabilitar / Habilitar 
        public async Task DisableAccountAsync(string Id)
        {
            //Buscar usuario por Id
            var user = await _userManager.FindByIdAsync(Id);
            if (user != null)
            {
                user.EmailConfirmed = false;
                await _userManager.UpdateAsync(user);
            }

        }       

        
        public async Task ActiveAccountAsync(string Id)
        {
            //Buscar usuario por Id
            var user = await _userManager.FindByIdAsync(Id);
            if (user != null)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
            }

        }

        #endregion 

        #endregion
    }
}
