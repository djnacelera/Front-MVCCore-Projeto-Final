using FrontMVC.Helpers;
using FrontMVC.Interfaces;
using FrontMVC.Models.Prato;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http.Headers;

namespace FrontMVC.Services
{
    public class PratoService : IServicePrato<PratoModel>
    {
        private readonly ClientHelpers _client;
        private IConfiguration configuration;
        

        public PratoService(IConfiguration configuration, ClientHelpers client)
        {
            this.configuration = configuration;
            _client = client;
        }
        public async Task<IEnumerable<PratoModel>> Listar()
        {
            
            List<PratoModel>? list = new List<PratoModel>();

            HttpResponseMessage response = await _client.gerarClienComToken(configuration["EndPointsDEV:API_Prato"]).GetAsync("Listar");

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<PratoModel>>(dados);
            }
            return list;
        }
        public async Task<PratoModel> Adicionar(PratoModel pratoModel)
        {
       
            string json = JsonConvert.SerializeObject(pratoModel); 
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.gerarClienComToken(configuration["EndPointsDEV:API_Prato"]).PostAsync("Incluir", httpContent);

            if (response.IsSuccessStatusCode)
            {
                string sc = "Sucesso";
            }

            return pratoModel;  
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

        public async Task<string> ConverteImg(IFormFile foto)
        {
            var FileDic = configuration["Caminho:Foto_Prato"];

            if (!Directory.Exists(FileDic))
                Directory.CreateDirectory(FileDic);

            var filePath = Path.Combine(FileDic, foto.FileName);
            using (FileStream fs = System.IO.File.Create(filePath))
            {
                foto.CopyTo(fs);
            }

            byte[] imageArray = await System.IO.File.ReadAllBytesAsync(filePath);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);

            File.Delete(filePath);

            return base64ImageRepresentation;

        }
    }
}
