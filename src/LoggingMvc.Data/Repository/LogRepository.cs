using LoggingMvc.Business;
using LoggingMvc.Business.Interfaces;
using LoggingMvc.Business.Models;
using LoggingMvc.Data.Context;
using System.Text.Json;

namespace LoggingMvc.Data.Repository
{
    public class LogRepository : Repository<Log>, ILogRepository
    {
        public LogRepository(LogDbContext context) : base(context) { }

        public async Task<IEnumerable<Log>> GetAllFromJson()
        {
            var filePath = @$"{Settings.JsonPath}";
            var Logs = new List<Log>();

            if (File.Exists(filePath))
            {
                try
                {
                    var jsonData = await File.ReadAllTextAsync(filePath);
                    Logs = JsonSerializer.Deserialize<List<Log>>(jsonData);
                }
                catch (Exception)
                {
                    return Logs;
                }
            }
            return Logs;
        }

        public async Task<Log> GetByIdFromJson(Guid id)
        {
            var filePath = @$"{Settings.JsonPath}";

            if (File.Exists(filePath))
            {
                try
                {
                    var jsonData = await File.ReadAllTextAsync(filePath);
                    var Logs = JsonSerializer.Deserialize<List<Log>>(jsonData);

                    return Logs.FirstOrDefault(l => l.Id == id);
                }
                catch (Exception)
                {
                    return null!;
                }
            }
            return null!;
        }

        public async Task<bool> AddToJson(Log log)
        {
            log.Id = Guid.NewGuid();
            var filePath = @$"{Settings.JsonPath}";
            try
            {
                if (File.Exists(filePath))
                {
                    var jsonData = await File.ReadAllTextAsync(filePath);
                    var logList = JsonSerializer.Deserialize<List<Log>>(jsonData) ?? new List<Log>();
                    logList.Add(log);
                    jsonData = JsonSerializer.Serialize(logList);
                    File.WriteAllText(filePath, jsonData);
                }
                else
                {
                    var logList = new List<Log>();
                    logList.Add(log);
                    var jsonData = JsonSerializer.Serialize(logList);
                    File.WriteAllText(filePath, jsonData);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateLogFromJson(Log log)
        {
            var filePath = @$"{Settings.JsonPath}";
            try
            {
                if (File.Exists(filePath))
                {
                    var jsonData = await File.ReadAllTextAsync(filePath);
                    var logList = JsonSerializer.Deserialize<List<Log>>(jsonData);

                    var obj = logList.FirstOrDefault(l => l.Id == log.Id);
                    if (obj == null)
                        return false;

                    obj.Date = log.Date;
                    obj.Type = log.Type;
                    obj.Description = log.Description;

                    jsonData = JsonSerializer.Serialize(logList);
                    File.WriteAllText(filePath, jsonData);
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
