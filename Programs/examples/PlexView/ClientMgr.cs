using System;
using System.Collections.Generic;

namespace PlexView
{
	public sealed class ClientMgr
	{
		private static readonly ClientMgr instance = new ClientMgr();
		private Dictionary<Guid, User> users;

		private ClientMgr()
		{
			users = new Dictionary<Guid, User>();
		}

		public static ClientMgr Instance {
			get {
				return instance; 
			}
		}

		public User NewUser(Creds creds)
		{
			User client = new User(creds);
			client.Settings.LOGIN_SERVER = creds.gridURL;
			//@todo if creds.gridName not in Grids list add it
			users.Add(client.sessionID, client);
			return client;
		}

		public bool Login(User client)
		{
			return client.Network.Login(client.creds.first, client.creds.last, client.creds.pass, Constants.CHANNEL, Constants.VERSION);
		}

		public bool Logout(Guid sessionID)
		{
			if (users.ContainsKey(sessionID)) {
				users[sessionID].Network.Logout(); //@note this is a blocking call
				return true;
			} else {
				return false;
			}
		}
	}
}

