using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MiTutorBEN.Controllers
{
	[AllowAnonymous]
	[ApiController]
	[Route("api/[controller]")]
	public class AvailabilityDaysController : ControllerBase
	{
		private ILogger<AvailabilityDaysController> _logger;

		public AvailabilityDaysController(
			ILogger<AvailabilityDaysController> logger
			)
		{
			_logger = logger;
		}


	}
}