using System;
using System.ServiceModel.Configuration;

namespace WCF.RESTService.Cors
{
    public class CorsSupportBehaviorElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(CorsSupportBehavior); }
        }

        protected override object CreateBehavior()
        {
            Console.WriteLine("Create CorsSupport");
            return new CorsSupportBehavior();
        }
    }
}
