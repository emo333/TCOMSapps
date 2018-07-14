using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;

namespace TCOMSapps.Data
{
	// Add profile data for application users by adding properties to the ApplicationUser class
	public class ApplicationUser : IdentityUser<Guid>
	{
		[DisplayName("First Name")]
		public string FirstName { get; set; }

		[DisplayName("Last Name")]
		public string LastName { get; set; }

		public int CountyId { get; set; }

		public Guid? SupervisorId { get; set; }

		[DisplayName("Normal Shift")]
		public int DefaultShiftId { get; set; }

		[DisplayName("Normal Station")]
		public int DefaultStationId { get; set; }

	}
}
