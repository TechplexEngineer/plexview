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
			Post["/login"] = parameters => 
			{
				//process a login request
				Creds creds = this.Bind<Creds>();

				User client = ClientMgr.Instance.NewUser(creds);

				if (ClientMgr.Instance.Login(client))
	            {
					Hashtable msg = new Hashtable();
					msg.Add("status", "success");
					msg.Add("motd", client.Network.LoginMessage);
					msg.Add("sessionID", client.sessionID);
					return msg;
	            }
	            else
	            {
					Hashtable msg = new Hashtable();
					msg.Add("status", "fail");
					msg.Add("motd", client.Network.LoginMessage);
					msg.Add("sessionID", client.sessionID);
					return msg;
	            }
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

