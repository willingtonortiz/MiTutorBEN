using System.Threading.Tasks;
using MiTutorBEN.Models;

namespace MiTutorBEN.Services
{
	public interface ITutorCourseService
	{
		Task<TutorCourse> FindByTutorIdAndCourseIdAsync(int tutorId, int courseId);
		Task DeleteByTutorIdAndCourseIdAsync(TutorCourse tutorCourse);
	}
}