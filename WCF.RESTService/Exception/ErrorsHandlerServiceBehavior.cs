using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace WCF.RESTService.Exception
{
    public class ErrorsHandlerServiceBehavior : IServiceBehavior
    {
        public bool IsLogFile { get; private set; }

        public ErrorsHandlerServiceBehavior() { }

        public ErrorsHandlerServiceBehavior(bool isLogFile)
        {
            IsLogFile = isLogFile;
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase) { }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters) { }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (var channelDispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                var channelDispatcher = channelDispatcherBase as ChannelDispatcher;
                if (channelDispatcher != null)
                {
                    channelDispatcher.ErrorHandlers.Add(new ServiceErrorsHandler());
                }
            }
        }
    }
}
