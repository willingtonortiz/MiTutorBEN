using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MiTutorBEN.Converters;
using MiTutorBEN.DTOs;
using MiTutorBEN.DTOs.Response;
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


		[HttpGet("{universityId}/persons")]
		public ActionResult<string> GetData([FromRoute]int universityId)
		{
			return $"UniversitiesController: {universityId}";
		}


		[HttpGet]
		public async Task<ActionResult<IEnumerable<UniversityDTO>>> FindAll()
		{
			IEnumerable<University> universities = await _universityService.FindAll();

			return universities.Select(x => _universityConverter.FromEntity(x)).ToList();
		}


		[HttpGet("{universityId}/courses/{courseId}/tutoringoffers")]
		public async Task<ActionResult<List<TutoringOfferInfo>>> FindTutoringOffersByUniversityIdAndCourseId(int universityId, int courseId)
		{

			University foundUniversity = await _universityService.FindById(universityId);
			if (foundUniversity == null)
			{
				return NotFound(new { message = "University not found" });
			}

			Course foundCourse = await _courseService.FindById(courseId);
			if (foundCourse == null)
			{
				return NotFound(new { message = "Course not found" });
			}

			IEnumerable<TutoringOffer> tutoringOffers = await _tutoringOfferService.FindByUniversityIdAndCourseId(universityId, courseId);
			// if (tutoringOffers == null)
			// {
			// 	return NotFound(new { message = "Tutoring Offers not fount" });
			// }

			List<TutoringOfferInfo> result = tutoringOffers.Select(x => new TutoringOfferInfo
			{
				TutoringOfferId = x.TutoringOfferId,
				CourseName = x.Course.Name,
				StartTime = x.StartTime,
				EndTime = x.EndTime,
				TutorName = x.Tutor.Person.FullName
			}).ToList<TutoringOfferInfo>();

			return Ok(result);
		}


		[HttpGet("{universityId}/courses")]
		public async Task<ActionResult<CourseDTO>> FindCourseByUniversityIdAndCourseName(int universityId, string courseName = "")
		{
			// _logger.LogError($"{universityId} => {courseName}");

			Course course = await _courseService.FindByUniversityIdAndCourseName(universityId, courseName);

			// _logger.LogWarning("Curso encontrado");
			if (course == null)
			{
				return NotFound(new { message = "Course not found" });
			}

			CourseDTO courseDTO = _courseConverter.FromEntity(course);

			return Ok(courseDTO);
		}


		[HttpGet("{universityId}")]
		public async Task<ActionResult<UniversityDTO>> FindById(int universityId)
		{
			University university = await _universityService.FindById(universityId);

			if (university == null)
			{
				return NotFound(new { message = "No se encontró la universidad" });
			}

			return _universityConverter.FromEntity(university);
		}


		[HttpPost]
		public async Task<ActionResult<UniversityDTO>> Create([FromBody] UniversityDTO universityDTO)
		{
			University university = _universityConverter.FromDto(universityDTO);

			University created = await _universityService.Create(university);

			if (created == null)
			{
				return Created("", new { message = "Ya existe una universidad con ese nombre" });
			}
			return Created($"", _universityConverter.FromEntity(created));
		}


		[HttpDelete("{universityId}")]
		public async Task<ActionResult<UniversityDTO>> Delete(int universityId)
		{
			University deleted = await _universityService.DeleteById(universityId);

			if (deleted == null)
			{
				return NotFound(new { message = "No se encontró la universidad" });
			}

			return _universityConverter.FromEntity(deleted);
		}
	}
}