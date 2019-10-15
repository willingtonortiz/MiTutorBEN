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


        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }

        public int Capacity { get; set; }

        [DataType(DataType.Text)]
        public string Description { get; set; }


        public int UniversityId { get; set; }
        public University University { get; set; }


        public int CourseId { get; set; }
        public Course Course { get; set; }


        public int TutorId { get; set; }
        public Tutor Tutor { get; set; }


        public List<TopicTutoringOffer> TopicTutoringOffers { get; set; } = new List<TopicTutoringOffer>();


        public List<TutoringSession> TutoringSessions { get; set; } = new List<TutoringSession>();


        public TutoringOffer() { }

        public override string ToString()
        {
             return $"TutoringOffer {{ Id: {TutoringOfferId}, StartTime: {StartTime},  EndTime: {EndTime}, Capacity: {Capacity}, Description: {Description} }}";
        }
    }
}