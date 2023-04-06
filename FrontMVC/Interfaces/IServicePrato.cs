namespace FrontMVC.Interfaces
{
    public interface IServicePrato<TEntity> : IService<TEntity>
    {
        Task<string> ConverteImg(IFormFile foto);

    }
}
