using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiTutorBEN.Models
{
    public class University
    {
        [Key]
        public int UniversityId { get; set; }

        [DataType(DataType.Text)]
        public string Name { get; set; }

        public virtual List<Person> Persons { get; set; } = new List<Person>();

        public virtual List<Course> Courses { get; set; } = new List<Course>();


        public University() { }
    }
}