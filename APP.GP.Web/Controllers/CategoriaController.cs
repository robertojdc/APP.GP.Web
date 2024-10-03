using APP.GP.Web.Model;
using APP.GP.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace APP.GP.Web.Controllers;

public class CategoriaController : Controller
{
    private readonly GrupoService _grupoService;

    public CategoriaController(GrupoService grupoService)
    {
        _grupoService = grupoService;
    }

    public async Task<IActionResult> Index()
    {
        var grupos = await _grupoService.GetGrupos();
        ViewBag.Grupos = grupos;
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetSubGrupos(int grupoId) => Json(await _grupoService.GetSubGrupos(grupoId));

    [HttpGet]
    public async Task<IActionResult> GetCategorias(int subGrupoId) => Json(await _grupoService.GetCategoriasAsync(subGrupoId));

    [HttpGet]
    public async Task<IActionResult> GetCategoriasGerarquico(int subGrupoId) => Json(await _grupoService.GetCategoriasGerarquicoAsync(subGrupoId));

    public async Task<IActionResult> ObtenerSubCategorias(int idCategoria)
    {
        var subCategorias = await _grupoService.GetSubCategoriasAsync(idCategoria);
        return Ok(subCategorias);
    }

    [HttpGet]
    public ActionResult Details(int id)
    {
        return View();
    }

    public async Task<IActionResult> Create()
    {
        var grupos = await _grupoService.GetGrupos();

        ViewBag.Grupos = grupos;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateCat(int idCategoria, int idSubGrupo, string descripcion)
    {
        try
        {
            var result = await _grupoService.InsCatalogoCategoria(new Categoria { IdSubGrupo = idSubGrupo, Descripcion = descripcion, IdSubcategoria = idCategoria });

            return Json(result);
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateSub(int idCategoria, int idSubGrupo, string descripcion)
    {
        try
        {
            var result = await _grupoService.InsCatalogoCategoria(new Categoria { IdSubGrupo = 0, Descripcion = descripcion, IdSubcategoria = idCategoria });

            return Json(result);
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var categoria = await _grupoService.GetCategoriaByIdAsync(id);
        return View(categoria);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int idCategoria, string descripcion)
    {
        try
        {
            var cat = new Categoria
            {
                IdCategoria = idCategoria,
                Descripcion = descripcion,
                IdSubcategoria = 0
            };
            var categoria = await _grupoService.UpdCatalogoCategoriaAsync(cat);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var categoria = await _grupoService.GetCategoriaByIdAsync(id);
        await _grupoService.DelCatalogoCategoriaAsync(categoria);
        return View(categoria);
    }
}