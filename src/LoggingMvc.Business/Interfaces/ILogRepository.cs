using LoggingMvc.Business.Models;

namespace LoggingMvc.Business.Interfaces
{
    public interface ILogRepository : IRepository<Log>
    {
        Task<IEnumerable<Log>> GetAllFromJson();
        Task<Log> GetByIdFromJson(Guid id);
        Task<bool> AddToJson(Log log);
        Task<bool> UpdateLogFromJson(Log log);
    }
}
