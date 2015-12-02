using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using WCF.RESTService.Notification;

namespace WCF.RESTService.Interface
{
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface IRESTService
    {

        INotification Notification { get; }

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetAllUsers", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        List<User> GetAllUsers();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/SetUser", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        void SetUser(User user);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetUser", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        User GetUser();
    }
}
