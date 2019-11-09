using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DefaultNamespace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiTutorBEN.DTOs;
using MiTutorBEN.Services;
using MiTutorBEN.Models;
using MiTutorBEN.ServicesImpl;
using MiTutorBEN.Data;
using Microsoft.EntityFrameworkCore;
using MiTutorBEN.Converters;
using MiTutorBEN.DTOs.Input;
using System.Net;
using Swashbuckle.AspNetCore.Annotations;

namespace MiTutorBEN.Controllers
{
	[AllowAnonymous]
	[ApiController]
	[Route("api/[controller]")]
	public class TutorsController : ControllerBase
	{
		#region Attributes

		private readonly ILogger<TutorsController> _logger;
		private readonly IUserService _userService;
		private readonly ITutorService _tutorService;
		private readonly ITutoringOfferService _tutoringOfferService;
		private readonly ICourseService _courseService;
		private readonly TutoringOfferConverter _tutoringOfferConverter;
		private readonly UniversityConverter _universityConverter;
		private readonly TutorConverter _tutorConverter;
		private readonly TutorCourseConverter _tutorCourseConverter;
		private readonly CourseConverter _courseConverter;

		#endregion


		#region Constructor

		public TutorsController(
			ILogger<TutorsController> logger,
			IUserService userService,
			ITutorService tutorService,
			ITutoringOfferService tutoringOfferService,
			ICourseService courseService,
			TutoringOfferConverter tutoringOfferConverter,
			TutorConverter tutorConverter,
			UniversityConverter universityConverter,
			TutorCourseConverter tutorCourseConverter,
			CourseConverter courseConverter
		)
		{
			_logger = logger;
			_userService = userService;
			_tutorService = tutorService;
			_tutoringOfferService = tutoringOfferService;
			_courseService = courseService;
			_tutoringOfferConverter = tutoringOfferConverter;
			_tutorConverter = tutorConverter;
			_universityConverter = universityConverter;
			_tutorCourseConverter = tutorCourseConverter;
			_courseConverter = courseConverter;
		}

		#endregion


		/// <summary>
		/// Find tutor by id
		/// </summary>
		/// <param name="id">The id of tutor</param>  
		[HttpGet("{id}")]
		public async Task<ActionResult<Tutor>> GetTutor(int id)
		{
			var tutor = await _tutorService.FindById(id);

			if (tutor == null)
			{
				return NotFound();
			}

			return tutor;
		}


		[HttpGet("{id}/university")]
		public async Task<ActionResult<UniversityDTO>> GetTutorUniversity(long id)
		{
			var university = await _tutorService.FindUniversity(id);
			if (university == null)
			{
				return NotFound();
			}

			return _universityConverter.FromEntity(university);
		}


		#region FindAll

		[HttpGet]
		public async Task<ActionResult<IEnumerable<TutorDTO>>> FindAll()
		{
			IEnumerable<Tutor> tutors = await _tutorService.FindAll();

			_logger.LogError(tutors.Count().ToString());

			IEnumerable<TutorDTO> tutorDtos = tutors.Select(x => _tutorConverter.FromEntity(x));

			return Ok(tutorDtos);
		}

		#endregion


		#region FindTutoringOffersByTutorId

		[HttpGet("{tutorId}/tutoringoffers")]
		public async Task<ActionResult<IEnumerable<TutoringOfferInfo>>> FindTutoringOffersByTutorId(
			[FromRoute] int tutorId
		)
		{
			Tutor foundTutor = await _tutorService.FindById(tutorId);
			if (foundTutor == null)
			{
				return NotFound();
			}

			IEnumerable<TutoringOffer> tutoringOffers = await _tutoringOfferService
				.FindAllByTutorIdAsync(tutorId);

			IEnumerable<TutoringOfferInfo> tutoringOffersInfo = tutoringOffers
				.Select(x => _tutoringOfferConverter.TutoringOfferToTutoringOfferInfo(x));

			return Ok(tutoringOffersInfo);
		}

		#endregion


		#region AddCourseToTutorByTutorIdAndCourseId

		/// <summary>
		/// Attach a course to a tutor
		/// </summary>
		/// <remarks>
		/// Adds a new course to the tutors list courses
		/// </remarks>
		/// <param name="tutorId">Tutor id</param>
		/// <param name="courseInput">Course input data</param>
		/// <response code="200">Course added successfully.</response>
		/// <response code="400">Course already added</response>
		/// <response code="404">Course or tutor not found</response>
		/// <response code="404">Course not found</response>
		/// <response code="404">Tutor not found</response>
		/// <response code="500">Internal application error</response>
		[SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(TutorCourseDTO))]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(string))]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
		[HttpPost("{tutorId}/courses")]
		public async Task<ActionResult<TutorCourseDTO>> AddCourseToTutorByTutorIdAndCourseId(
			[FromRoute] int tutorId,
			[FromBody] CourseIdInput courseInput
		)
		{
			Course course = await _courseService.FindById(courseInput.CourseId);
			if (course == null)
			{
				return NotFound("COURSE_NOT_FOUND");
			}

			Tutor tutor = await _tutorService.FindById(tutorId);
			if (tutor == null)
			{
				return NotFound("TUTOR_NOT_FOUND");
			}

			IEnumerable<Course> courses = await _courseService.FindAllByTutorIdAsync(tutor.TutorId);
			foreach (Course item in courses)
			{
				if (item.CourseId == course.CourseId)
				{
					return BadRequest("COURSE_ALREADY_ADDED");
				}
			}

			var tutorCourse = await _tutorService.AddCourseAsync(tutorId, course);

			return Ok(_tutorCourseConverter.FromEntity(tutorCourse));
		}

		#endregion

		#region FindAllCoursesByTutorId

		[HttpGet("{tutorId}/courses")]
		public async Task<ActionResult<IEnumerable<CourseDTO>>> FindAllCoursesByTutorId(
			[FromRoute] int tutorId
		)
		{
			Tutor foundTutor = await _tutorService.FindById(tutorId);
			if (foundTutor == null)
			{
				return NotFound("TUTOR_NOT_FOUND");
			}

			IEnumerable<Course> courses = await _courseService.FindAllByTutorIdAsync(tutorId);

			IEnumerable<CourseDTO> courseDtos = courses.Select(x => _courseConverter.FromEntity(x));

			return Ok(courseDtos);
		}

		#endregion
	}
}