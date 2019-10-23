using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiTutorBEN.Models
{
	public class TopicTutoringSession
	{
		public int TopicId { get; set; }
		public virtual Topic Topic { get; set; }

		public int TutoringSessionId { get; set; }
		public virtual TutoringSession TutoringSession { get; set; }

		public TopicTutoringSession() { }
	}
}