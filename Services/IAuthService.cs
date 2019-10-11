using MiTutorBEN.DTOs;

namespace MiTutorBEN.Services
{
	public interface IAuthService
	{
		UserAuthDTO Authenticate(string username, string password);

	}
}