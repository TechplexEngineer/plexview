using System;
using System.Collections.Generic;
using Nancy;

namespace PlexView
{
	public class GridMod : NancyModule
	{
		public GridMod () : base("/api")
		{
			Get["/grid"] = parameters => 
			{
				//return a list of grids
				return "";
			};
		}
	}
}

