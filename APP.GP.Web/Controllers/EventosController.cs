using APP.GP.Web.Model.Eventos;
using APP.GP.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

    public async Task<IActionResult> GetDetalleAsignacion(int idActor, int idEscenario)
    {
        var actor = await _eventoService.GetDetalleAsignacion(idActor, idEscenario);
        return Json(actor);
    }

    public async Task<IActionResult> GetDisposicionesForEscenario(int idEvento, int idEscenario)
    {
        var disposiciones = await _eventoService.GetDisposicionesForEscenario(idEvento, idEscenario);
        return Json(disposiciones);
    }

    public async Task<IActionResult> BuscarActores(string search)
    {
        var disposicion = await _eventoService.GetBusquedaActor(search, Convert.ToInt32(User.FindFirst(ClaimTypes.Sid)?.Value));
        return Json(disposicion);
    }
    public async Task<IActionResult> GetAllActores()
    {
        var disposicion = await _eventoService.GetBusquedaActor(string.Empty, Convert.ToInt32(User.FindFirst(ClaimTypes.Sid)?.Value));
        return Json(disposicion);
    }

    public async Task<IActionResult> GetActoresEscenariosById(int idEscenario)
    {
        var disposicion = await _eventoService.GetActoresEscenariosById(Convert.ToInt32(User.FindFirst(ClaimTypes.Sid)?.Value), idEscenario);
        return Json(disposicion.Lista.OrderBy(a => a.Nombre));
    }

    public async Task<IActionResult> GetActorById(int id)
    {
        var actor = await _grupoService.GetActorByIdAsync(id);

        return Json(actor);
    }

    [HttpPost]
    public async Task<IActionResult> AddDisposicion(int IdActor, int IdEscenario, string Fila, int Columna)
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

    public async Task<IActionResult> AddInvitacion(int idDisposicion, string codigoQr)
    {
        var result = await _eventoService.AddInvitacion(new Invitacion { IdDisposicion = idDisposicion, CodigoQR = codigoQr });

        return Ok(result);
    }

    public async Task<IActionResult> AddEnvioInvitacion(int idInvitacion, int tipoEnvio, string datosEnvio)
    {
        var result = await _eventoService.AddEnvioInvitacion(new EnvioInvitacion { IdInvitacion = idInvitacion, TipoEnvio = tipoEnvio, DatosEnvio = datosEnvio });

        return Ok(result);
    }

    public async Task<IActionResult> GetEventos()
    {
        var eventos = await _eventoService.GetEventos(Convert.ToInt32(User.FindFirst(ClaimTypes.Sid)?.Value));
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
        string idEscenario = Helper.Encriptacion.Encriptar(p1).Replace(" ", "+").Replace("/", ".");
        string idActor = Helper.Encriptacion.Encriptar(p2).Replace(" ", "+").Replace("/", ".");

        string url = _configuration.GetValue<string>("UrlValidar");
        if (Convert.ToInt32(User.FindFirst(ClaimTypes.Sid)?.Value) == 2)
        {
            url = _configuration.GetValue<string>("UrlValidarSendela");
            return Json($"{url}/{p2}/{p1}");
        }
        return Json($"{url}/{idActor}/{idEscenario}");
    }

    public async Task<IActionResult> EnviarInvitacionCorreo(int idInvitacion, string cuerpo, string imagen)
    {
        var result = await _eventoService.EnviarInvitacionCorreo(idInvitacion, cuerpo, imagen);

        return Ok(result);
    }

    //Descargar reporte excel
    [HttpGet]
    public async Task<IActionResult> DescargarReporteExcel(int idEscenario)
    {
        var result = await _eventoService.ObtenerReporteExcel(idEscenario);

        return File(Convert.FromBase64String(result.Cadena), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteEvento.xlsx");
    }
}
