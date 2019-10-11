using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiTutorBEN.Entities
{
	public class Availability
	{
		[Key]
		public int AvailabilityId { get; set; }


		public int TutorId { get; set; }
		public Tutor Tutor { get; set; }


		public List<AvailabilityDay> AvailabilityDays { get; set; } = new List<AvailabilityDay>();
	}
}