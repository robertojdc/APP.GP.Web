using APP.GP.Web.Model;
using APP.GP.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace APP.GP.Web.Controllers
{
    public class GrupoController : Controller
    {
        private readonly GrupoService _grupoService;

        public GrupoController(GrupoService grupoService)
        {
            _grupoService = grupoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CatalogoGrupo> lista = await _grupoService.GetCatalogoGruposAsync();

            return View(lista);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CatalogoGrupo objeto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("error", "El nombre es obligatorio");
                    return View();
                }

                objeto.IdGrupo = 0;
                objeto.Estatus = true;
                objeto.EstatusDescripcion = "Activo";

                await _grupoService.InsCatalogoGrupoAsync(objeto);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                Resultado<CatalogoGrupo> resultado = await _grupoService.GetGrupoByIdAsync(id);

                if (resultado.ProcesoExitoso == 0)
                    throw new ArgumentException(resultado.Mensaje);

                return View(resultado.Objeto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CatalogoGrupo objeto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("error", "El nombre es obligatorio");
                    return View();
                }

                objeto.EstatusDescripcion = objeto.Estatus ? "Activo" : "Inactivo";

                await _grupoService.UpdCatalogoGrupoAsync(objeto);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Resultado<CatalogoGrupo> resultado = await _grupoService.GetGrupoByIdAsync(id);

                if (resultado.ProcesoExitoso == 0)
                    throw new ArgumentException(resultado.Mensaje);

                return View(resultado.Objeto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CatalogoGrupo objeto)
        {
            try
            {
                if (objeto.IdGrupo == 0)
                {
                    ModelState.AddModelError("error", "El nombre es obligatorio");
                    return View();
                }

                await _grupoService.DelCatalogoGrupoAsync(objeto.IdGrupo);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
                return View();
            }
        }
    }
}
