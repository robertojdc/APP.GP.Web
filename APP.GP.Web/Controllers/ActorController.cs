using APP.GP.Web.Model;
using APP.GP.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APP.GP.Web.Controllers;

public class ActorController : Controller
{
    private readonly GrupoService _grupoService;

    // Inyección de dependencias del GrupoService
    public ActorController(GrupoService grupoService)
    {
        _grupoService = grupoService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Select()
    {
        return View();
    }

    public async Task<IActionResult> Consultar(string nombre, string apellidoPaterno, string apellidoMaterno, int idGrupo, int idSubGrupo, int idCategoria)
    {
        var grupos = await _grupoService.GetActoresAsync(new Model.Filtros.FiltroActor {
            Nombre = nombre,
            ApellidoPaterno = apellidoPaterno,
            ApellidoMaterno = apellidoMaterno,
            IdGrupo = idGrupo,
            IdSubGrupo = idSubGrupo,
            IdCategoria = idCategoria
        });
        return Ok(grupos);
    }

    public async Task<IActionResult> Create([FromForm] Actor actor, string categoriasJson)
    {
        var categorias = JsonConvert.DeserializeObject<List<Categoria>>(categoriasJson);
        actor.SubCategorias = categorias;
        var resultado = await _grupoService.AddActorAsync(actor);
        return Ok();
    }

    public async Task<IActionResult> ObtenerGrupos()
    {
        var grupos = await _grupoService.GetGrupos();
        return Ok(grupos);
    }

    public async Task<IActionResult> ObtenerSubGrupos(int idGrupo)
    {
        var subGrupos = await _grupoService.GetSubGrupos(idGrupo);
        return Ok(subGrupos);
    }

    public async Task<IActionResult> ObtenerCategorias(int idSubGrupo)
    {
        var categorias = await _grupoService.GetCategoriasAsync(idSubGrupo);
        return Ok(categorias);
    }

    public async Task<IActionResult> ObtenerSubCategorias(int idCategoria)
    {
        var subCategorias = await _grupoService.GetSubCategoriasAsync(idCategoria);
        return Ok(subCategorias);
    }
}
