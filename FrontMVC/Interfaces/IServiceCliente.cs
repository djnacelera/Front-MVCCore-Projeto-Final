namespace FrontMVC.Interfaces
{
    public interface IServiceCliente<TEntity> : IService<TEntity>
    {
        Task<TEntity> FiltrarPorCpf(string cpf);
    }
}
