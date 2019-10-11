using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiTutorBEN.Entities
{
	public class Tutor
	{
		[Key]
		[ForeignKey("Person")]
		public int TutorId { get; set; }

		public int Points { get; set; }

		[DataType(DataType.Text)]
		public string Description { get; set; }


		public List<TutoringSession> TutoringSessions { get; set; } = new List<TutoringSession>();

		public List<TutoringOffer> TutoringOffers { get; set; } = new List<TutoringOffer>();

		public List<TutorCourse> TutorCourses { get; set; } = new List<TutorCourse>();

		public Availability Availability { get; set; }

		public Person Person { get; set; }


		public Tutor() { }


		public override string ToString()
		{
			return $"Tutor {{ Id: {TutorId}, Points: {Points}, Description: {Description} }}";
		}
	}
}