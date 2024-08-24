using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace APP.GP.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost(Name = "Send")]
        public async Task Send()
        {
            var accountSid = "AC0d86845c167b5efa88866303028600d3";
            var authToken = "3759209f2bbbd92cb35caec8af9bcab0";
            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "Hola, este es un mensaje de prueba desde .NET Core usando Twilio! Visita nuestro sitio web: https://www.example.com",
                from: new PhoneNumber("whatsapp:+14155238886"), // Tu número de WhatsApp de Twilio
                to: new PhoneNumber("whatsapp:+5547836703") // El número de WhatsApp del destinatario
            );
         
        }
    }
}
