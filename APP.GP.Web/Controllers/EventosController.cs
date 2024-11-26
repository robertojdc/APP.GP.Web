using APP.GP.Web.Model.Eventos;
using APP.GP.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APP.GP.Web.Controllers;
[Authorize]
public class EventosController : Controller
{
    private readonly EventoService _eventoService;
    private readonly GrupoService _grupoService;
    private IConfiguration _configuration;
    public EventosController(EventoService eventoService, GrupoService grupoService, IConfiguration configuration)
    {
        _eventoService = eventoService;
        _grupoService = grupoService;
        _configuration = configuration;
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Asignacion()
    {
        return View();
    }

    public async Task<IActionResult> GetDisposicionesForEscenario(int idEvento, int idEscenario)
    {
        var disposiciones = await _eventoService.GetDisposicionesForEscenario(idEvento, idEscenario);
        return Json(disposiciones);
    }

    public async Task<IActionResult> BuscarActores(string search)
    {
        var disposicion = await _eventoService.GetBusquedaActor(search);
        return Json(disposicion);
    }
    public async Task<IActionResult> GetAllActores()
    {
        var disposicion = await _eventoService.GetBusquedaActor(string.Empty);
        return Json(disposicion);
    }

    public async Task<IActionResult> GetActorById(int id)
    {
        var actor = await _grupoService.GetActorByIdAsync(id);

        return Json(actor);
    }

    [HttpPost]
    public async Task<IActionResult> AddDisposicion(int IdActor, int IdEscenario, int Fila, int Columna)
    {
        var disposicion = new Disposicion
        {
            IdActor = IdActor,
            IdEscenario = IdEscenario,
            Fila = Fila,
            Columna = Columna
        };

        var result = await _eventoService.AddDisposicion(disposicion);

        return Ok(result);
    }

    public async Task<IActionResult> GetEventos()
    {
        var eventos = await _eventoService.GetEventos();
        return Json(eventos);
    }

    public async Task<IActionResult> GetEscenariosByEvento(int idEvento)
    {
        var escenarios = await _eventoService.GetEscenariosByEvento(idEvento);
        return Json(escenarios);
    }

    public async Task<IActionResult> GetEscenarioById(int idEscenario)
    {
        var actores = await _eventoService.GetEscenarioById(idEscenario);
        return Json(actores);
    }

    public async Task<IActionResult> GenerarUrl(string p1, string p2)
    {
        string idEscenario = Helper.Encriptacion.Encriptar(p1);
        string idActor = Helper.Encriptacion.Encriptar(p2.Replace(" ", "+"));

        string url = _configuration.GetValue<string>("UrlValidar");

        return Json($"{url}/{idActor}/{idEscenario}");
    }
}
