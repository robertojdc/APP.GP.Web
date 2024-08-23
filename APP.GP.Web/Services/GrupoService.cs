using APP.GP.Web.Model;

namespace APP.GP.Web.Services;

public class GrupoService
{
    private readonly HttpClient _httpClient;

    public GrupoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Grupo>> GetGruposAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<List<Grupo>>("/Actor/GetGrupos");
        return response;
    }

    //public async Task<List<Grupo>> GetCatalogoAsync(int idCatalogo)
    //{
    //    var response = await _httpClient.GetFromJsonAsync<List<Grupo>>($"/Actor/GetCatalogoAsync/{idCatalogo}");
    //    return response;
    //}

    //public async Task<Resultado> AddActorAsync(Actor actor)
    //{
    //    var response = await _httpClient.PostAsJsonAsync("/Actor/AddActor", actor);
    //    response.EnsureSuccessStatusCode();

    //    var result = await response.Content.ReadFromJsonAsync<Resultado>();
    //    return result;
    //}

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
}