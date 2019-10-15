using MiTutorBEN.DTOs;
using MiTutorBEN.Models;
namespace MiTutorBEN.Services
{
	public interface IUserService
	{
        bool UserNameValid(string username);

	}
}