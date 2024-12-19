using APP.GP.Web.Model;
using APP.GP.Web.Model.Eventos;
using APP.GP.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
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

    public async Task<IActionResult> GetDisposicionesForEscenario(int idEvento, int idEscenario, int marcar = 0)
    {
        var disposiciones = await _eventoService.GetDisposicionesForEscenario(idEvento, idEscenario, marcar);
        return Json(disposiciones);
    }

    [HttpPost]
    public async Task<IActionResult> CargarInvitados(IFormFile file)
    {
        List<ActorEscenarioDisposicion> actores = new();
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        //Extraer actores de archivo excel que se recibe
        using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream); // Copiamos el contenido del archivo a un stream
            stream.Position = 0; // Reiniciamos la posición del stream

            using (var package = new ExcelPackage(stream))
            {
                // Accedemos a la primera hoja del archivo Excel
                var worksheet = package.Workbook.Worksheets[0];

                // Asumimos que los datos comienzan en la fila 2 (después de los encabezados)
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    //if (!string.IsNullOrEmpty(worksheet.Cells[row, 7].Text))
                    actores.Add(new ActorEscenarioDisposicion
                    {
                        Nombre = string.IsNullOrEmpty(worksheet.Cells[row, 7].Text) ? string.Empty : worksheet.Cells[row, 7].Text,
                        Columna = Convert.ToInt32(worksheet.Cells[row, 4].Text),
                        Fila = worksheet.Cells[row, 3].Text,
                        Zona = worksheet.Cells[row, 2].Text,
                        CargoActual = worksheet.Cells[row, 9].Text,
                        Telefono = worksheet.Cells[row, 8].Text,
                    });
                }
            }
        }
        ActoresRequest request = new ActoresRequest { Actores = actores };
        var result = await _eventoService.AddDisposiciones(request);

        return Ok(result);
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
        //string idEscenario = Helper.Encriptacion.Encriptar(p1).Replace(" ", "+").Replace("/", ".");
        //string idActor = Helper.Encriptacion.Encriptar(p2).Replace(" ", "+").Replace("/", ".");

        string url = _configuration.GetValue<string>("UrlValidar");
        if (Convert.ToInt32(User.FindFirst(ClaimTypes.Sid)?.Value) == 2)
        {
            url = _configuration.GetValue<string>("UrlValidarSendela");
            return Json($"{url}/{p2}/{p1}");
        }
        return Json($"{url}/{p2}/{p1}");
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
