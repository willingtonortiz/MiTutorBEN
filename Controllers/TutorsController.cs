using System;
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
	[ApiController]
	[AllowAnonymous]
	[Route("api/[controller]")]
	public class TutorsController : ControllerBase
	{
		private readonly ILogger<TutorsController> _logger;
		private readonly TutorConverter _tutorConverter;
		private readonly ITutorService _tutorService;

		public TutorsController(
			ILogger<TutorsController> logger,
			TutorConverter tutorConverter,
			ITutorService tutorService
			)
		{
			_logger = logger;
			_tutorConverter = tutorConverter;
			_tutorService = tutorService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<TutorDTO>>> FindAll()
		{
			IEnumerable<Tutor> tutors = await _tutorService.FindAll();

			return tutors.Select(x => _tutorConverter.FromEntity(x)).ToList();
		}
	}
}