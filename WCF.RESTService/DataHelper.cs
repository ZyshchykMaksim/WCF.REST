using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using Res = WCF.RESTService.Properties.Resource;

namespace WCF.RESTService
{

    public static class DataHelper
    {
        public static List<User> GetUsers()
        {
            List<User> users = new List<User>();
            XDocument document = XDocument.Parse(Res.Users);
            if (document.Descendants("user").Any())
            {
                foreach (var itemUser in document.Descendants("user"))
                {
                    User user = new User();
                    user.Name = itemUser.Attribute("name").Value;
                    user.YearOfBirth = Convert.ToDateTime(itemUser.Attribute("yearofbirth").Value);
                    user.Phone = itemUser.Attribute("phone").Value;
                    user.Email = itemUser.Attribute("email").Value;

                    users.Add(user);
                }
            }

            return users;
        }
    }
}
