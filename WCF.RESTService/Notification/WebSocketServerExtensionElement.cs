using System;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace WCF.RESTService.Notification
{
		public class WebSocketServerExtensionElement : BehaviorExtensionElement
		{
				private const string IsLogFilePropName = "IsLogFile";
				private const string UrlPropName = "Url";


				[ConfigurationProperty(UrlPropName, DefaultValue = "")]
				public string Url
				{
						get { return (string)base[UrlPropName]; }
						set { base[UrlPropName] = value; }
				}
		
				[ConfigurationProperty(IsLogFilePropName, DefaultValue = false)]
				public bool IsLogFile
				{
						get { return (bool)base[IsLogFilePropName]; }
						set { base[IsLogFilePropName] = value; }
				}

				protected override object CreateBehavior()
				{
						return new WebSocketServiceBehavior(this.Url, this.IsLogFile);
				}

				public override Type BehaviorType { get { return typeof(WebSocketServiceBehavior); } }
		}
}
