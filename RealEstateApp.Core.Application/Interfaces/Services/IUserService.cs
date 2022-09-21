using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin);
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<RegisterResponse> RegisterClientAsync(SaveUserViewModel vm, string origin);
        Task<RegisterResponse> RegisterAgentAsync(SaveUserViewModel vm, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm);
        Task<RegisterResponse> UpdateAgentAsync(SaveUserViewModel resquest);

        #region randy

        Task<RegisterResponse> UpdateAgentInfoAsync(EditUser vm);
        Task<int> ActiveAgente();
        Task<int> InactiveAgente();
        Task<int> ActiveCliente();
        Task<int> InactiveCliente();
        Task<int> ActiveDeveloper();
        Task<int> InactiveDeveloper();
        Task DisableAccount(string Id);
        Task ActiveAccount(string Id);
        Task<RegisterResponse> RegisterAdminAsync(SaveUserViewModel vm, string origin);
        Task<RegisterResponse> RegisterDeveloperAsync(SaveUserViewModel vm, string origin);

        #endregion
        Task SignOutAsync();
    }
}
