using APP.GP.Web.Model;
using APP.GP.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace APP.GP.Web.Controllers
{
    public class SubGrupoController : Controller
    {
        private readonly GrupoService _grupoService;
        private readonly SubGrupoService _subGrupoService;

        public SubGrupoController(SubGrupoService subGrupoService, GrupoService grupoService)
        {
            _subGrupoService = subGrupoService;
            _grupoService = grupoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CatalogoSubGrupo> lista = await _subGrupoService.GetSubGruposAsync();
            return View(lista);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            List<CatalogoGrupo> lista = await _grupoService.GetCatalogoGruposAsync();
            this.ViewBag.ListaGrupos = lista;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CatalogoSubGrupo objeto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("error", "El nombre es obligatorio");
                    return View();
                }

                objeto.IdSubGrupo = 0;
                objeto.NombreGrupo = "N/A";

                await _subGrupoService.InsSubGrupoAsync(objeto);

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
                Resultado<CatalogoSubGrupo> resultado = await _subGrupoService.GetSubGrupoByIdAsync(id);

                if (resultado.ProcesoExitoso == 0)
                    throw new ArgumentException(resultado.Mensaje);

                List<CatalogoGrupo> lista = await _grupoService.GetCatalogoGruposAsync();
                this.ViewBag.ListaGrupos = lista;

                return View(resultado.Objeto);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CatalogoSubGrupo objeto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("error", "El nombre es obligatorio");
                    return View();
                }

                await _subGrupoService.UpdSubGrupoAsync(objeto);

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
            var categoria = await _subGrupoService.GetSubGrupoByIdAsync(id);
            return View(categoria.Objeto);
        }

        //Borrar subgrupo
        [HttpPost]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                Resultado resultado = await _subGrupoService.DelSubGrupoAsync(new CatalogoSubGrupo { IdSubGrupo = id });

                if (resultado.ProcesoExitoso == 0)
                    throw new ArgumentException(resultado.Mensaje);

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
