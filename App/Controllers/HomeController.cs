using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Diagnostics;
using App.Models;
using System.ComponentModel;

namespace App.Controllers;

public class HomeController : Controller
{
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly ILogger<HomeController> _logger;

    public HomeController(IWebHostEnvironment hostingEnvironment, ILogger<HomeController> logger)
    {
        _hostingEnvironment = hostingEnvironment;
        _logger = logger;
    }

    public IActionResult Arquivos(string location = "")
    {
        var webRootPath = _hostingEnvironment.WebRootPath;

        _logger.LogWarning("LOG WEBROOTPATH: " + webRootPath);
        _logger.LogWarning("LOG LOCATION: " + location);


        if (string.IsNullOrWhiteSpace(_hostingEnvironment.WebRootPath))
        {
            webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            _hostingEnvironment.WebRootPath = webRootPath;
            _logger.LogWarning("LOG WEBROOTPATH2: " + webRootPath);
        }

        location = Path.Combine(webRootPath, location);

        _logger.LogWarning("LOG LOCATION2: " + location);

        var nomesArquivos = new List<string>();

        if (Directory.Exists(location))
        {
            string[] arquivos = Directory.GetFiles(location);

            foreach (string arquivo in arquivos)
                nomesArquivos.Add(Path.GetFileName(arquivo));

            string[] pastas = Directory.GetDirectories(location);

            foreach (string pasta in pastas)
                nomesArquivos.Add("/" + Path.GetFileName(pasta));
        }

        return Ok(nomesArquivos);
    }

    public IActionResult Login() => View();


    [HttpPost]
    public async Task<IActionResult> Login(string senha)
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
