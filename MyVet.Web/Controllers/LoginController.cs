using Microsoft.AspNetCore.Mvc;
using MyVet.Web.Models;

namespace MyVet.Web.Controllers
{
    public class LogingController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loguinViewModel)
        {
            // return View();
            if (!ModelState.IsValid)
            {


            }

            return View(loguinViewModel);
        }
    }
}
