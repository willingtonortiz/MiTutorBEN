using MiTutorBEN.Models;

namespace MiTutorBEN.Services
{
	public interface IPersonService : ICrudService<Person>
	{
		void DeleteAll();
	}
}