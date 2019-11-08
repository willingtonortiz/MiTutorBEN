using System;
using System.Collections.Generic;

namespace MiTutorBEN.DTOs.Responses
{
	public class TutoringOfferResponse
	{
		public int TutoringOfferId { get; set; }
		public string Course { get; set; }
		public string Tutor { get; set; }
		public int TutorId {get; set; }
		public string Description { get; set; }
		public string University { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public int Capacity { get; set; }
		public List<String> Topics { get; set; } = new List<string>();
		public List<TutoringSessionResponse> Sessions { get; set; } = new List<TutoringSessionResponse>();
	}
}