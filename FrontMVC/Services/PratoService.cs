﻿using FrontMVC.Helpers;
using FrontMVC.Interfaces;
using FrontMVC.Models.Prato;
using Microsoft.EntityFrameworkCore;
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
<<<<<<< HEAD
            string token = tokenService.GetToken();

            client.BaseAddress = new Uri(configuration["EndPointsDEV:API_Prato"]);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string json = JsonConvert.SerializeObject(pratoModel);
=======
       
            string json = JsonConvert.SerializeObject(pratoModel); 
>>>>>>> main
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.gerarClienComToken(configuration["EndPointsDEV:API_Prato"]).PostAsync("Incluir", httpContent);

            if (response.IsSuccessStatusCode)
            {
                string sc = "Sucesso";
            }

            return pratoModel;  
        }

        public async Task<PratoModel> Atualizar(PratoModel entity, Guid id)
        {
            string token = tokenService.GetToken();

            client.BaseAddress = new Uri(configuration["EndPointsDEV:API_Prato"]);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string json = JsonConvert.SerializeObject(entity);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");


            HttpResponseMessage response = await client.PutAsync($"Alterar/{id}", httpContent);

            if (response.IsSuccessStatusCode)
            {
                string sc = "Sucesso";
            }

            return entity;
        }

        public async Task<bool> Excluir(Guid id)
        {
            string token = tokenService.GetToken();

            client.BaseAddress = new Uri(configuration["EndPointsDEV:API_Prato"]);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string json = JsonConvert.SerializeObject(id);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");


            HttpResponseMessage response = await client.DeleteAsync($"Deletar/{id}");

            bool delete = false;
            if (response.IsSuccessStatusCode)
            {
                delete = true;
            }

            return delete;
        }

        public async Task<PratoModel> FiltrarId(Guid id)
        {
            string token = tokenService.GetToken();
            List<PratoModel>? list = new List<PratoModel>();

            client.BaseAddress = new Uri(configuration["EndPointsDEV:API_Prato"]);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync("Listar");
            PratoModel prato = new PratoModel();

            if (response.IsSuccessStatusCode)
            {
                var dados = response.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<PratoModel>>(dados);
                prato = list.Find(x => x.Id == id);
            }
            return prato;
            //string token = tokenService.GetToken();
            //PratoModel prato = new PratoModel();

            //client.BaseAddress = new Uri(configuration["EndPointsDEV:API_Prato"]);
            //client.DefaultRequestHeaders.Clear();
            //client.DefaultRequestHeaders.Accept.Add(
            //new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //HttpResponseMessage response = await client.GetAsync($"FiltrarPorId​/{id}");


            //if (response.IsSuccessStatusCode)
            //{
            //    var dados = response.Content.ReadAsStringAsync().Result;
            //    prato = JsonConvert.DeserializeObject<PratoModel>(dados);
            //}
            //return prato;
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

        public async Task<string> AtivarPrato(Guid id,PratoModel prato)
        {
            //PratoModel prato =await FiltrarId(id);

            if (prato.Status)
                return "Prato já está Ativo";

            prato.Status = true;

            string token = tokenService.GetToken();

            client.BaseAddress = new Uri(configuration["EndPointsDEV:API_Prato"]);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string json = JsonConvert.SerializeObject(prato);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync($"AtivarPrato/{id}", httpContent);
            string sc = "Erro";

            if (response.IsSuccessStatusCode)
            {
                sc = "Prato ativo com sucesso!";
            }

            return sc;
        }
        public async Task<string> InativarPrato(Guid id)
        {
            PratoModel prato = await FiltrarId(id);

            if (!prato.Status)
                return "Prato ja esta Inativo!";

            prato.Status = false;

            await Atualizar(prato, id);

            return $"Prato Inativo com sucesso!";
        }
    }
}
