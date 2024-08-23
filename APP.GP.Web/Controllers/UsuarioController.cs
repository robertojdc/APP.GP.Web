using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APP.GP.Web.Controllers;

public class UsuarioController : Controller
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult Validate(string username, string password)
    {
        // Implementa aquí la lógica de autenticación.

        if (username == "admin" && password == "admin")
        {
            // Redirige al usuario a otra página si la autenticación es exitosa.
            return RedirectToAction("Default", "Home");
        }

        // Si la autenticación falla, muestra un mensaje de error o vuelve a la vista de login.
        ViewBag.Error = "Usuario o contraseña incorrectos.";
        return View("Login");
    }
}