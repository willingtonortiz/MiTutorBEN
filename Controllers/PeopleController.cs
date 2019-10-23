using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiTutorBEN.Services;

namespace MiTutorBEN.Controllers
{
	[ApiController]
	[AllowAnonymous]
	[Route("api/[controller]")]
	public class PeopleController
	{
		private readonly ILogger<PeopleController> _logger;
		private readonly IPersonService _personService;

		public PeopleController(
			ILogger<PeopleController> logger,
			IPersonService personService
			)
		{
			_logger = logger;
			_personService = personService;
		}
	}
}