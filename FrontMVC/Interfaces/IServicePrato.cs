namespace FrontMVC.Interfaces
{
    public interface IServicePrato<TEntity> : IService<TEntity>
    {
        Task<string> ConverteImg(IFormFile foto);
        Task<string> AtivarPrato(Guid id);
        Task<string> InativarPrato(Guid id);
    }
}
