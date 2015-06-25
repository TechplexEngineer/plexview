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
		public Guid BeginNewUserLogin(Creds creds)
		{
			//create the user object
			User client = new User(creds);

			//add the user to the list of users
			users.Add(client.sessionID, client);
			//initiate login
			client.BeginLogin();
			return client.sessionID;
			
		}

		public bool BeginLogout(Guid sessionID)
		{
			if (users.ContainsKey(sessionID)) {
				//begin the logot process
				users[sessionID].BeginLogout();

				//remove the user from the active list
				users.Remove(sessionID);
				return true;
			} else {
				return false;
			}
		}
	}
}

