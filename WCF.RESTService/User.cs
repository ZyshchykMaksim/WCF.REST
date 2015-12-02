using System;
using System.Runtime.Serialization;

namespace WCF.RESTService
{
    [DataContract]
    public class User
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public DateTime Time { get; set; }
        public override string ToString()
        {
            return String.Format("Name: {0}, Year:{1}, Date time: {2}", this.Name, this.Year, this.Time);
        }
    }
}
