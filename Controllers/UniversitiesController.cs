using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
	public class UniversitiesController : ControllerBase
	{
		private readonly ILogger<UniversitiesController> _logger;
		private readonly IUniversityService _universityService;
		private readonly UniversityConverter _universityConverter;
		private readonly ITutoringOfferService _tutoringOfferService;
		private readonly TutoringOfferConverter _tutoringOfferConverter;
		private readonly PersonConverter _personConverter;
		private readonly ICourseService _courseService;
		private readonly CourseConverter _courseConverter;

		public UniversitiesController(
			ILogger<UniversitiesController> logger,
			IUniversityService universityService,
			UniversityConverter universityConverter,
			ITutoringOfferService tutoringOfferService,
			TutoringOfferConverter tutoringOfferConverter,
			PersonConverter personConverter,
			ICourseService courseService,
			CourseConverter courseConverter
			)
		{
			_logger = logger;
			_universityService = universityService;
			_universityConverter = universityConverter;
			_tutoringOfferService = tutoringOfferService;
			_tutoringOfferConverter = tutoringOfferConverter;
			_personConverter = personConverter;
			_courseService = courseService;
			_courseConverter = courseConverter;
		}

		[HttpGet]
		public ActionResult<IEnumerable<UniversityDTO>> FindAll()
		{
			IEnumerable<University> universities = _universityService.FindAll();

			return universities.Select(x => _universityConverter.FromEntity(x)).ToList();
		}


		[HttpGet("{universityId}/courses/{courseId}/tutoringoffers")]
		public async Task<ActionResult<List<object>>> FindTutoringOffers(int universityId, int courseId)
		{
			IEnumerable<TutoringOffer> tutoringOffers = await _tutoringOfferService.FindByUniversityAndCourse(universityId, courseId);

			if (tutoringOffers.Count() == 0)
			{
				return NotFound(new { message = "No se encontraron tutorías" });
			}

			List<object> result = new List<object>();

			foreach (TutoringOffer item in tutoringOffers)
			{
				result.Add(new
				{
					tutoringOffer = _tutoringOfferConverter.FromEntity(item),
					person = _personConverter.FromEntity(item.Tutor.Person),
					course = _courseConverter.FromEntity(item.Course)
				});
			}

			return Ok(result);
		}

		[HttpGet("{universityId}/courses/{courseName}")]
		public async Task<ActionResult<Course>> FindCourse(int universityId, string courseName)
		{
			Course course = await _courseService.findCourse(universityId, courseName);

			if (course == null)
			{
				return NotFound(new { message = "No se encontró el curso" });
			}

			return Ok(course);
		}

		[HttpGet("{universityId}")]
		public ActionResult<UniversityDTO> FindById(int universityId)
		{
			University university = _universityService.FindById(universityId);

			if (university == null)
			{
				return NotFound(new { message = "No se encontró la universidad" });
			}

			return _universityConverter.FromEntity(university);
		}

		[HttpPost]
		public ActionResult<UniversityDTO> Create([FromBody] UniversityDTO universityDTO)
		{
			University university = _universityConverter.FromDto(universityDTO);

			University created = _universityService.Create(university);

			if (created == null)
			{
				return Created("", new { message = "Ya existe una universidad con ese nombre" });
			}
			return Created($"", _universityConverter.FromEntity(created));

			// return _universityConverter.FromEntity(created);
		}

		[HttpDelete("{universityId}")]
		public ActionResult<UniversityDTO> Delete(int universityId)
		{
			University deleted = _universityService.DeleteById(universityId);

			if (deleted == null)
			{
				return NotFound(new { message = "No se encontró la universidad" });
			}

			return _universityConverter.FromEntity(deleted);
		}
	}
}