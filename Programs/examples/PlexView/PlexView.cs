using System;
using System.Collections.Generic;
using OpenMetaverse;
using Nancy;
using Nancy.Hosting.Self;

namespace PlexView
{
	public class PlexView
	{
		public PlexView (string[] args)
		{
			//bootstrap the app here, initialize services, start http, get ready!
			Console.WriteLine ("Server Starting");

			var nancyHost = new NancyHost(Constants.HTTP_URL);

			nancyHost.Start();
			Console.WriteLine("HTTP server listening "+Constants.HTTP_URL);
			Console.ReadKey();

		}
	}
}

