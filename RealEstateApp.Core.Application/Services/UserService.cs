using AutoMapper;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Filter;
using RealEstateApp.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public UserService(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel vm)
        {
            AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(vm);
            AuthenticationResponse userResponse = await _accountService.AuthenticateAsync(loginRequest);
            return userResponse;
        }
        public async Task SignOutAsync()
        {
            await _accountService.SignOutAsync();
        }

        public async Task<RegisterResponse> RegisterClientAsync(SaveUserViewModel vm, string origin)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            return await _accountService.RegisterClientUserAsync(registerRequest, origin);
        }

        public async Task<RegisterResponse> RegisterAgentAsync(SaveUserViewModel vm, string origin)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            return await _accountService.RegisterAgentUserAsync(registerRequest, origin);
        }

        public async Task<RegisterResponse> RegisterAdminAsync(SaveUserViewModel vm, string origin)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            return await _accountService.RegisterAdminUserAsync(registerRequest, origin);
        }

        public async Task<RegisterResponse> RegisterDeveloperAsync(SaveUserViewModel vm, string origin)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            return await _accountService.RegisterDeveloperUserAsync(registerRequest, origin);
        }

        public async Task<RegisterResponse> UpdateAgentAsync(SaveUserViewModel vm)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            return await _accountService.UpdateAgentAsync(registerRequest);
        }

        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {
            return await _accountService.ConfirmAccountAsync(userId, token);
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin)
        {
            ForgotPasswordRequest forgotRequest = _mapper.Map<ForgotPasswordRequest>(vm);
            return await _accountService.ForgotPasswordAsync(forgotRequest, origin);
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm)
        {
            ResetPasswordRequest resetRequest = _mapper.Map<ResetPasswordRequest>(vm);
            return await _accountService.ResetPasswordAsync(resetRequest);
        }

        #region Randy
       
        public async Task<RegisterResponse> UpdateAgentInfoAsync(EditUser vm)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            return await _accountService.UpdateAgentInfoAsync(registerRequest);
        }

        public async Task<int> ActiveAgente()
        {
            return await _accountService.GetAgenteActive();
        }

        public async Task<int> InactiveAgente()
        {
            return await _accountService.GetAgenteInactive();
        }

        public async Task<int> ActiveCliente()
        {
            return await _accountService.GetClienteActive();
        }

        public async Task<int> InactiveCliente()
        {
            return await _accountService.GetClienteInactive();
        }

        public async Task<int> ActiveDeveloper()
        {
            return await _accountService.GetDeveloperActive();
        }

        public async Task<int> InactiveDeveloper()
        {
            return await _accountService.GetDeveloperInactive();
        }

        //Disabled usuario
        public async Task DisableAccount(string Id)
        {
            await _accountService.DisableAccountAsync(Id);
        }

        //Active usuario
        public async Task ActiveAccount(string Id)
        {
            await _accountService.ActiveAccountAsync(Id);
        }

        #endregion

         

    }
}
