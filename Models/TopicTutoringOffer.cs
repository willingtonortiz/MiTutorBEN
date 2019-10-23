using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiTutorBEN.Models
{
	public class TopicTutoringOffer
	{
		public int TopicId { get; set; }
		public virtual Topic Topic { get; set; }

		public int TutoringOfferId { get; set; }
		public virtual TutoringOffer TutoringOffer { get; set; }
		

		public TopicTutoringOffer() { }

	}
}