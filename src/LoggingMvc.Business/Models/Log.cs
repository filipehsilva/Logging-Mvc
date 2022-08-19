namespace LoggingMvc.Business.Models
{
    public class Log : Entity
    {
        public DateTime Date { get; set; }
        public string Type { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
    }
}
