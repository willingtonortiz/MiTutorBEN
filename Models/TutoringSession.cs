using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiTutorBEN.Models
{
	public class TutoringSession
	{
		[Key]
		public int TutoringSessionId { get; set; }

		[DataType(DataType.Text)]
		public string Place { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime Date { get; set; }

		public int Capacity { get; set; }

		[DataType(DataType.Text)]
		public string Description { get; set; }

		public Course Course { get; set; }

		public List<TopicTutoringOffer> TopicTutoringOffers { get; set; }

		public int TutorId { get; set; }
		public Tutor Tutor { get; set; }


		// sdasd
		public List<Qualification> Qualifications { get; set; } = new List<Qualification>();
		public List<StudentTutoringSession> StudentTutoringSessions { get; set; } = new List<StudentTutoringSession>();
		public List<TopicTutoringSession> TopicTutoringSessions { get; set; } = new List<TopicTutoringSession>();


		public TutoringSession() { }


		public override string ToString()
		{
			return $"TutorignSession {{ Id: {TutoringSessionId}, Place: {Place}, Date: {Date}, Capacity: {Capacity}, Description: {Description} }}";
		}
	}
}