using System;
using System.Diagnostics;
using WebSocketSharp.Server;

namespace WCF.RESTService.Notification
{
	public class WebSocketNotification : INotification
	{
		private static WebSocketServer _wssv;

		private bool _isLogFile = false;

		public WebSocketNotification(string url, bool isLogFile)
		{
			try
			{

				this._isLogFile = isLogFile;
				_wssv = new WebSocketServer(url);
				_wssv.AddWebSocketService<DefaultWebSocketBehavior>("/");
				_wssv.Start();
			}
			catch (System.Exception ex)
			{
				if (_wssv != null)
					_wssv.Stop();

				throw;
			}
		}

		public void Send(string msg)
		{
			if (string.IsNullOrWhiteSpace(msg) || (_wssv == null || _wssv.WebSocketServices.Count <= 0))
				return;

			_wssv.WebSocketServices.Broadcast(msg);
		}

	}
}
