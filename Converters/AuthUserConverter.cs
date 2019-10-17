using MiTutorBEN.DTOs;
using MiTutorBEN.Models;

namespace MiTutorBEN.Converters
{
	public class AuthUserConverter : IConverter<User, UserAuthDTO>
	{
		public User FromDto(UserAuthDTO dto)
		{
			User user = new User
			{
				UserId = dto.UserId,
				Username = dto.Username
			};

			return user;
		}

		public UserAuthDTO FromEntity(User entity)
		{
			UserAuthDTO userAuth = new UserAuthDTO
			{
				UserId = entity.UserId,
				Username = entity.Username
			};

			return userAuth;
		}
	}
}