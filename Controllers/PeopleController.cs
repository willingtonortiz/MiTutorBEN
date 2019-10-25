using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiTutorBEN.Converters;
using MiTutorBEN.DTOs;
using MiTutorBEN.Models;
using MiTutorBEN.Services;

namespace MiTutorBEN.Controllers
{
	[Consumes("application/json")]
	[ApiController]
	[AllowAnonymous]
	[Route("api/[controller]")]
	public class PeopleController : ControllerBase
	{
		private readonly ILogger<PeopleController> _logger;
		private readonly IPersonService _personService;
		private readonly PersonConverter _personConverter;

		public PeopleController(
			ILogger<PeopleController> logger,
			IPersonService personService,
			PersonConverter personConverter
			)
		{
			_logger = logger;
			_personService = personService;
			_personConverter = personConverter;
		}


		[HttpGet]
		public async Task<ActionResult<IEnumerable<PersonDTO>>> FindAll()
		{
			IEnumerable<Person> persons = await _personService.FindAll();

			IEnumerable<PersonDTO> personsDTO = persons
				.Select(x => _personConverter.FromEntity(x));

			return Ok(personsDTO);
		}
	}
}