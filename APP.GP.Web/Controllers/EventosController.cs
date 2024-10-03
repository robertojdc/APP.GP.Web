using APP.GP.Web.Model.Eventos;
using APP.GP.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace APP.GP.Web.Controllers;

public class EventosController : Controller
{
    private readonly EventoService _eventoService;
    private readonly GrupoService _grupoService;

    public EventosController(EventoService eventoService, GrupoService grupoService)
    {
        _eventoService = eventoService;
        _grupoService = grupoService;
    }
    public IActionResult Index()
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
}
