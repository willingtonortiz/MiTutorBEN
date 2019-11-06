using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DefaultNamespace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MiTutorBEN.Converters;
using MiTutorBEN.DTOs;
using MiTutorBEN.DTOs.Responses;
using MiTutorBEN.Models;
using MiTutorBEN.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace MiTutorBEN.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class UniversitiesController : ControllerBase
    {
        #region Attributes

        private readonly ILogger<UniversitiesController> _logger;
        private readonly IUniversityService _universityService;
        private readonly ITutoringOfferService _tutoringOfferService;
        private readonly ICourseService _courseService;
        private readonly ITutorService _tutorService;
        private readonly CourseConverter _courseConverter;
        private readonly PersonConverter _personConverter;
        private readonly TutoringOfferConverter _tutoringOfferConverter;
        private readonly UniversityConverter _universityConverter;
        private readonly TutorConverter _tutorConverter;

        #endregion


        #region Constructor

        public UniversitiesController(
            ILogger<UniversitiesController> logger,
            IUniversityService universityService,
            UniversityConverter universityConverter,
            ITutoringOfferService tutoringOfferService,
            TutoringOfferConverter tutoringOfferConverter,
            PersonConverter personConverter,
            ICourseService courseService,
            CourseConverter courseConverter,
            ITutorService tutorService,
            TutorConverter tutorConverter
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
            _tutorService = tutorService;
            _tutorConverter = tutorConverter;
        }

        #endregion


        #region FindAll	

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UniversityDTO>>> FindAll()
        {
            IEnumerable<University> universities = await _universityService.FindAll();

            return universities.Select(x => _universityConverter.FromEntity(x)).ToList();
        }

        #endregion


        #region FindById

        [HttpGet("{universityId}")]
        public async Task<ActionResult<UniversityDTO>> FindById(int universityId)
        {
            University university = await _universityService.FindById(universityId);

            if (university == null)
            {
                return NotFound(new {message = "No se encontró la universidad"});
            }

            return _universityConverter.FromEntity(university);
        }

        #endregion


        #region Create

        [HttpPost]
        public async Task<ActionResult<UniversityDTO>> Create([FromBody] UniversityDTO universityDTO)
        {
            University university = _universityConverter.FromDto(universityDTO);

            University created = await _universityService.Create(university);

            if (created == null)
            {
                return Created("", new {message = "Ya existe una universidad con ese nombre"});
            }

            return Created($"", _universityConverter.FromEntity(created));
        }

        #endregion


        #region Delete

        [HttpDelete("{universityId}")]
        public async Task<ActionResult<UniversityDTO>> Delete(int universityId)
        {
            University deleted = await _universityService.DeleteById(universityId);

            if (deleted == null)
            {
                return NotFound(new {message = "No se encontró la universidad"});
            }

            return _universityConverter.FromEntity(deleted);
        }

        #endregion


        #region FindTutoringOffersByUniversityIdAndCourseId

        /// <summary>
        /// Finds all tutoring offers by university id and course id
        /// </summary>
        /// <remarks>
        /// Finds all tutoring offers related to an university and course
        /// </remarks>
        /// <param name="universityId">The id of the university</param>
        /// <param name="courseId">The id of the course</param>
        /// <response code="200">Tutoring offers found.</response>
        /// <response code="404">University or course not found</response>
        /// <response code="500">Internal application error</response>
        [SwaggerResponse((int) HttpStatusCode.OK, Type = typeof(IEnumerable<TutoringOfferInfo>))]
        [SwaggerResponse((int) HttpStatusCode.NotFound, Type = typeof(string))]
        [SwaggerResponse((int) HttpStatusCode.InternalServerError, Type = typeof(string))]
        [HttpGet("{universityId}/courses/{courseId}/tutoringoffers")]
        public async Task<ActionResult<List<TutoringOfferInfo>>> FindTutoringOffersByUniversityIdAndCourseId(
            [FromRoute] int universityId,
            [FromRoute] int courseId
        )
        {
            University foundUniversity = await _universityService.FindById(universityId);
            if (foundUniversity == null)
            {
                return NotFound(new {message = "University not found"});
            }

            Course foundCourse = await _courseService.FindById(courseId);
            if (foundCourse == null)
            {
                return NotFound(new {message = "Course not found"});
            }

            IEnumerable<TutoringOffer> tutoringOffers =
                await _tutoringOfferService.FindByUniversityIdAndCourseId(universityId, courseId);
            List<TutoringOfferInfo> result = tutoringOffers
                .Select(x => _tutoringOfferConverter.TutoringOfferToTutoringOfferInfo(x))
                .ToList<TutoringOfferInfo>();

            return Ok(result);
        }

        #endregion


        #region FindCoursesByUniversityId

        /// <summary>
        /// Finds all courses in an university
        /// </summary>
        /// <remarks>
        /// Finds all courses in an university specified by an id 
        /// </remarks>
        /// <param name="universityId">The id of the university</param>
        /// <param name="courseName">The name of the course</param>
        /// <response code="200">Courses found.</response>
        /// <response code="404">If the course name is provided. The course with that name is not found</response>
        /// <response code="500">Internal application error</response>
        [SwaggerResponse((int) HttpStatusCode.OK, Type = typeof(IEnumerable<CourseDTO>))]
        [SwaggerResponse((int) HttpStatusCode.NotFound, Type = typeof(string))]
        [SwaggerResponse((int) HttpStatusCode.InternalServerError, Type = typeof(string))]
        [HttpGet("{universityId}/courses")]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> FindCoursesByUniversityId(
            [FromRoute] int universityId,
            [FromQuery] string courseName
        )
        {
            if (courseName == null)
            {
                IEnumerable<Course> courses = await _courseService.FindAllByUniversityId(universityId);

                IEnumerable<CourseDTO> coursesDTO = courses
                    .Select(item => _courseConverter.FromEntity(item))
                    .ToList();

                return Ok(coursesDTO);
            }
            else
            {
                Course course = await _courseService.FindByUniversityIdAndCourseName(universityId, courseName);

                if (course == null)
                {
                    return NotFound("Course not found");
                }

                List<CourseDTO> coursesDTO = new List<CourseDTO>();
                coursesDTO.Add(_courseConverter.FromEntity(course));

                return Ok(coursesDTO);
            }
        }

        #endregion


        #region FindTutorsByUniversityIdAndCourseId

        /// <summary>
        /// Finds tutors by an university id and course id
        /// </summary>
        /// <remarks>
        /// Finds all tutors who teaches in a specific university an a specific course
        /// </remarks>
        /// <param name="universityId">The id of the university where tutors teaches</param>
        /// <param name="courseId">The id of the course which is taught by the tutor</param>
        /// <response code="200">Tutors found.</response>
        /// <response code="404">University id or course id not found</response>
        /// <response code="500">Internal application error</response>
        [SwaggerResponse((int) HttpStatusCode.OK, Type = typeof(List<TutorInfo>))]
        [SwaggerResponse((int) HttpStatusCode.NotFound, Type = typeof(string))]
        [SwaggerResponse((int) HttpStatusCode.InternalServerError, Type = typeof(string))]
        [HttpGet("{universityId}/courses/{courseId}/tutors")]
        public async Task<ActionResult<List<TutorInfo>>> FindTutorsByUniversityIdAndCourseId(
            [FromRoute] int universityId,
            [FromRoute] int courseId
        )
        {
            University foundUniversity = await _universityService.FindById(universityId);
            if (foundUniversity == null)
            {
                return NotFound("University not found");
            }

            Course foundCourse = await _courseService.FindById(courseId);
            if (foundCourse == null)
            {
                return NotFound("Course not found");
            }

            IEnumerable<Tutor> tutors = await _tutorService.FindAllByUniversityIdAndCourseId(universityId, courseId);
            IEnumerable<TutorInfo> result = tutors.Select(x => _tutorConverter.TutorToTutorInfo(x));

            return Ok(result);
        }

        #endregion
    }
}