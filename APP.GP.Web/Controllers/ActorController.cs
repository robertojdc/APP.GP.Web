using APP.GP.Web.Model;
using APP.GP.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        var grupos = await _grupoService.GetActoresAsync(new Model.Filtros.FiltroActor
        {
            Nombre = nombre,
            ApellidoPaterno = apellidoPaterno,
            ApellidoMaterno = apellidoMaterno,
            IdGrupo = idGrupo,
            IdSubGrupo = idSubGrupo,
            IdCategoria = idCategoria
        });
        return Ok(grupos);
    }

    public async Task<IActionResult> GetActores(int idSubCategoria, int tipo)
    {
        var grupos = await _grupoService.GetActoresAsync(new Model.Filtros.FiltroActor
        {
            IdCategoria = idSubCategoria,
            Tipo = tipo
        });
        return Ok(grupos);
    }

    public async Task<IActionResult> Create([FromForm] Actor actor, string categoriasJson)
    {
        // Deserializar las categorías
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

        // Manejar el archivo de foto si existe
        if (actor.Foto != null && actor.Foto.Length > 0)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Copia el contenido del archivo al MemoryStream
                await actor.Foto.CopyToAsync(memoryStream);

                // Convierte el contenido del archivo a un arreglo de bytes
                byte[] fileBytes = memoryStream.ToArray();

                // Convierte el arreglo de bytes a una cadena Base64
                actor.FotoBase64 = Convert.ToBase64String(fileBytes);
            }
        }

        // Llama al servicio para guardar el actor con la imagen en Base64
        var resultado = await _grupoService.AddActorAsync(actor);

        // Retorna la respuesta según el resultado
        return Ok(resultado);
    }

    [HttpGet]
    public async Task<IActionResult> Details([FromQuery] int idActor)
    {
        var actor = await _grupoService.GetActorByIdAsync(idActor);

        if (actor == null)
        {
            return NotFound(); // Manejo en caso de que no se encuentre el actor
        }

        // Devuelve la vista Details pasando el actor como modelo
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
    //GET Edit
    public async Task<IActionResult> Edit(int idActor)
    {
        var actor = await _grupoService.GetActorByIdAsync(idActor);
        ViewBag.Afinidades = (await _grupoService.GetAfinidades())
             .Select(a => new SelectListItem
             {
                 Value = a.Id.ToString(),
                 Text = a.Nombre
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
