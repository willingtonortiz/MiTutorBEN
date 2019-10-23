using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiTutorBEN.Models
{
	public class User
	{
		// Entity attributes
		[Key]
		[ForeignKey("Person")]
		public int UserId { get; set; }

		[DataType(DataType.Text)]
		public string Username { get; set; }

		[DataType(DataType.Password)]
		public string Password { get; set; }

		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[DataType(DataType.Text)]
		public string Role { get; set; }


		// Navigation attributes
		public virtual Person Person { get; set; }


		// Methods
		public User() { }

		public override string ToString()
		{
			return $"User {{ UserId: {UserId}, Username: {Username}, Password: {Password}, Role: {Role} }}";
		}
	}
}