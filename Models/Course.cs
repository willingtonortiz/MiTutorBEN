using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiTutorBEN.Models
{

	public class Course
	{
		[Key]
		public int CourseId { get; set; }

		[DataType(DataType.Text)]
		public string Name { get; set; }


		public List<Topic> Topics { get; set; } = new List<Topic>();
		public List<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
		public List<TutorCourse> TutorCourses { get; set; } = new List<TutorCourse>();


		public Course() { }

		public override string ToString()
		{
			return $"Course {{ Course: {CourseId}, Name: {Name} }}";
		}
	}
}