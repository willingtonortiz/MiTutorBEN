using System.ComponentModel.DataAnnotations;

namespace MiTutorBEN.Models
{
	public class User
	{
		[Key]
		public int UserId { get; set; }

		[DataType(DataType.Text)]
		public string Username { get; set; }

		[DataType(DataType.Password)]
		public string Password { get; set; }

		[DataType(DataType.Text)]
		public string Role { get; set; }


		public Person Person { get; set; }


		public User() { }

		public override string ToString()
		{
			return $"User {{ UserId: {UserId}, Username: {Username}, Password: {Password}, Role: {Role} }}";
		}
	}
}