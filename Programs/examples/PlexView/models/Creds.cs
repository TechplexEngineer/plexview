using System;

namespace PlexView
{
	/// <summary>
	/// Login Creds model.
	/// </summary>
	public class Creds
	{
		public string first { get; set; }

		public string last { get; set; }

		public string pass { get; set; } //@todo encrypt this in transit

		public string gridName { get; set; }

		public string gridURL { get; set; }
	}
}

