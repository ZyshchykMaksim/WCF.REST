using System.Runtime.Serialization;

namespace WCF.RESTService.Exception
{
    [DataContract]
    public class CustomException
    {
        public CustomException(string info, string details)
        {
            Info = info;
            Details = details;
        }

        [DataMember]
        public string Details { get; private set; }
        [DataMember]
        public string Info { get; private set; }
    }
}
