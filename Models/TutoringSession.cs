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
        public DateTime StartTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }

		public int StudentCount { get; set; }

		[DataType(DataType.Text)]
		public string Description { get; set; }

		public double Price {get; set;}


		public int TutoringOfferId {get; set;}
		public virtual TutoringOffer TutoringOffer {get; set;}	

		


		public virtual List<Qualification> Qualifications { get; set; } = new List<Qualification>();
		public virtual List<StudentTutoringSession> StudentTutoringSessions { get; set; } = new List<StudentTutoringSession>();
		public virtual List<TopicTutoringSession> TopicTutoringSessions { get; set; } = new List<TopicTutoringSession>();


		public TutoringSession() { }


		public override string ToString()
		{
			return $"TutoringSession {{ Id: {TutoringSessionId}, Place: {Place}, StartTime: {StartTime},  EndTime: {EndTime}, StudentCount: {StudentCount}, Description: {Description} }}";
		}
	}
}