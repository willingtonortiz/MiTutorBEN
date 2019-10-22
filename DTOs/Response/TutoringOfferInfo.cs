using System;

namespace MiTutorBEN.DTOs.Response
{
    public class TutoringOfferInfo
    {
        public int TutoringOfferId { get; set; }
        public string CourseName { get; set; }
        public string TutorName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}