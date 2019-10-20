using System.Threading.Tasks;
using MiTutorBEN.Models;

namespace MiTutorBEN.Services
{
	public interface IUniversityService : ICrudService<University>
	{
		Task<University> FindByName(string name);
	}
}