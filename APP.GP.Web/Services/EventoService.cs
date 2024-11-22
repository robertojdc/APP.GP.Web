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

    public async Task<List<Actor>> GetBusquedaActor(string searchTerm)
    {
        return await _httpClient.GetFromJsonAsync<List<Actor>>($"/Actor/GetBusquedaActor");
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

    public async Task<List<Evento>> GetEventos()
    {
        var response = await _httpClient.GetFromJsonAsync<Resultado<Evento>>("/Eventos/GetEventos");
        return response.Lista;
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
}