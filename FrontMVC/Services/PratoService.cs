using FrontMVC.Interfaces;
using FrontMVC.Models.Prato;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace FrontMVC.Services
{
    public class PratoService : IService<PratoModel>
    {
        HttpClient client = new HttpClient();
        private IConfiguration configuration;
        private TokenService tokenService;

        public PratoService(IConfiguration configuration, TokenService tokenService)
        {
            this.configuration = configuration;
            this.tokenService = tokenService;
        }
        public async Task<IEnumerable<PratoModel>> Listar()
        {
            string token = tokenService.GetToken();
            List<PratoModel>? list = new List<PratoModel>();
            client.BaseAddress = new Uri(configuration["EndPointsDEV:API_Prato"]);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.GetAsync("Listar");

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<PratoModel>>(dados);
            }
            return list;
        }
        public Task<PratoModel> Adicionar(PratoModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<PratoModel> Atualizar(PratoModel entity, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Excluir(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PratoModel> FiltrarId(Guid id)
        {
            throw new NotImplementedException();
        }




    }
}
