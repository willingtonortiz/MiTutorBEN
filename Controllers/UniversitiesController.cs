using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MiTutorBEN.Converters;
using MiTutorBEN.DTOs;
using MiTutorBEN.DTOs.Responses;
using MiTutorBEN.Models;
using MiTutorBEN.Services;

namespace MiTutorBEN.Controllers
{
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
                return NotFound(new { message = "No se encontró la universidad" });
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
                return Created("", new { message = "Ya existe una universidad con ese nombre" });
            }

            return Created($"", _universityConverter.FromEntity(created));
        }

        #endregion


        #region Delete
        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="universityId"></param> 
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

        #endregion


        #region FindTutoringOffersByUniversityIdAndCourseId

        [HttpGet("{universityId}/courses/{courseId}/tutoringoffers")]
        public async Task<ActionResult<List<TutoringOfferResponse>>> FindTutoringOffersByUniversityIdAndCourseId(int universityId, int courseId)
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
            List<TutoringOfferResponse> result = tutoringOffers.Select(x => new TutoringOfferResponse
            {
                TutoringOfferId = x.TutoringOfferId,
                Course = x.Course.Name,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Tutor = x.Tutor.Person.FullName
            }).ToList<TutoringOfferResponse>();

            return Ok(result);
        }

        #endregion


        #region FindCoursesByUniversityId

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
                    return NotFound(new { message = "Course not found" });
                }

                List<CourseDTO> coursesDTO = new List<CourseDTO>();
                coursesDTO.Add(_courseConverter.FromEntity(course));

                return Ok(coursesDTO);

            }
        }

        #endregion


        #region FindTutorsByUniversityIdAndCourseId

        /// <summary>
        /// Finds all tutors by an university id and course id
        /// </summary>
        [HttpGet("{universityId}/courses/{courseId}/tutors")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Produces("application/json")]
        public async Task<ActionResult<List<Tutor>>> FindTutorsByUniversityIdAndCourseId(
            [FromRoute] int universityId,
            [FromRoute] int courseId
        )
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

            IEnumerable<Tutor> tutors = await _tutorService.FindAllByUniversityIdAndCourseId(universityId, courseId);
            IEnumerable<TutorDTO> result = tutors.Select(x => _tutorConverter.FromEntity(x));

            return Ok(result);
        }

        #endregion
    }
}