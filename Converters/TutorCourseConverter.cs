using MiTutorBEN.DTOs;
using MiTutorBEN.Models;

namespace MiTutorBEN.Converters
{
    public class TutorCourseConverter : IConverter<TutorCourse, TutorCourseDTO>
    {
        public TutorCourse FromDto(TutorCourseDTO dto)
        {
            return new TutorCourse
            {
                CourseId = dto.CourseId,
                TutorId = dto.TutorId
            };
        }

        public TutorCourseDTO FromEntity(TutorCourse entity)
        {
            return new TutorCourseDTO
            {
                CourseId = entity.CourseId,
                TutorId = entity.TutorId
            };
        }
    }
}