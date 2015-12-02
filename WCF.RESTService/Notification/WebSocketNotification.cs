using System;
using System.Diagnostics;
using WebSocketSharp.Server;

namespace WCF.RESTService.Notification
{
    public class WebSocketNotification : INotification
    {
        private static WebSocketServer _wssv = (WebSocketServer)null;
        private bool _isDisplayConsole = true;
        private bool _isLogFile = false;

        public WebSocketNotification(string url, bool isDisplayConsole, bool isLogFile)
        {
            try
            {
                this._isDisplayConsole = isDisplayConsole;
                this._isLogFile = isLogFile;
                WebSocketNotification._wssv = new WebSocketServer(url);
                WebSocketNotification._wssv.AddWebSocketService<DefaultWebSocketBehavior>("/");
                WebSocketNotification._wssv.Start();
            }
            catch (System.Exception ex)
            {
                if (WebSocketNotification._wssv != null)
                    WebSocketNotification._wssv.Stop();

                if (isDisplayConsole)
                {
                    Console.WriteLine(ex.Message);
                }

                throw;
            }
        }

        public void Send(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg) || (WebSocketNotification._wssv == null || WebSocketNotification._wssv.WebSocketServices.Count <= 0))
                return;
            WebSocketNotification._wssv.WebSocketServices.Broadcast(msg);
            string message = string.Format("Push notification websocket => {0}", (object)msg);
            if (this._isDisplayConsole)
                Debug.WriteLine(message);

        }

    }
}
