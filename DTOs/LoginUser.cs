using System.ComponentModel.DataAnnotations;

namespace MiTutorBEN.DTOs
{
    public class LoginUser
    {
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
    }
}