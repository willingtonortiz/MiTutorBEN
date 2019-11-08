using System.Collections.Generic;
using System.Threading.Tasks;
using MiTutorBEN.Models;

namespace MiTutorBEN.Services
{
	public interface ICourseService : ICrudService<Course>
	{
		Task<bool> ExistsById(int courseId);
		Task<Course> FindByUniversityIdAndCourseName(int universityId, string courseName);
		Task<IEnumerable<Course>> FindAllByUniversityId(int universityId);
		Task<List<Topic>> FindTopics(int courseId);

		Task<IEnumerable<Course>> FindAllByTutorIdAsync(int tutorId);
	}
}