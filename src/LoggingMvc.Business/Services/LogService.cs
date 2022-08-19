using LoggingMvc.Business.Interfaces;
using LoggingMvc.Business.Models;
using LoggingMvc.Business.Models.Validations;
using System.IO;
using System.Text.Json;

namespace LoggingMvc.Business.Services
{
    public class LogService : BaseService, ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository, INotifier notifier) : base(notifier)
        {
            _logRepository = logRepository;
        }

        public async Task<IEnumerable<Log>> GetAll()
        {
            if (Settings.DatabaseType == "json")
            {
                return await _logRepository.GetAllFromJson();
            } else
            {
                return await _logRepository.GetAll();
            }
                
        }

        public async Task<Log> GetById(Guid id)
        {
            if (Settings.DatabaseType == "json")
            {
                return await _logRepository.GetByIdFromJson(id);
            }
            else
            {
                return await _logRepository.GetById(id);
            }
        }

        public async Task Add(Log log)
        {
            if (!RunValidation(new LogValidation(), log)) return;

            if (Settings.DatabaseType == "json")
            {
                var logToJson = await _logRepository.AddToJson(log);
                if (!logToJson)
                    Notify("An error occurred while saving the log to the json file.");
            }
            else
            {
                await _logRepository.Add(log);
            }
        }

        public async Task Update(Log log)
        {
            if (!RunValidation(new LogValidation(), log)) return;

            if (Settings.DatabaseType == "json")
            {
                var logToJson = await _logRepository.UpdateLogFromJson(log);
                if (!logToJson)
                    Notify("An error occurred while saving the log to the json file.");
            }
            else
            {
                await _logRepository.Update(log);
            }
        }

        public void Dispose()
        {
            _logRepository?.Dispose();
        }
    }
}
