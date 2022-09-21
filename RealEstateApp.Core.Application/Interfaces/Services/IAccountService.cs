using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<RegisterResponse> RegisterClientUserAsync(RegisterRequest request, string origin);
        Task<RegisterResponse> RegisterAgentUserAsync(RegisterRequest request, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task<RegisterResponse> UpdateAgentAsync(RegisterRequest resquest);
        Task SignOutAsync();

        #region randy     

        Task<RegisterResponse> UpdateAgentInfoAsync(RegisterRequest resquest);
        Task<int> GetAgenteActive();
        Task<int> GetAgenteInactive();
        Task<int> GetClienteActive();
        Task<int> GetClienteInactive();
        Task<int> GetDeveloperActive();
        Task<int> GetDeveloperInactive();

        Task DisableAccountAsync(string Id);
        Task ActiveAccountAsync(string Id);
        Task<RegisterResponse> RegisterAdminUserAsync(RegisterRequest request, string origin);

        Task<RegisterResponse> RegisterDeveloperUserAsync(RegisterRequest request, string origin);




        #endregion
    }
}
