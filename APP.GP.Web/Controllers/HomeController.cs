using APP.GP.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APP.GP.Web.Controllers
{
    [Authorize] // Requiere autenticación para todas las acciones del controlador por defecto
    public class HomeController : Controller
    {
        private readonly GrupoService _grupoService;

        public HomeController(GrupoService grupoService)
        {
            _grupoService = grupoService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Tablero"); // Solo usuarios autenticados pueden acceder
        }

        [HttpGet]
        public IActionResult Place()
        {
            return View(); // Solo usuarios autenticados pueden acceder
        }

        [HttpGet]
        public IActionResult Default()
        {
            return View(); // Solo usuarios autenticados pueden acceder
        }

        [HttpGet]
        public IActionResult Tablero()
        {
            return View(); // Solo usuarios autenticados pueden acceder
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Invite()
        {
            return View(); // Solo usuarios autenticados pueden acceder
        }

        [HttpGet]
        public IActionResult Actor()
        {
            return View(); // Solo usuarios autenticados pueden acceder
        }

        // Los siguientes métodos están permitidos para usuarios no autenticados
        [HttpGet]

        public async Task<IActionResult> ObtenerGrupos()
        {
            var grupos = await _grupoService.GetAllTablero();
            return Json(grupos);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCategoriaGrupo(int idSubGrupo)
        {
            var grupos = await _grupoService.GetCategoriasAsync(idSubGrupo);
            return Json(grupos);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCategoriaGrupoTablero(int idSubGrupo)
        {
            var grupos = await _grupoService.GetCategoriasTablero(idSubGrupo);
            return Json(grupos);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerSubCategoria(int idCategoria)
        {
            var grupos = await _grupoService.GetSubCategoriasAsync(idCategoria);
            return Json(grupos);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerActoresCategoria()
        {
            var grupos = await _grupoService.GetCatalogoGruposAsync();
            return Json(grupos);
        }

        [HttpGet]
        public JsonResult GetContentData()
        {
            // Solo usuarios autenticados pueden acceder
            var data = new { /* estructura de datos */ };
            return Json(data);
        }

        [HttpGet]
        public JsonResult GetPopupContent(int popupId)
        {
            // Solo usuarios autenticados pueden acceder
            var data = new { /* estructura de datos */ };
            return Json(data);
        }

        [HttpGet]
        public JsonResult GetContactDetails(int productId)
        {
            // Solo usuarios autenticados pueden acceder
            var contact = new { /* detalle del contacto */ };
            return Json(contact);
        }
    }
}
