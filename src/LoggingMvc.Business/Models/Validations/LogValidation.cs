using FluentValidation;

namespace LoggingMvc.Business.Models.Validations
{
    public class LogValidation : AbstractValidator<Log>
    {
        public LogValidation()
        {
            RuleFor(l => l.Date)
                .NotEmpty();

            RuleFor(l => l.Type)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(l => l.Description)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}
