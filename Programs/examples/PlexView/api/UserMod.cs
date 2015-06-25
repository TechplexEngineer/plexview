using System;
using System.Collections;
using Nancy;
using Nancy.ModelBinding;
using OpenMetaverse;

namespace PlexView
{
	public class UserMod : NancyModule
	{
		public UserMod () : base("/api")
		{
			//When the user requests to login
			Post["/login"] = parameters => 
			{
				//parese their credentials
				Creds creds = this.Bind<Creds>();

				//initiate login process
				Guid sessionID = ClientMgr.Instance.BeginNewUserLogin(creds);

				//tell them that we have started login
				Hashtable msg = new Hashtable();
				msg.Add("status", "login begun");
				msg.Add("sessionID", sessionID);
				return msg;

			};

			//when the user requests to logout
			Post["/logout"] = parameters => 
			{
				//parse their logout credentials
				Logout logout = this.Bind<Logout>();
				//we don't want users logging eachother out, xss so we need some sort of session ID

				if (ClientMgr.Instance.BeginLogout(logout.sessionID))
				{
					//tell them that we have started logout
					Hashtable msg = new Hashtable();
					msg.Add("status", "logout begun");
					msg.Add("sessionID", logout.sessionID);
					return msg;
				}
				else
				{
					Console.WriteLine("Unable to begin logout");
					//tell them that we have FAILEDstarted login
					Hashtable msg = new Hashtable();
					msg.Add("status", "logout FAILED");
					msg.Add("sessionID", logout.sessionID);
					return msg;
				}
			};
		}
	}
}

