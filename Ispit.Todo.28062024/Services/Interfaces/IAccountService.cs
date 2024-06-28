using Ispit.Todo._28062024.Models.ViewModels;
using System.Security.Claims;

namespace Ispit.Todo._28062024.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ApplicationUserViewModel> GetUserProfile(ClaimsPrincipal user);
    }
}