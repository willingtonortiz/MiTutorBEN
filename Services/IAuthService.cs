using MiTutorBEN.DTOs;
using MiTutorBEN.Models;
namespace MiTutorBEN.Services
{
	public interface IAuthService
	{
		UserAuthDTO Authenticate(string username, string password);
		User RegisterUser(User user);
		void Register(Person person, Student student,User user);

	}
}