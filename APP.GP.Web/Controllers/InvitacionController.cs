using APP.GP.Web.Model;
using APP.GP.Web.Model.Filtros;
using APP.GP.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace APP.GP.Web.Controllers
{
    public class InvitacionController : Controller
    {
        private readonly RegistroInvitacionService _registroInvitacionService;

        public InvitacionController(RegistroInvitacionService registroInvitacionService)
        {
            _registroInvitacionService = registroInvitacionService;
        }

        [HttpGet]
        public IActionResult Relation()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BuscarInvitados(string nombre, string apellidoPaterno, string apellidoMaterno,
            string telefono, string correoElectronico, int estatus, int descartado)
        {
            RegistroInvitacionRequest request = new RegistroInvitacionRequest()
            {
                Nombre = nombre,
                ApellidoPaterno = apellidoPaterno,
                ApellidoMaterno = apellidoMaterno,
                TelefonoPersonal = telefono,
                CorreoElectronico = correoElectronico,
                Vinculado = estatus,
                Descartado = descartado
            };

            return Ok(await _registroInvitacionService.GetRegistroInvitacion(request));
        }

        [HttpPost]
        public async Task<IActionResult> RelacionarInvitados(bool todos, bool nombre, bool apellidos, bool correo, bool telefono)
        {
            RelacionActorAutomaticoRequest request = new RelacionActorAutomaticoRequest()
            {
                IdUsuario = Convert.ToInt32(User.FindFirst(ClaimTypes.Sid)?.Value),
                Todos = todos,
                Nombre = nombre,
                Apellidos = apellidos,
                CorreoElectronico = correo,
                Telefono = telefono
            };

            return Ok(await _registroInvitacionService.AddRelacionarActorAutomatico(request));
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerOpcionesVinculacion(int idRegistroInvitacion)
        {
            return Ok(await _registroInvitacionService.GetOpcionesVinculacion(idRegistroInvitacion));
        }

        [HttpPost]
        public async Task<IActionResult> AddActorRegistroInvitacion(int idRegistroInvitacion, int idActor)
        {
            ActorInvitacionRequest request = new ActorInvitacionRequest()
            {
                IdRegistroInvitacion = idRegistroInvitacion,
                IdActor = idActor,
                Propuesta = false,
                Vinculado = true
            };

            return Ok(await _registroInvitacionService.AddActorRegistroInvitacion(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetInvitacionDetalle(int id)
        {
            var detalle = await _registroInvitacionService.GetActorRegistroInvitacionById(id);

            return PartialView("_InvitacionDetailPartial", detalle.Objeto);
        }

        [HttpGet("Invitacion/Validar/{p1}/{p2}")]
        public async Task<IActionResult> Validar(string p1, string p2)
        {
            int idEscenario = Convert.ToInt32(Helper.Encriptacion.Desencriptar(p1.Replace(" ", "+").Replace(".", "/")));
            int idActor = Convert.ToInt32(Helper.Encriptacion.Desencriptar(p2.Replace(" ", "+").Replace(".", "/")));
            var detalle = await _registroInvitacionService.GetDetalleInvitacionBy(idEscenario, idActor);

            return View(detalle.Objeto);
        }

        [HttpGet("Sendela/Validar/{p1}/{p2}")]
        public async Task<IActionResult> Sendela(int p1, int p2)
        {
            //int idEscenario = Convert.ToInt32(Helper.Encriptacion.Desencriptar(p1.Replace(" ", "+").Replace(".", "/")));
            //int idActor = Convert.ToInt32(Helper.Encriptacion.Desencriptar(p2.Replace(" ", "+").Replace(".", "/")));
            var detalle = await _registroInvitacionService.GetDetalleInvitacionBy(p1, p2);

            return View(detalle.Objeto);
        }


        [HttpPost]
        public async Task<IActionResult> DescartarInvitado(int idRegistroInvitacion)
        {
            RegistroInvitacionRequest request = new RegistroInvitacionRequest()
            {
                IdRegistroInvitacion = idRegistroInvitacion,
                Descartado = 1
            };

            return Ok(await _registroInvitacionService.UpdDescartarInvitado(request));
        }

        [HttpPost]
        public async Task<IActionResult> HabilitarInvitado(int idRegistroInvitacion)
        {
            RegistroInvitacionRequest request = new RegistroInvitacionRequest()
            {
                IdRegistroInvitacion = idRegistroInvitacion,
                Descartado = 0
            };

            return Ok(await _registroInvitacionService.UpdDescartarInvitado(request));
        }

        [HttpGet]
        public async Task<IActionResult> ExportarInivitados(string nombre, string apellidoPaterno, string apellidoMaterno,
            string telefono, string correoElectronico, int estatus, int descartado)
        {
            RegistroInvitacionRequest request = new RegistroInvitacionRequest()
            {
                Nombre = nombre,
                ApellidoPaterno = apellidoPaterno,
                ApellidoMaterno = apellidoMaterno,
                TelefonoPersonal = telefono,
                CorreoElectronico = correoElectronico,
                Vinculado = estatus,
                Descartado = descartado
            };

            ResultadoArchivo resultado = await _registroInvitacionService.GetExportarInvitados(request);

            return File(resultado.Archivo, "application/octet-stream", "Invitados.xlsx");
        }

    }
}
