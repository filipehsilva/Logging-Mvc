using LoggingMvc.Business.Notifications;

namespace LoggingMvc.Business.Interfaces
{
    public interface INotifier
    {
        bool HasNotifications();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
