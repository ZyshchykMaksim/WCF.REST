using System;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace WCF.RESTService.Exception
{
    public class ErrorsHandlerServiceBehaviorExtension : BehaviorExtensionElement
    {
        private const string IsLogFilePropName = "IsLogFile";

        [ConfigurationProperty(IsLogFilePropName, DefaultValue = true)]
        public bool IsLogFile
        {
            get { return (bool)base[IsLogFilePropName]; }
            set { base[IsLogFilePropName] = value; }
        }

        protected override object CreateBehavior()
        {
            Console.WriteLine("Create ErrorsHandler");
            return new ErrorsHandlerServiceBehavior(this.IsLogFile);
        }

        public override Type BehaviorType { get { return typeof(ErrorsHandlerServiceBehavior); } }
    }
}
