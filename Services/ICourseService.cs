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
	}
}