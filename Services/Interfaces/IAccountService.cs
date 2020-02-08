using Common.Enums;
using DAL.Models.Account;
using Services.Models;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAccountService
    {
        Task<UserResponseModel> SignUp(ApplicationUser user, string password, RolesEnum role);
        Task<ApplicationUser> SignIn(string userName, string password);
    }
}
