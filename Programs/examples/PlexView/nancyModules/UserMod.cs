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
			//When the users requests to login
			Post["/login"] = parameters => 
			{
				//parese their credentials
				Creds creds = this.Bind<Creds>();

				//initiate login process
				Guid sessionID = ClientMgr.Instance.NewUserLogin(creds);

				//tell them that we have started login
				Hashtable msg = new Hashtable();
				msg.Add("status", "login begin");
				msg.Add("sessionID", sessionID);
				return msg;

			};

			Post["/logout"] = parameters => 
			{
				Logout logout = this.Bind<Logout>();

				//figure out who they are based on a parameter
				//we don't want users logging eachother out, xss so we need some sort of session ID

				Hashtable msg = new Hashtable();
				if (ClientMgr.Instance.Logout(logout.sessionID)){
					msg.Add("status", "success");
				} else {
					msg.Add("status", "fail");
				}
				return msg;

			};
		}
	}
}

