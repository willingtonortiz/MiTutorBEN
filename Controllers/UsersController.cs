using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MiTutorBEN.Controllers
{
	[ApiController]
	[AllowAnonymous]
	[Route("api/[controller]")]
	public class UsersController
	{
		private readonly ILogger<UsersController> _logger;

		public UsersController(ILogger<UsersController> logger)
		{
			_logger = logger;
		}
	}
}