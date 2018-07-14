using System.Collections.Generic;


namespace TCOMSapps.Features.OOSTitles.entities
{
	public class Location
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int CountyId { get; set; }
		public string Manager { get; set; }
		public bool Default { get; set; }
		public string Color { get; set; }
		public List<Transfer> Transfers { get; set; }

	}
}