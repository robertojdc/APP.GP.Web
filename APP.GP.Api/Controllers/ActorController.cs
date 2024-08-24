using APP.GP.Business.Facade;
using APP.GP.Common.Utils;
using APP.GP.Entities;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML.Messaging;
using Twilio.Types;

namespace APP.GP.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ActorController : ControllerBase
{
    private readonly ILogger<ActorController> _logger;
    private readonly TableroFacade _tableroFacade;

    public ActorController(ILogger<ActorController> logger, TableroFacade _facade)
    {
        _logger = logger;
        _tableroFacade = _facade;
    }

    // POST: ActorController/AddActor
    [HttpPost]
    [Route("AddActor")]
    public async Task<IActionResult> AddActor(Actor _request)
    {
        try
        {
            Resultado result = new()
            {
                ProcesoExitoso = await Task.Run(() => _tableroFacade.AddTablero(new Actor
                {
                    Nombre = _request.Nombre,
                    Apellido = _request.Apellido,
                    Email = _request.Email,
                    Telefono = _request.Telefono,
                    Direccion = _request.Direccion,
                    Observaciones = _request.Observaciones,
                    FechaNacimiento = _request.FechaNacimiento
                }))
            };

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}