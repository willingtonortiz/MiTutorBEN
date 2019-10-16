using System;

namespace MiTutorBEN.DTOs
{
    public class TutoringOfferDTO
    {
        public int TutoringOfferId { get; set; }
        public DateTime StartTime{ get; set; }
        public DateTime EndTime { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
        
    }
}