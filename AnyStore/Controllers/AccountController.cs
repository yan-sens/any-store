using AnyStore.Models;
using Common.Enums;
using DAL.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using Services.Models;
using Services.Services;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AnyStore.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(UserManager<ApplicationUser> userManager,
            IConfiguration configuration, IAccountService accountService) : base(userManager, configuration)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> SignIn(AccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            ApplicationUser user;

            try
            {
                user = await _accountService.SignIn(model.UserName, model.Password);

                //Create the identity for the user  
                var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.UserName)
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            }
            catch (Exception ex)
            {
                
            }

            return RedirectToAction("Privacy", "Home");
        }

        public async Task<UserResponseModel> SignUp()
        {
            var newUser = new ApplicationUser();
            return await _accountService.SignUp(newUser, "abc123", RolesEnum.Administrator);
        }
    }
}
