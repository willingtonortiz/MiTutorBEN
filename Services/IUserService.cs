using MiTutorBEN.DTOs;
using MiTutorBEN.Models;
namespace MiTutorBEN.Services
{
	public interface IUserService : ICrudService<User>
	{
		bool UserNameValid(string username);
		bool EmailValid(string email);
		void DeleteAll();
	}
}