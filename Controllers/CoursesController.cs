using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiTutorBEN.Converters;
using MiTutorBEN.DTOs;
using MiTutorBEN.DTOs.Input;
using MiTutorBEN.Models;
using MiTutorBEN.Services;

namespace MiTutorBEN.Controllers
{
    [Consumes("application/json")]
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ILogger<CoursesController> _logger;
        private readonly ICourseService _courseService;
        private readonly IUniversityService _universityService;
        private readonly CourseConverter _courseConverter;
        private readonly TopicConverter _topicConverter;

        public CoursesController(
            ILogger<CoursesController> logger,
            ICourseService courseService,
            CourseConverter courseConverter,
            IUniversityService universityService,
            TopicConverter topicConverter
        )
        {
            _logger = logger;
            _courseService = courseService;
            _courseConverter = courseConverter;
            _universityService = universityService;
            _topicConverter = topicConverter;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> FindAll()
        {
            IEnumerable<Course> courses = await _courseService.FindAll();

            IEnumerable<CourseDTO> coursesDTO = courses
                .Select(x => _courseConverter.FromEntity(x));

            return Ok(coursesDTO);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>> FindById(
            [FromRoute] int id
        )
        {
            Course course = await _courseService.FindById(id);

            if (course == null)
            {
                return NotFound();
            }

            CourseDTO courseDTO = _courseConverter.FromEntity(course);

            return Ok(courseDTO);
        }

        [HttpGet("{id}/topics")]
        public async Task<ActionResult<IEnumerable<TopicDTO>>> FindTopics(
            [FromRoute] int id
        )
        {
            List<Topic> topics = await _courseService.FindTopics(id);

            if (topics == null)
            {
                return NotFound();
            }

            List<TopicDTO> topicsDtos = new List<TopicDTO>();

            foreach (var t in topics)
            {
                topicsDtos.Add(_topicConverter.FromEntity(t));
            }

            return Ok(topicsDtos);
        }


        [HttpPost]
        public async Task<ActionResult<CourseDTO>> Create(
            [FromBody] CreateCourseInput createCourse
        )
        {
            University university = await _universityService
                .FindById(createCourse.UniversityId);

            if (university == null)
            {
                return NotFound();
            }

            Course course = new Course
            {
                Name = createCourse.Name,
                UniversityId = university.UniversityId
            };

            course = await _courseService.Create(course);

            CourseDTO courseDTO = _courseConverter.FromEntity(course);

            return CreatedAtAction(nameof(FindById), new {id = courseDTO.Id}, courseDTO);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<CourseDTO>> Delete(
            [FromRoute] int id
        )
        {
            Course course = await _courseService.FindById(id);

            if (course == null)
            {
                return NotFound();
            }

            course = await _courseService.DeleteById(id);

            CourseDTO courseDTO = _courseConverter.FromEntity(course);

            return Ok(courseDTO);
        }
    }
}