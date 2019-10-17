using System.Threading.Tasks;
using MiTutorBEN.Models;

namespace MiTutorBEN.Services
{
	public interface ICourseService : ICrudService<Course>
	{
		Task<Course> findCourse(int universityId, string courseName);
		void DeleteAll();
	}
}