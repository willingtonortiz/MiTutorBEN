using System;
using System.Collections.Generic;

namespace MiTutorBEN.DTOs.Requests
{
    public class TutoringOfferRequest
    {
        public int Capacity { get; set; }
        public String Description { get; set; }
        public int UniversityId { get; set; }
        public int CourseId { get; set; }
		public int TutorId {get; set; }
        public List<TutoringSessionRequest> TutoringSessionRequests { get; set; } = new List<TutoringSessionRequest>();
        
    }
}