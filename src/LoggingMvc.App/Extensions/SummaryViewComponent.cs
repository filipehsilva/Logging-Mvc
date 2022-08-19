using LoggingMvc.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoggingMvc.App.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        private readonly INotifier _notifier;

        public SummaryViewComponent(INotifier notifier)
        {
            _notifier = notifier;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notificacoes = await Task.FromResult(_notifier.GetNotifications());
            notificacoes.ForEach(c => ViewData.ModelState.AddModelError(string.Empty, c.Message));

            return View();
        }
    }
}
