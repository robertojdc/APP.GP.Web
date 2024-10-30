using APP.GP.Web.Model;
using APP.GP.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace APP.GP.Web.Controllers;
[Authorize]
public class ActorController : Controller
{
    private readonly GrupoService _grupoService;

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

    public async Task<IActionResult> Consultar(string nombre, string apellidoPaterno, string apellidoMaterno, int idGrupo, int idSubGrupo, int idCategoria, int afinidad, int compromiso)
    {
        var grupos = await _grupoService.GetActoresAsync(new Model.Filtros.FiltroActor
        {
            Nombre = nombre,
            ApellidoPaterno = apellidoPaterno,
            ApellidoMaterno = apellidoMaterno,
            IdGrupo = idGrupo,
            IdSubGrupo = idSubGrupo,
            IdCategoria = idCategoria,
            Afinidad = afinidad,
            Compromiso = compromiso
        });
        return Ok(grupos);
    }

    public async Task<IActionResult> GetActores(int idSubCategoria, int tipo, int idSubGrupo)
    {
        var grupos = await _grupoService.GetActoresAsync(new Model.Filtros.FiltroActor
        {
            IdCategoria = idSubCategoria,
            Tipo = tipo,
            IdSubGrupo = idSubGrupo
        });
        return Ok(grupos);
    }

    public async Task<IActionResult> Create([FromForm] Actor actor, string categoriasJson)
    {
        var categorias = JsonConvert.DeserializeObject<List<Categoria>>(categoriasJson);
        actor.SubCategorias = categorias;

        if (actor.RedesSociales == null)
        {
            if (!string.IsNullOrEmpty(actor.inputRedSocial))
            {
                actor.RedesSociales = new List<string>
                {
                    actor.inputRedSocial
                };
            }
        }

        if (actor.Foto != null && actor.Foto.Length > 0)
        {
            using (var memoryStream = new MemoryStream())
            {
                await actor.Foto.CopyToAsync(memoryStream);

                byte[] fileBytes = memoryStream.ToArray();

                actor.FotoBase64 = Convert.ToBase64String(fileBytes);
            }
        }

        var resultado = await _grupoService.AddActorAsync(actor);

        return Ok(resultado);
    }

    [HttpGet]
    public async Task<IActionResult> Details([FromQuery] int idActor)
    {
        var actor = await _grupoService.GetActorByIdAsync(idActor);
        if (actor == null)
            return NotFound();


        return View(actor);
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

    [HttpGet]
    public async Task<IActionResult> Delete(int idActor)
    {
        var resultado = await _grupoService.DelActorAsync(idActor);
        return Ok(resultado);
    }

    public async Task<IActionResult> Edit(int idActor)
    {
        var actor = await _grupoService.GetActorByIdAsync(idActor);
        ViewBag.Afinidades = (await _grupoService.GetAfinidades())
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Nombre,
                    Selected = a.Id == actor.AfinidadId
                }).ToList();

        ViewBag.Compromisos = (await _grupoService.GetCompromisos())
            .Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Nombre,
                Selected = a.Id == actor.CompromisoId
            }).ToList();
        return View(actor);
    }

    public async Task<IActionResult> GetActorDetalle(int id)
    {
        var actor = await _grupoService.GetActorByIdAsync(id);

        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            return PartialView("_ActorDetailPartial", actor);
        }

        return View(actor);
    }
    public async Task<IActionResult> GetAfinidades()
    {
        var afinidades = await _grupoService.GetAfinidades();
        return Json(afinidades);
    }

    public async Task<IActionResult> GetCompromisos()
    {
        var afinidades = await _grupoService.GetCompromisos();
        return Json(afinidades);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Actor actor, IFormFile Foto)
    {
        if (Foto != null && Foto.Length > 0)
        {
            using (var memoryStream = new MemoryStream())
            {
                await Foto.CopyToAsync(memoryStream);
                actor.FotoBase64 = Convert.ToBase64String(memoryStream.ToArray());
            }
        }

        var resultado = await _grupoService.EditActorAsync(actor);
        return Ok(resultado);
    }

}
