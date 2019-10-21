using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MiTutorBEN.Controllers
{
	[AllowAnonymous]
	[ApiController]
	[Route("api/universities")]
	public class UniversitiesPersonsController : ControllerBase
	{
		private ILogger<UniversitiesPersonsController> _logger;

		public UniversitiesPersonsController(ILogger<UniversitiesPersonsController> logger)
		{
			_logger = logger;
		}

		[HttpGet("{universityId}/persons/{personId}")]
		public ActionResult<string> GetData([FromRoute]int universityId, [FromRoute] int personId)
		{
			return $"UniversitiesPersonsController: {universityId} - {personId}";
		}
	}
}