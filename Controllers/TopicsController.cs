using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MiTutorBEN.Controllers
{
    [ApiController]
	[AllowAnonymous]
	[Route("api/[controller]")]
    public class TopicsController
    {
        private readonly ILogger<TopicsController> _logger;

		public TopicsController(ILogger<TopicsController> logger)
		{
			_logger = logger;
		}
    }
}