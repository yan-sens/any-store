using Common.Enums;
using DAL.Models.Account;
using Microsoft.AspNetCore.Identity;
using Services.Interfaces;
using Services.Models;
using System;
using System.Threading.Tasks;

namespace Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Create new user account
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<UserResponseModel> SignUp(ApplicationUser user, string password, RolesEnum role)
        {
            var result = await _userManager.CreateAsync(user, password);


            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.ToString());
            }

            await _userManager.AddToRoleAsync(user, role.ToString());

            return new UserResponseModel { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, Role = role };
        }

        /// <summary>
        /// Authorize all users except patients
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<ApplicationUser> SignIn(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new Exception();
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);

            if (!isPasswordValid)
            {
                await _userManager.AccessFailedAsync(user);
                throw new Exception();
            }

            return user;
        }
    }
}
