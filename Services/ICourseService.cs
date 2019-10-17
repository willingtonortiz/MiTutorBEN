using MiTutorBEN.Models;

namespace MiTutorBEN.Services
{
	public interface ICourseService : ICrudService<Course>
	{
		void DeleteAll();
	}
}