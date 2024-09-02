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

    public async Task<List<CatalogoGrupo>> GetCatalogoGruposAsync()
    {
        var response  = await _httpClient.GetFromJsonAsync<List<CatalogoGrupo>>("/Grupo/GetGrupos");
        return response;
    }

    public async Task<Resultado<CatalogoGrupo>> GetGrupoByIdAsync(int idGrupo)
    {
        var response = await _httpClient.GetFromJsonAsync<Resultado<CatalogoGrupo>>("/Grupo/GetGrupoById?idGrupo=" + idGrupo);
        return response;
    }

    public async Task<Resultado> InsCatalogoGrupoAsync(CatalogoGrupo objeto)
    {
        var response = await _httpClient.PostAsJsonAsync<CatalogoGrupo>("/Grupo/AddGrupo", objeto);

        return await response.Content.ReadFromJsonAsync<Resultado>();
    }

    public async Task<Resultado> UpdCatalogoGrupoAsync(CatalogoGrupo objeto)
    {
        var response = await _httpClient.PutAsJsonAsync<CatalogoGrupo>("/Grupo/UpdGrupo", objeto);

        return await response.Content.ReadFromJsonAsync<Resultado>();
    }

    public async Task<Resultado> DelCatalogoGrupoAsync(int idGrupo)
    {
        var response = await _httpClient.DeleteAsync("/Grupo/DelGrupo?idGrupo=" + idGrupo);

        return await response.Content.ReadFromJsonAsync<Resultado>();
    }
}