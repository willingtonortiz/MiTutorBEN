using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiTutorBEN.Models
{
    public class StudentTutoringSession
    {
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        public int TutoringSessionId { get; set; }
        public virtual TutoringSession TutoringSession { get; set; }

    }
}