using APP.GP.Web.Model;
using APP.GP.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APP.GP.Web.Controllers;

public class CategoriaController : Controller
{
    private readonly GrupoService _grupoService;

    // Inyección de dependencias del GrupoService
    public CategoriaController(GrupoService grupoService)
    {
        _grupoService = grupoService;
    }
    // GET: CategoriaController
    public async Task<IActionResult> Index()
    {
        //Obtener los grupos del grupoService
        var grupos = await _grupoService.GetGrupos();

        ViewBag.Grupos = grupos;

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetSubGrupos(int grupoId)
    {
        // Obtener los subgrupos según el Id del grupo seleccionado
        var subGrupos = await _grupoService.GetSubGrupos(grupoId);

        // Retornar los subgrupos como JSON
        return Json(subGrupos);
    }

    [HttpGet]
    public async Task<IActionResult> GetCategorias(int subGrupoId)
    {
        // Obtener las categorías según el Id del subgrupo seleccionado
        var categorias = await _grupoService.GetCategoriasAsync(subGrupoId);

        // Retornar las categorías como JSON
        return Json(categorias);
    }

    [HttpGet]
    // GET: CategoriaController/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: CategoriaController/Create
    public async Task<IActionResult> Create()
    {
        var grupos = await _grupoService.GetGrupos();

        ViewBag.Grupos = grupos;
        return View();
    }

    // POST: CategoriaController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(IFormCollection collection)
    {
        try
        {
            var cat = new Categoria
            {
                IdSubGrupo = int.Parse(collection["IdSubGrupo"]),
                Descripcion = collection["Descripcion"],
                IdSubcategoria = 0
            };
            await _grupoService.InsCatalogoCategoria(cat);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: CategoriaController/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var categoria = await _grupoService.GetCategoriaByIdAsync(id);
        return View(categoria);
    }

    // POST: CategoriaController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, IFormCollection collection)
    {
        try
        {
            var cat = new Categoria
            {
                IdCategoria = int.Parse(collection["IdCategoria"]),
                Descripcion = collection["Descripcion"],
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

    // GET: CategoriaController/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var categoria = await _grupoService.GetCategoriaByIdAsync(id);
        return View(categoria);
    }

    // POST: CategoriaController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, IFormCollection collection)
    {
        try
        {
            var categoria = await _grupoService.DelCatalogoCategoriaAsync(new Categoria { IdCategoria = id });
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
