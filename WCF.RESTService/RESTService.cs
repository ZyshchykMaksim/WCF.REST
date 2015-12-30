using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Timers;
using WCF.RESTService.Interface;
using WCF.RESTService.Notification;

namespace WCF.RESTService
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class RESTService : IRESTService
	{
		static Timer _timer;
		private static List<User> _users = new List<User>();
		public INotification Notification
		{
			get { return NotificationManager.Notification; }
		}

		public RESTService()
		{
			_timer = new Timer(5000) { Enabled = true };
			_timer.Elapsed += (sender, args) =>
			{
				if (Notification != null)
				{
					Notification.Send(String.Format("DateTime => {0}", DateTime.Now));
				}
			};
		}

		public List<User> GetAllUsers()
		{
			return _users;
		}

		public void SetUser(User user)
		{
			_users.Add(user);
		}

		public User GetUser()
		{
			return new User() { Name = "Mike", Year = 123 };
		}
	}
}

