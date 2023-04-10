using FrontMVC.Interfaces;
using FrontMVC.Models.Mesa;
using FrontMVC.Models.Prato;

namespace FrontMVC.Services
{
    public class MesaService : IServicePrato<MesaModel>
    {
        public Task<MesaModel> Adicionar(MesaModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<MesaModel> Atualizar(MesaModel entity, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<string> ConverteImg(IFormFile foto)
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

        public Task<IEnumerable<MesaModel>> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
