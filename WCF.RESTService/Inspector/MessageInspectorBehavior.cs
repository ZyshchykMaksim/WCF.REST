using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Xml;


namespace WCF.RESTService.Inspector
{
    using NLog;

    public class MessageInspectorBehavior : IDispatchMessageInspector, IEndpointBehavior
    {

        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public bool IsDisplayConsole { get; private set; }

        public bool IsLogFile { get; private set; }

        public event EventHandler OnMessage;

        public MessageInspectorBehavior(bool isDisplayConsole, bool isLogFile)
        {
            IsDisplayConsole = isDisplayConsole;
            IsLogFile = isLogFile;
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(this);
        }

        public void Validate(ServiceEndpoint endpoint) { }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime) { }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) { }


        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            MessageInfo messageInfo = new MessageInfo
            {
                ServerTimeStamp = DateTime.Now,
                Platform = "WCF"
            };

            OperationDescription operationDesc = GetOperationDescription(ref messageInfo, OperationContext.Current);
            if (operationDesc != null)
            {
                Type contractType = operationDesc.DeclaringContract.ContractType;
                messageInfo.Action = operationDesc.Name;
                messageInfo.TypeName = contractType.FullName;
                messageInfo.AssemblyName = contractType.Assembly.GetName().Name;
            }

            messageInfo.ServerName = Dns.GetHostName();
            messageInfo.ServerProcessName = AppDomain.CurrentDomain.FriendlyName;

            string message = String.Format("Request => {0} {1}", messageInfo.ServerTimeStamp.ToString(CultureInfo.InvariantCulture), messageInfo.Request);

            if (IsDisplayConsole)
            {
                Console.WriteLine();
                Console.WriteLine(message);
            }

            if (IsLogFile)
            {
                _logger.Log(LogLevel.Info, message);
            }

            if (OnMessage != null)
            {
                OnMessage(this, new MessageInspectorArgs
                {
                    MessageInspectionType = MessageInspectionType.Request,
                    Message = messageInfo
                });
            }

            return messageInfo;
        }


        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            if (correlationState != null)
            {
                MessageInfo messageInfo = correlationState as MessageInfo;
                if (messageInfo != null)
                {
                    messageInfo.ServerEndTimeStamp = DateTime.Now;

                    MessageBuffer mb = reply.CreateBufferedCopy(int.MaxValue);
                    Message responseMsg = mb.CreateMessage();
                    reply = mb.CreateMessage();


                    using (XmlDictionaryReader reader = responseMsg.GetReaderAtBodyContents())
                    {
                        messageInfo.Response = reader.ReadOuterXml();
                                      
                    }

                    if (reply.IsFault)
                    {
                        messageInfo.IsError = true;
                    }

                    string message = String.Format("Response => {0} {1}", messageInfo.ServerEndTimeStamp.ToString(CultureInfo.InvariantCulture), messageInfo.Response);
                    if (IsDisplayConsole)
                    {
                        Console.WriteLine(message);
                    }

                    if (IsLogFile)
                    {
                        _logger.Log(LogLevel.Info, message);
                    }
                }

                if (OnMessage != null)
                {
                    OnMessage(this, new MessageInspectorArgs
                    {
                        MessageInspectionType = MessageInspectionType.Response,
                        Message = messageInfo
                    });
                }
            }
        }

        private OperationDescription GetOperationDescription(ref MessageInfo messageInfo, OperationContext operationContext)
        {
            OperationDescription od = null;
            string bindingName = operationContext.EndpointDispatcher.ChannelDispatcher.BindingName;
            string methodName;
            if (bindingName.Contains("WebHttpBinding"))
            {
                //REST request
                methodName = (string)operationContext.IncomingMessageProperties["HttpOperationName"];
                messageInfo.Request = operationContext.IncomingMessageProperties["Via"].ToString();
            }
            else
            {
                //SOAP request
                string action = operationContext.IncomingMessageHeaders.Action;
                methodName = operationContext.EndpointDispatcher.DispatchRuntime.Operations.FirstOrDefault(o => o.Action == action).Name;
            }

            EndpointAddress epa = operationContext.EndpointDispatcher.EndpointAddress;
            ServiceDescription hostDesc = operationContext.Host.Description;
            ServiceEndpoint ep = hostDesc.Endpoints.Find(epa.Uri);

            if (ep != null)
            {
                od = ep.Contract.Operations.Find(methodName);
            }

            return od;
        }
    }
}
