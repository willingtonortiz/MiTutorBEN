using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiTutorBEN.DTOs;
using MiTutorBEN.Models;
using MiTutorBEN.Services;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;


namespace MiTutorBEN.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class UsersController : ControllerBase
	{
		private readonly ILogger<UsersController> _logger;

		private readonly IUserService _userService;

		public UsersController(
			ILogger<UsersController> logger,
			IUserService userService
		)
		{
			_logger = logger;
			_userService = userService;
		}


		[AllowAnonymous]
		[HttpGet]
		[Route("isUsernameExist")]
		public async Task<ActionResult<bool>> AuthenticateUsername(string username)
		{
			if (await _userService.UserNameValid(username))
			{
				// _logger.LogWarning("El usuario existe en la bd");
				return Ok(true);
			}
			else
			{
				// _logger.LogWarning("El usuario no existe");
				return Ok(false);
			}

		}

		[AllowAnonymous]
		[HttpGet]
		[Route("isEmailExist")]
		public async Task<ActionResult<bool>> AuthenticateEmail(string email)
		{
			if (await _userService.EmailValid(email))
			{
				return Ok(true);
			}
			else
			{
				return Ok(false);
			}

		}
	}
}