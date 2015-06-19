using System;
using OpenMetaverse;

namespace PlexView
{
	public class PlexView
	{
		public PlexView (string[] args)
		{
			GridClient client = new GridClient();
			client.Settings.LOGIN_SERVER = "http://login.osgrid.org/";
			if (client.Network.Login("", "", "", "FirstBot", "1.0"))
            {
                // Yay we made it! let's print out the message of the day
                Console.WriteLine("You have successfully logged into Second Life!\n The Message of the day is {0}\nPress any Key to Logout", 
                    client.Network.LoginMessage);
                
                Console.ReadLine(); // Wait for user to press a key before we continue
 
                client.Network.Logout(); // Lets logout since we're done here
            }
            else
            {
                // tell the user why the login failed
                Console.WriteLine("We were unable to login to Second Life, The Login Server said: {0}",
                    client.Network.LoginMessage);
            }
            Console.WriteLine("Press Any Key to Exit");
            Console.ReadLine(); // Wait for user to press a key before we exit
		}
	}
}

