using APP.GP.Web.Model;
using APP.GP.Web.Model.Filtros;
using APP.GP.Web.Services;
using Microsoft.AspNetCore.Mvc;

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
            string p1 = Helper.Encriptacion.Encriptar("1");
            string p2 = Helper.Encriptacion.Encriptar("41");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BuscarInvitados(string nombre, string apellidoPaterno, string apellidoMaterno, 
            string telefono, string correoElectronico, int estatus)
        {
            RegistroInvitacionRequest request = new RegistroInvitacionRequest()
            {
                Nombre = nombre,
                ApellidoPaterno = apellidoPaterno,
                ApellidoMaterno = apellidoMaterno,
                TelefonoPersonal = telefono,
                CorreoElectronico = correoElectronico,
                Vinculado = estatus
            };

            return Ok(await _registroInvitacionService.GetRegistroInvitacion(request));
        }

        [HttpPost]
        public async Task<IActionResult> RelacionarInvitados(bool todos, bool nombre, bool apellidos, bool correo, bool telefono)
        {
            RelacionActorAutomaticoRequest request = new RelacionActorAutomaticoRequest()
            {
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
            int idEscenario = Convert.ToInt32(Helper.Encriptacion.Desencriptar(p1));
            int idActor = Convert.ToInt32(Helper.Encriptacion.Desencriptar(p2.Replace(" ", "+")));
            var detalle = await _registroInvitacionService.GetDetalleInvitacionBy(idEscenario, idActor);

            return View(detalle.Objeto);
        }

    }
}
