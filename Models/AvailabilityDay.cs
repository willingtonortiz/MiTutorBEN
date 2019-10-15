using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiTutorBEN.Models
{
	public class AvailabilityDay
	{
		[Key]
		public int AvailabilityDayId { get; set; }

		[DataType(DataType.Text)]
		public string Day { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime StartTime { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime EndTime { get; set; }


		public int TutorId {get; set;}
		public Tutor Tutor { get; set; }


		public AvailabilityDay() { }
	}
}