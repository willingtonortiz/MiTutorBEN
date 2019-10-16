using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MiTutorBEN.Controllers
{
	[ApiController]
	[AllowAnonymous]
	[Route("api/[controller]")]
	public class TutoringOffersController
	{
		private readonly ILogger<TutoringOffersController> _logger;

		public TutoringOffersController(ILogger<TutoringOffersController> logger)
		{
			_logger = logger;

		}
	}
}