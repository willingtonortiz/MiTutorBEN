using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiTutorBEN.Entities
{
	public class TopicTutoringSession
	{
		public int TopicId { get; set; }
		public Topic Topic { get; set; }

		public int TutoringSessionId { get; set; }
		public TutoringSession TutoringSession { get; set; }

		public TopicTutoringSession() { }
	}
}