using APP.GP.Web.Model;
using APP.GP.Web.Model.DTO;
using APP.GP.Web.Model.Filtros;
using Newtonsoft.Json;
using System.Text;

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

    public async Task<Resultado> DelActorAsync(int idActor)
    {
        var response = await _httpClient.PostAsJsonAsync<Actor>("/Actor/DelActor", new Actor { IdActor = idActor });
        return await response.Content.ReadFromJsonAsync<Resultado>();
    }

    public async Task<HttpResponseMessage> AddActorAsync(Actor actor)
    {

        try
        {
            var json = JsonConvert.SerializeObject(actor);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PostAsJsonAsync("/Actor/AddActor", actor);

            if (!response.IsSuccessStatusCode)
                Console.WriteLine($"Error en la solicitud: {await response.Content.ReadAsStringAsync()}");

            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Excepción durante el envío: {ex.Message}");
            throw;
        }
    }

    public async Task<HttpResponseMessage> EditActorAsync(Actor actor)
    {

        try
        {
            var json = JsonConvert.SerializeObject(actor);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PostAsJsonAsync("/Actor/UpdActor", actor);

            if (!response.IsSuccessStatusCode)
                Console.WriteLine($"Error en la solicitud: {await response.Content.ReadAsStringAsync()}");

            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Excepción durante el envío: {ex.Message}");
            throw;
        }
    }
    public async Task<Actor> GetActorByIdAsync(int idActor)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<Resultado<Actor>>($"/Actor/GetActorById/{idActor}");

            return response.Objeto;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener el actor: {ex.Message}");
            throw;
        }
    }

    public async Task<List<AfinidadDto>> GetAfinidades()
    {
        var response = await _httpClient.GetFromJsonAsync<List<AfinidadDto>>("/Actor/Afinidad");
        return response;
    }
    public async Task<List<Categoria>> GetCategoriasAsync(int idSubGrupo)
    {
        var response = await _httpClient.GetFromJsonAsync<List<Categoria>>($"/Categoria/GetCategorias/{idSubGrupo}");
        return response;
    }

    public async Task<List<Categoria>> GetCategoriasTablero(int idSubGrupo)
    {
        var response = await _httpClient.GetFromJsonAsync<List<Categoria>>($"/Categoria/GetCategoriasTablero/{idSubGrupo}");
        return response;
    }

    public async Task<List<Categoria>> GetCategoriasGerarquicoAsync(int idSubGrupo)
    {
        var response = await _httpClient.GetFromJsonAsync<List<Categoria>>($"/Categoria/GetCategoriasGerarquicoAsync/{idSubGrupo}");
        return response;
    }

    public async Task<List<Categoria>> GetSubCategoriasAsync(int idCategoria)
    {
        var response = await _httpClient.GetFromJsonAsync<List<Categoria>>($"/Categoria/GetSubCategorias/{idCategoria}");
        return response;
    }

    public async Task<List<CatalogoGrupo>> GetCatalogoGruposAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<List<CatalogoGrupo>>("/Grupo/GetGrupos");
        return response;
    }

    public async Task<Resultado<CatalogoGrupo>> GetGrupoByIdAsync(int idGrupo)
    {
        var response = await _httpClient.GetFromJsonAsync<Resultado<CatalogoGrupo>>("/Grupo/GetGrupoById?idGrupo=" + idGrupo);
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

        if (filtro.Tipo.HasValue)
            query.Add("Tipo", filtro.Tipo.Value.ToString());

        var queryString = string.Join("&", query.Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value)}"));

        var response = await _httpClient.GetFromJsonAsync<List<Actor>>($"/Actor/GetActores?{queryString}");

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

    public async Task<Resultado> InsCatalogoCategoria(Categoria categoria)
    {
        var response = await _httpClient.PostAsJsonAsync<Categoria>("/Categoria/AddCategoria", categoria);

        return await response.Content.ReadFromJsonAsync<Resultado>();
    }

    public async Task<Categoria> GetCategoriaByIdAsync(int idCat)
    {
        var response = await _httpClient.GetFromJsonAsync<Categoria>($"/Categoria/GetCategoriaById/{idCat}");
        return response;
    }

    public async Task<Resultado> UpdCatalogoCategoriaAsync(Categoria categoria)
    {
        var response = await _httpClient.PostAsJsonAsync<Categoria>("/Categoria/UpdCategoria", categoria);

        return await response.Content.ReadFromJsonAsync<Resultado>();
    }

    public async Task<Resultado> DelCatalogoCategoriaAsync(Categoria categoria)
    {
        var response = await _httpClient.PostAsJsonAsync<Categoria>("/Categoria/DelCategoria", categoria);

        return await response.Content.ReadFromJsonAsync<Resultado>();
    }
}