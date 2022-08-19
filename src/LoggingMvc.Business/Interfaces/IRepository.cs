using LoggingMvc.Business.Models;

namespace LoggingMvc.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Add(TEntity entity);
        Task<TEntity> GetById(Guid id);
        Task<IEnumerable<TEntity>> GetAll();
        Task Update(TEntity entity);
        Task Delete(Guid id);
        Task<int> SaveChanges();
    }
}
