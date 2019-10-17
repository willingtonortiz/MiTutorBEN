using MiTutorBEN.DTOs;
using MiTutorBEN.Models;

namespace MiTutorBEN.Converters
{
	public class CourseConverter : IConverter<Course, CourseDTO>
	{
		public Course FromDto(CourseDTO dto)
		{
			Course course = new Course
			{
				CourseId = dto.CourseId,
				Name = dto.Name
			};

			return course;
		}

		public CourseDTO FromEntity(Course entity)
		{
			CourseDTO courseDTO = new CourseDTO
			{
				CourseId = entity.CourseId,
				Name = entity.Name
			};

			return courseDTO;
		}
	}
}