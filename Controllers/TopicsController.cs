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
	[ApiController]
	[AllowAnonymous]
	[Route("api/[controller]")]
	public class TopicsController : ControllerBase
	{
		#region Attributes

		private readonly ILogger<TopicsController> _logger;
		private readonly ITopicService _topicService;
		private readonly ICourseService _courseService;
		private readonly TopicConverter _topicConverter;
		private readonly CourseConverter _courseConverter;

		#endregion


		#region Constructor

		public TopicsController(
			ILogger<TopicsController> logger,
			ITopicService topicService,
			TopicConverter topicConverter,
			ICourseService courseService,
			CourseConverter courseConverter
			)
		{
			_logger = logger;
			_topicService = topicService;
			_topicConverter = topicConverter;
			_courseService = courseService;
			_courseConverter = courseConverter;
		}

		#endregion


		[HttpGet]
		public async Task<ActionResult<IEnumerable<TopicDTO>>> FindAll()
		{
			IEnumerable<Topic> topics = await _topicService.FindAll();

			IEnumerable<TopicDTO> topicsDTO = topics
				.Select(x => _topicConverter.FromEntity(x));

			return Ok(topicsDTO);
		}


		[HttpGet("{id}")]
		public async Task<ActionResult<TopicDTO>> FindById(
			[FromRoute] int id
		)
		{
			Topic topic = await _topicService.FindById(id);

			if (topic == null)
			{
				return NotFound();
			}

			TopicDTO topicDTO = _topicConverter.FromEntity(topic);

			return Ok(topicDTO);
		}


		[HttpPost]
		public async Task<ActionResult<TopicDTO>> Create(
			[FromBody] CreateTopicInput createTopic
		)
		{
			Course course = await _courseService
				.FindById(createTopic.CourseId);

			if (course == null)
			{
				return NotFound();
			}

			Topic newTopic = new Topic
			{
				CourseId = createTopic.CourseId,
				Name = createTopic.Name
			};
			newTopic = await _topicService
				.Create(newTopic);

			TopicDTO topicDTO = _topicConverter
				.FromEntity(newTopic);

			return Ok(topicDTO);
		}


		[HttpDelete("{id}")]
		public async Task<ActionResult<TopicDTO>> Delete(
			[FromRoute] int id
		)
		{
			Topic foundTopic = await _topicService
				.FindById(id);

			if (foundTopic == null)
			{
				return NotFound();
			}

			foundTopic = await _topicService
				.DeleteById(id);

			TopicDTO topicDTO = _topicConverter
				.FromEntity(foundTopic);

			return Ok(topicDTO);
		}
	}
}