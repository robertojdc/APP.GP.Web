using APP.GP.Web.Model;
using APP.GP.Web.Model.DTO;
using APP.GP.Web.Model.Filtros;

namespace APP.GP.Web.Services
{
    public class RegistroInvitacionService
    {
        private readonly HttpClient _httpClient;

        public RegistroInvitacionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RegistroInvitacion>> GetRegistroInvitacion(RegistroInvitacionRequest request)
        {
            var response = 
                await _httpClient.GetFromJsonAsync<List<RegistroInvitacion>>(
                    $"/RegistroInvitacion/GetRegistroInvitacion?Nombre={request.Nombre}&ApellidoPaterno={request.ApellidoPaterno}&ApellidoMaterno={request.ApellidoMaterno}&TelefonoPersonal={request.TelefonoPersonal}&CorreoElectronico={request.CorreoElectronico}&Vinculado={request.Vinculado}");
            return response;
        }

        public async Task<Resultado<RegistroInvitacion>> GetRegistroInvitacionById(int idRegistroInvitacion)
        {
            var response = await _httpClient.GetFromJsonAsync<Resultado<RegistroInvitacion>>("/RegistroInvitacion/GetRegistroInvitacionById?idRegistroInvitacion=" + idRegistroInvitacion);
            return response;
        }

        public async Task<Resultado> AddRegistroInvitacion(RegistroInvitacion objeto)
        {
            var response = await _httpClient.PostAsJsonAsync("/RegistroInvitacion/AddRegistroInvitacion", objeto);

            var result = await response.Content.ReadFromJsonAsync<Resultado>();
            return result;
        }

        public async Task<Resultado<RegistroInvitacion>> AddRelacionarActorAutomatico(RelacionActorAutomaticoRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("/RegistroInvitacion/AddRelacionarActorAutomatico", request);

            var result = await response.Content.ReadFromJsonAsync<Resultado<RegistroInvitacion>>();
            return result;
        }

        public async Task<Resultado<OpcionVinculacionDto>> GetOpcionesVinculacion(int idRegistroInvitacion)
        {
            var response = await _httpClient.GetFromJsonAsync<Resultado<OpcionVinculacionDto>>("/RegistroInvitacion/GetOpcionesVinculacion/" + idRegistroInvitacion);
            return response;
        }

        public async Task<Resultado> AddActorRegistroInvitacion(ActorInvitacionRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("/RegistroInvitacion/AddActorRegistroInvitacion", request);

            var result = await response.Content.ReadFromJsonAsync<Resultado>();
            return result;
        }

        public async Task<Resultado<DetalleInvitacionDto>> GetActorRegistroInvitacionById(int idRegistroInvitacion)
        {
            var response = await _httpClient.GetFromJsonAsync<Resultado<DetalleInvitacionDto>>("/RegistroInvitacion/GetActorRegistroInvitacion/" + idRegistroInvitacion);
            return response;
        }

        public async Task<Resultado<DetalleValidarInvitacionDto>> GetDetalleInvitacionBy(int idEscenario, int idActor)
        {
            var response = await _httpClient.GetFromJsonAsync<Resultado<DetalleValidarInvitacionDto>>($"/Invitacion/GetInvitacionDe/{idEscenario}/{idActor}");
            return response;
        }

    }
}
