namespace Meteo
{
	class Meteoinfo
	{
		public class Request
		{
			public string type { get; set; }
			public string query { get; set; }
		}
	
		public class location
		{
			public string name { get; set; }
			public string country { get; set; }
			public string region { get; set; }
			public string timezone { get; set; }
			public string  localtime { get; set; }
		}
		public class current
		{
			public int temperature { get; set; }
			public double wind_speed { get; set; }
			public int wind_degree { get; set; }
			public string wind_dir { get; set; }
			public double pressure { get; set; }
			public double humidity { get; set; }
			public double cloudcover { get; set; }
			public double precip { get; set; }
		}
		public class root
        {
			public Request request { get; set; }
			public current current { get; set; }
			public location location { get; set; }
        }
	}
}
