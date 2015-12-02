using System.Runtime.Serialization.Json;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using NLog;

namespace WCF.RESTService.Exception
{
    public class ServiceErrorsHandler : IErrorHandler
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public void ProvideFault(System.Exception error, MessageVersion version, ref Message fault)
        {
            //This condition is needed to skip handling the WebFaultException
            //since it is already handled in the layers below  (I have used WebFaultException<string>
            //In case you use different type, add the condition for that)
            if (!(error is System.ServiceModel.Web.WebFaultException))
            {

                //I have used XML type; so why I am using DataContractSerializer
                //If you want to use JSON type, use DataContractJSONSerializer
                fault = Message.CreateMessage(version, string.Empty, new CustomException("Error", error.Message), new DataContractJsonSerializer(typeof(CustomException)));
                fault.Properties.Add(WebBodyFormatMessageProperty.Name, new WebBodyFormatMessageProperty(WebContentFormat.Json));

                HttpResponseMessageProperty prop = new HttpResponseMessageProperty();
                prop.StatusCode = System.Net.HttpStatusCode.Conflict;
                fault.Properties[HttpResponseMessageProperty.Name] = prop;
            }
        }

        public bool HandleError(System.Exception error)
        {
            _logger.Log(LogLevel.Error, error);
            return true;
        }
    }
}
