using LoggingMvc.Business.Models;

namespace LoggingMvc.Business.Interfaces
{
    public interface ILogService : IDisposable
    {
        Task<IEnumerable<Log>> GetAll();
        Task<Log> GetById(Guid id);
        Task Add(Log log);
        Task Update(Log log);
    }
}
