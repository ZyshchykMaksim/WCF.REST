namespace WCF.RESTService.Notification
{
    public class NotificationManager
    {
        public static INotification Notification { get; private set; }
        public void Create(string url, bool isLogFile)
        {
            Notification = new WebSocketNotification(url, isLogFile);
        }
    }
}
