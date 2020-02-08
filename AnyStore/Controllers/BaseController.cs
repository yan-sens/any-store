using DAL.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AnyStore.Controllers
{
    [Authorize(AuthenticationSchemes = "Cookies")]
    public class BaseController : Controller
    {
        protected readonly UserManager<ApplicationUser> UserManager;
        protected ApplicationUser CurrentUser;
        protected IConfiguration Configuration;

        protected BaseController(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            UserManager = userManager;
            Configuration = configuration;
        }
    }
}