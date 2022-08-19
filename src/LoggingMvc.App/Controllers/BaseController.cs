using LoggingMvc.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoggingMvc.App.Controllers
{
    public class BaseController : Controller
    {
        private readonly INotifier _notifier;

        public BaseController(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected bool IsValid()
        {
           return !_notifier.HasNotifications();
        }
    }
}
