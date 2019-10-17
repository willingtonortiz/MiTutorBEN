using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MiTutorBEN.Controllers
{
	[ApiController]
	[AllowAnonymous]
	[Route("api/[controller]")]
	public class TutorsController
	{
		private readonly ILogger<TutorsController> _logger;

		public TutorsController(ILogger<TutorsController> logger)
		{
			_logger = logger;
		}
	}
}