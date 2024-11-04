using APP.GP.Web.Model;

namespace APP.GP.Web.Services
{
    public class RegistroInvitacionService
    {
        private readonly HttpClient _httpClient;

        public RegistroInvitacionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RegistroInvitacion>> GetRegistroInvitacion()
        {
            var response = await _httpClient.GetFromJsonAsync<List<RegistroInvitacion>>("/RegistroInvitacion/GetRegistroInvitacion");
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
    }
}
