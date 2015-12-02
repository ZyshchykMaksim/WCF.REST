using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace WCF.RESTService.Notification
{
    public class WebSocketServiceBehavior : IServiceBehavior
    {
        public string Url { get; private set; }

        public bool IsDisplayConsole { get; private set; }

        public bool IsLogFile { get; private set; }

        public WebSocketServiceBehavior() { }

        public WebSocketServiceBehavior(string url, bool isDisplayConsole, bool isLogFile)
        {
            IsLogFile = isLogFile;
            IsDisplayConsole = isDisplayConsole;
            Url = url;
        }
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase) { }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters) { }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            NotificationManager notificationManager = new NotificationManager();
            notificationManager.Create(this.Url, this.IsDisplayConsole, this.IsLogFile);
        }
    }
}
