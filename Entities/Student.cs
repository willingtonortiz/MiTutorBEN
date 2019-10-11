using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiTutorBEN.Entities
{
	public class Student
	{
		[Key]
		[ForeignKey("Person")]
		public int StudentId { get; set; }
		public int Points { get; set; }


		public Person Person { get; set; }

		public List<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();


		public List<StudentTutoringSession> StudentTutoringSessions { get; set; } = new List<StudentTutoringSession>();

		public Student() { }


		public override string ToString()
		{
			return $"Student {{ Id: {StudentId}, Points: {Points} }}";
		}
	}
}