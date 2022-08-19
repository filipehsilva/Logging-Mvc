using System.ComponentModel.DataAnnotations;

namespace LoggingMvc.App.ViewModels
{
    public class LogViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
        [Required]
        public string Type { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
