using Microsoft.AspNetCore.Mvc;
using MyVet.Web.Helpers;
using MyVet.Web.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MyVet.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        public AccountController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
        }

        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)

        {
            if (ModelState.IsValid)
            {
                var resul = await _userHelper.LoginAsync(model);
                if (resul.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "User or password no valid.");

            }
            
            return View(model);
        }

    }
}

