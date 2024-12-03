using APP.GP.Web.Model;
using APP.GP.Web.Model.Eventos;

namespace APP.GP.Web.Services;

public class EventoService
{
    private readonly HttpClient _httpClient;

    public EventoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Disposicion>> GetDisposicionesForEscenario(int idEvento, int idEscenatio)
    {
        return await _httpClient.GetFromJsonAsync<List<Disposicion>>($"/Eventos/GetDisposicionesForEscenario/{idEvento}/{idEscenatio}");
    }

    public async Task<Disposicion> GetDetalleAsignacion(int idActor, int idEscenario)
    {
        return await _httpClient.GetFromJsonAsync<Disposicion>($"/Eventos/DetalleAsignacion/{idActor}/{idEscenario}");
    }

    public async Task<List<Actor>> GetBusquedaActor(string searchTerm, int idUsuario)
    {
        return await _httpClient.GetFromJsonAsync<List<Actor>>($"/Actor/GetBusquedaActor/{idUsuario}");
    }

    public async Task<Resultado<ActorEscenarioSimpl>> GetActoresEscenariosById(int idUsuario, int idEscenario)
    {
        var response = await _httpClient.GetFromJsonAsync<Resultado<ActorEscenarioSimpl>>($"/Actor/GetActoresEscenariosById/{idUsuario}/{idEscenario}");
        return response;
    }
    public async Task<HttpResponseMessage> AddActorEscenario(ActorEscenario request)
    {
        return await _httpClient.PostAsJsonAsync("/Eventos/AddActorEscenario", request);
    }

    public async Task<Resultado> AddDisposicion(Disposicion request)
    {
        var response = await _httpClient.PostAsJsonAsync("/Eventos/AddDisposicion", request);
        return await response.Content.ReadFromJsonAsync<Resultado>();
    }
    public async Task<int> AddInvitacion(Invitacion request)
    {
        var response = await _httpClient.PostAsJsonAsync("/Invitacion/AddInvitacion", request);
        return await response.Content.ReadFromJsonAsync<int>();
    }


    public async Task<int> EnviarInvitacionCorreo(int idInvitacion, string cuerpo, string imagen)
    {
        var response = await _httpClient.PostAsJsonAsync("/Eventos/EnviarInvitacionCorreo", new Invitacion { IdInvitacion = idInvitacion, CuerpoCorreo = cuerpo, CodigoQR = imagen });
        return response.StatusCode == System.Net.HttpStatusCode.OK ? 1 : 0;
    }

    public async Task<Resultado> AddEnvioInvitacion(EnvioInvitacion request)
    {
        var response = await _httpClient.PostAsJsonAsync("/Invitacion/AddEnvioInvitacion", request);
        return await response.Content.ReadFromJsonAsync<Resultado>();
    }
    public async Task<List<Evento>> GetEventos(int idUsuario)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<Resultado<Evento>>($"/Eventos/GetEventos/{idUsuario}");
            return response.Lista;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new List<Evento>();
        }
    }

    public async Task<List<Escenario>> GetEscenariosByEvento(int idEvento)
    {
        var response = await _httpClient.GetFromJsonAsync<Resultado<Escenario>>($"/Eventos/GetEscenarioByIdEvento/{idEvento}");
        return response.Lista;
    }

    public async Task<Escenario> GetEscenarioById(int idEscenario)
    {
        var response = await _httpClient.GetFromJsonAsync<Escenario>($"/Eventos/GetEscenarioById/{idEscenario}");
        return response;
    }


    public async Task<List<Actor>> GetActore()
    {
        var response = await _httpClient.GetFromJsonAsync<Resultado<Actor>>("/Actor/GetActores");
        return response.Lista;
    }

    public async Task<Resultado> ObtenerReporteExcel(int idEscenario)
    {
        var response = await _httpClient.GetFromJsonAsync<Resultado>($"/Eventos/ObtenerReporteExcel/{idEscenario}");
        return response;
    }
}