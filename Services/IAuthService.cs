using MiTutorBEN.DTOs;
using System.Threading.Tasks;

using MiTutorBEN.Models;
namespace MiTutorBEN.Services
{
	public interface IAuthService
	{
		AuthenticatedUser Login(string username, string password);
		// User RegisterUser(User user);
		Task<User> Register(Person person, Student student,User user);

	}
}