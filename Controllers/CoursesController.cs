using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MiTutorBEN.Controllers
{
	[ApiController]
	[AllowAnonymous]
	[Route("api/[controller]")]
	public class CoursesController
	{
		private readonly ILogger<CoursesController> _logger;

		public CoursesController(ILogger<CoursesController> logger)
		{
			_logger = logger;
		}

	}
}