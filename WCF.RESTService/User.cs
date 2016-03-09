using System;
using System.Runtime.Serialization;

namespace WCF.RESTService
{
    [DataContract]
    public class User
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public DateTime YearOfBirth { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public string Email { get; set; }

        public override string ToString()
        {
            return String.Format("Name: {0}, Year of birth: {1}, Phone:{2}, Email: {3}", this.Name, this.YearOfBirth, this.Phone, this.Email);
        }
    }
}
