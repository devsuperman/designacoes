
using App.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
#if DEBUG
            await Logar();
            return RedirectToAction("Index", "Designacoes");
#endif

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(string senha)
        {


            if (senha == "8318")
            {
                await Logar();
                return RedirectToAction("Index", "Designacoes");
            }

            return View();
        }

        private async Task Logar()
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Tiago"),
                    new Claim(ClaimTypes.NameIdentifier, "Tiago")
                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(claimsIdentity);
            var authProperties = new AuthenticationProperties { IsPersistent = true };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                claimPrincipal,
                authProperties);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
