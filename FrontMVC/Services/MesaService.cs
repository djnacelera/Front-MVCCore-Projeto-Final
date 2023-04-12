using AutoMapper;
using FrontMVC.Helpers;
using FrontMVC.Interfaces;
using FrontMVC.Models.Mesa;
using FrontMVC.Models.Prato;
using Newtonsoft.Json;

namespace FrontMVC.Services
{
    public class MesaService : IService<MesaModel>
    {
        private readonly ClientHelpers _client;
        private IConfiguration configuration;
        private readonly IMapper _mapper;

        public MesaService(ClientHelpers client, IConfiguration configuration, IMapper mapper)
        {
            _client = client;
            this.configuration = configuration;
            _mapper= mapper;
        }

        public async Task<MesaModel> Adicionar(MesaModel entity)
        {     
            MesaIncluir mesaIncluir= _mapper.Map<MesaIncluir>(entity);
            HttpResponseMessage response = await _client.gerarClienComTokenPost().PostAsJsonAsync(configuration["EndPointsDEV:API_Mesa"] + "Incluir", mesaIncluir);
            if (response.IsSuccessStatusCode)
            {
                string sc = "Sucesso";
            }
            return entity;
        }
      

        public Task<MesaModel> Atualizar(MesaModel entity, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Excluir(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<MesaModel> FiltrarId(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MesaModel>> Listar()
        {
            List<MesaModel>? list = new List<MesaModel>();

            HttpResponseMessage response = await _client.gerarClienComToken(configuration["EndPointsDEV:API_Mesa"]).GetAsync("Listar");

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<MesaModel>>(dados);
            }
            return list;
        }

      
    }
}
