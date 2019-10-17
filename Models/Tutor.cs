using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiTutorBEN.Models
{
    public class Tutor
    {
        // Entity attributes
        [Key]
        [ForeignKey("Person")]
        public int TutorId { get; set; }

        public int QualificationCount { get; set; }

        public double Points { get; set; }

        [DataType(DataType.Text)]
        public string Description { get; set; }


        // Navigation attributes
        public List<TutoringSession> TutoringSessions { get; set; } = new List<TutoringSession>();

        public List<TutoringOffer> TutoringOffers { get; set; } = new List<TutoringOffer>();

        public List<TutorCourse> TutorCourses { get; set; } = new List<TutorCourse>();

        public List<AvailabilityDay> AvailabilityDays { get; set; }

        public Person Person { get; set; }


        // Methods
        public Tutor() { }

        public override string ToString()
        {
            return $"Tutor {{ Id: {TutorId}, Points: {Points}, Description: {Description} }}";
        }
    }
}