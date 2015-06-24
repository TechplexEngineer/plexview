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
//		[Obsolete]
//		public User NewUser(Creds creds)
//		{
//			User client = new User(creds);
//			client.Settings.LOGIN_SERVER = creds.gridURL;
//			//@todo if creds.gridName not in Grids list add it
//			users.Add(client.sessionID, client);
//			return client;
//		}

		//create a new user and begin the login process
		public Guid NewUserLogin(Creds creds)
		{
			//create the user object
			User client = new User(creds);

			//add the user to the list of users
			users.Add(client.sessionID, client);
			//initiate login
			client.Login();
			return client.sessionID;
			
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

