using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiTutorBEN.Models
{
	public class Topic
	{
		// Entity attributes
		[Key]
		public int TopicId { get; set; }

		[DataType(DataType.Text)]
		public string Name { get; set; }


		// Navigation attributes
		public int CourseId { get; set; }
		public Course Course { get; set; }

		public List<TopicTutoringOffer> TopicTutoringOffers { get; set; } = new List<TopicTutoringOffer>();

		public List<TopicTutoringSession> TopicTutoringSessions { get; set; } = new List<TopicTutoringSession>();


		// Methods
		public Topic() { }

		public override string ToString()
		{
			return $"Topic {{ TopicId: {TopicId}, Name: {Name} }}";
		}
	}
}