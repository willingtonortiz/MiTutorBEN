using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiTutorBEN.Entities
{
    public class StudentTutoringSession
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int TutoringSessionId { get; set; }
        public TutoringSession TutoringSession { get; set; }

    }
}