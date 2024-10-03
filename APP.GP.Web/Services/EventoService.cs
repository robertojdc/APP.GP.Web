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
        var response = await _httpClient.GetFromJsonAsync<List<Disposicion>>($"/Eventos/GetDisposicionesForEscenario/{idEvento}/{idEscenatio}");
        return response;
    }

    public async Task<List<Actor>> GetBusquedaActor(string searchTerm)
    {
        var response = await _httpClient.GetFromJsonAsync<List<Actor>>($"/Actor/GetBusquedaActor");
        return response;
    }

    public async Task<HttpResponseMessage> AddActorEscenario(ActorEscenario request)
    {
        var response = await _httpClient.PostAsJsonAsync("/Eventos/AddActorEscenario", request);
        return response;
    }
    public async Task<Resultado> AddDisposicion(Disposicion request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync<Disposicion>("/Eventos/AddDisposicion", request);

            return await response.Content.ReadFromJsonAsync<Resultado>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener el actor: {ex.Message}");
            throw;
        }
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

    public async Task<List<Actor>> GetActore()
    {
        var response = await _httpClient.GetFromJsonAsync<Resultado<Actor>>("/Actor/GetActores");
        return response.Lista;
    }
}