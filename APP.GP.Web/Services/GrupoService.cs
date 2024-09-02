using APP.GP.Web.Model;
using APP.GP.Web.Model.Filtros;

namespace APP.GP.Web.Services;

public class GrupoService
{
    private readonly HttpClient _httpClient;

    public GrupoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Grupo>> GetAllTablero()
    {
        var response = await _httpClient.GetFromJsonAsync<List<Grupo>>("/Actor/GetAllTablero");
        return response;
    }

    public async Task<List<Grupo>> GetGrupos()
    {
        var response = await _httpClient.GetFromJsonAsync<List<Grupo>>($"/Actor/GetGrupos");
        return response;
    }

    public async Task<List<Grupo>> GetSubGrupos(int idGrupo)
    {
        var response = await _httpClient.GetFromJsonAsync<List<Grupo>>($"/Actor/GetSubGrupos/{idGrupo}");
        return response;
    }

    public async Task<Resultado> AddActorAsync(Actor actor)
    {
        var response = await _httpClient.PostAsJsonAsync("/Actor/AddActor", actor);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<Resultado>();
        return result;
    }

    public async Task<List<Categoria>> GetCategoriasAsync(int idSubGrupo)
    {
        var response = await _httpClient.GetFromJsonAsync<List<Categoria>>($"/Actor/GetCategorias/{idSubGrupo}");
        return response;
    }

    public async Task<List<Categoria>> GetSubCategoriasAsync(int idCategoria)
    {
        var response = await _httpClient.GetFromJsonAsync<List<Categoria>>($"/Actor/GetSubCategorias/{idCategoria}");
        return response;
    }

    public async Task<List<Actor>> GetActoresAsync(FiltroActor filtro)
    {
        var query = new Dictionary<string, string>();

        if (!string.IsNullOrEmpty(filtro.Nombre))
            query.Add("Nombre", filtro.Nombre);

        if (!string.IsNullOrEmpty(filtro.ApellidoPaterno))
            query.Add("ApellidoPaterno", filtro.ApellidoPaterno);

        if (!string.IsNullOrEmpty(filtro.ApellidoMaterno))
            query.Add("ApellidoMaterno", filtro.ApellidoMaterno);

        if (filtro.IdGrupo.HasValue)
            query.Add("IdGrupo", filtro.IdGrupo.Value.ToString());

        if (filtro.IdSubGrupo.HasValue)
            query.Add("IdSubGrupo", filtro.IdSubGrupo.Value.ToString());

        if (filtro.IdCategoria.HasValue)
            query.Add("IdCategoria", filtro.IdCategoria.Value.ToString());

        var queryString = string.Join("&", query.Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value)}"));

        var response = await _httpClient.GetFromJsonAsync<List<Actor>>($"/Actor/GetActores?{queryString}");

        return response;
    }
}