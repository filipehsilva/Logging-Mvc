using LoggingMvc.Business.Interfaces;

namespace LoggingMvc.Business.Notifications
{
    public class Notifier : INotifier
    {
        private List<Notification> _notifications;
        public Notifier()
        {
            _notifications = new List<Notification>();
        }

        public List<Notification> GetNotifications()
        {
            return _notifications;
        }

        public void Handle(Notification notification)
        {
            _notifications.Add(notification);
        }

        public bool HasNotifications()
        {
            return _notifications.Any();
        }
    }
}
