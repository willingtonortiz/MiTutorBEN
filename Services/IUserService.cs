using MiTutorBEN.DTOs;
using MiTutorBEN.Models;
using System.Threading.Tasks;

namespace MiTutorBEN.Services
{
	public interface IUserService : ICrudService<User>
	{
		Task<bool> UserNameValid(string username);
		Task<bool> EmailValid(string email);

		Task<Tutor> Subscription(MembershipDTO membershipDTO);
	}
}