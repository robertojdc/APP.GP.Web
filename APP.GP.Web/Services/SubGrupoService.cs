using APP.GP.Web.Model;

namespace APP.GP.Web.Services
{
    public class SubGrupoService
    {
        private readonly HttpClient _httpClient;

        public SubGrupoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CatalogoSubGrupo>> GetSubGruposAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<CatalogoSubGrupo>>("/SubGrupo/GetSubGrupos");
            return response;
        }

        public async Task<Resultado<CatalogoSubGrupo>> GetSubGrupoByIdAsync(int idSubGrupo)
        {
            var response = await _httpClient.GetFromJsonAsync<Resultado<CatalogoSubGrupo>>("/SubGrupo/GetSubGrupoById?idSubGrupo=" + idSubGrupo);
            return response;
        }

        public async Task<Resultado> InsSubGrupoAsync(CatalogoSubGrupo objeto)
        {
            var response = await _httpClient.PostAsJsonAsync("/SubGrupo/AddSubGrupo", objeto);

            return await response.Content.ReadFromJsonAsync<Resultado>();
        }

        public async Task<Resultado> UpdSubGrupoAsync(CatalogoSubGrupo objeto)
        {
            var response = await _httpClient.PutAsJsonAsync("/SubGrupo/UpdSubGrupo", objeto);

            return await response.Content.ReadFromJsonAsync<Resultado>();
        }

        public async Task<Resultado> DelSubGrupoAsync(CatalogoSubGrupo c)
        {
            var response = await _httpClient.PostAsJsonAsync("/SubGrupo/DelSubGrupo", c);

            return await response.Content.ReadFromJsonAsync<Resultado>();
        }
    }
}
