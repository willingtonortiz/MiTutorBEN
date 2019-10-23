using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiTutorBEN.Models
{

	public class Course
	{
		// Entity attributes
		[Key]
		public int CourseId { get; set; }

		[DataType(DataType.Text)]
		public string Name { get; set; }


		// Navigation attributes
		public int UniversityId { get; set; }
        public virtual University University { get; set; }

		public virtual List<Topic> Topics { get; set; } = new List<Topic>();

		public virtual List<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

		public virtual List<TutorCourse> TutorCourses { get; set; } = new List<TutorCourse>();


		// Methods
		public Course() { }

		public override string ToString()
		{
			return $"Course {{ Course: {CourseId}, Name: {Name} }}";
		}
	}
}