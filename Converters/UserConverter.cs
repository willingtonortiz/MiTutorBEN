using MiTutorBEN.DTOs;
using MiTutorBEN.Models;

namespace MiTutorBEN.Converters
{
	public class UserConverter : IConverter<User, UserDTO>
	{
		public User FromDto(UserDTO dto)
		{
			User user = new User
			{
				UserId = dto.UserId,
				Username = dto.Username,
				Email = dto.Email,
				Role = dto.Role
			};

			return user;
		}

		public UserDTO FromEntity(User entity)
		{
			UserDTO userDTO = new UserDTO
			{
				UserId = entity.UserId,
				Username = entity.Username,
				Email = entity.Email,
				Role = entity.Role
			};

            return userDTO;
		}
	}
}