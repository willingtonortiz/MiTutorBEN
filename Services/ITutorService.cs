using System.Collections.Generic;
using System.Threading.Tasks;
using MiTutorBEN.DTOs;
using MiTutorBEN.Models;

namespace MiTutorBEN.Services
{
    public interface ITutorService : ICrudService<Tutor>
    {
        Task<IEnumerable<Tutor>> FindAllByUniversityIdAndCourseId(int universityId, int courseId);
        Task<University> FindUniversity(long id);
        Task<TutorCourse> AddCourseAsync(int tutorId, Course course);
    }
}