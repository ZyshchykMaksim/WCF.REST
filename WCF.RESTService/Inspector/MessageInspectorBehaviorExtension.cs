using System;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace WCF.RESTService.Inspector
{
    public class MessageInspectorBehaviorExtension : BehaviorExtensionElement
    {
        private const string IsDisplayConsolePropName = "IsDisplayConsole";

        private const string IsLogFilePropName = "IsLogFile";

        [ConfigurationProperty(IsDisplayConsolePropName, DefaultValue = true)]
        public bool IsDisplayConsole
        {
            get { return (bool)base[IsDisplayConsolePropName]; }
            set { base[IsDisplayConsolePropName] = value; }
        }

        [ConfigurationProperty(IsLogFilePropName, DefaultValue = false)]
        public bool IsLogFile
        {
            get { return (bool)base[IsLogFilePropName]; }
            set { base[IsLogFilePropName] = value; }
        }

        public override Type BehaviorType
        {
            get { return typeof(MessageInspectorBehavior); }
        }

        protected override object CreateBehavior()
        {
            Console.WriteLine("MessageInspector");
            return new MessageInspectorBehavior(this.IsDisplayConsole, this.IsLogFile);
        }
    }
}
