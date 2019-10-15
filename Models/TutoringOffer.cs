using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiTutorBEN.Models
{
	public class TutoringOffer
	{
		[Key]
		public int TutoringOfferId { get; set; }

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


		public TutoringOffer() { }
	}
}