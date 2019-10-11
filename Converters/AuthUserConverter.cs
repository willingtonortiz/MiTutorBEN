using MiTutorBEN.DTOs;
using MiTutorBEN.Entities;

namespace MiTutorBEN.Converters
{
	public class AuthUserConverter : IConverter<User, UserAuthDTO>
	{
		public User FromDto(UserAuthDTO dto)
		{
			User user = new User();
            user.UserId = dto.UserId;
            user.Username = dto.Username;

            return user;
		}

		public UserAuthDTO FromEntity(User entity)
		{
			UserAuthDTO userAuth = new UserAuthDTO();
            userAuth.UserId = entity.UserId;
            userAuth.Username = entity.Username;
            
            return userAuth;
		}
	}
}