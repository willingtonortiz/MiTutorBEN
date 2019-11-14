using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiTutorBEN.Data;
using MiTutorBEN.Enums;
using MiTutorBEN.Models;
using MiTutorBEN.Services;

namespace MiTutorBEN.Controllers
{
	[ApiController]
	[AllowAnonymous]
	[Route("api/[controller]")]
	public class UtilsController : ControllerBase
	{
		#region Attributes

		private readonly ILogger<UtilsController> _logger;
		private readonly IUniversityService _universityService;
		private readonly IPersonService _personService;
		private readonly IUserService _userService;
		private readonly ITutoringOfferService _tutoringOfferService;
		private readonly ICourseService _courseService;
		private readonly ITopicService _topicService;
		private readonly ITutorService _tutorService;

		#endregion


		#region Constructor

		public UtilsController(
			ILogger<UtilsController> logger,
			IUniversityService universityService,
			IPersonService personService,
			IUserService userService,
			ITutoringOfferService tutoringOfferService,
			ICourseService courseService,
			ITopicService topicService,
			ITutorService tutorService
		)
		{
			_logger = logger;
			_universityService = universityService;
			_personService = personService;
			_userService = userService;
			_tutoringOfferService = tutoringOfferService;
			_courseService = courseService;
			_tutorService = tutorService;
			_topicService = topicService;
		}

		#endregion


		#region GenerateData

		[HttpGet("generateData")]
		public async Task<ActionResult<string>> GenerateData()
		{
			// University isCreated = _universityService.FindByName("Universidad Peruana de Ciencias Aplicadas");

			// if (isCreated != null)
			// {
			// 	return "Los datos ya se encuentran creados";
			// }


			#region University

			University university1 = new University
			{
				Name = "Universidad Peruana de Ciencias Aplicadas"
			};
			await _universityService.Create(university1);

			#endregion

			// _logger.LogWarning("Universities created");

			#region Course

			Course course1 = new Course
			{
				Name = "calculo 2",
				UniversityId = university1.UniversityId
			};
			await _courseService.Create(course1);
			Course course2 = new Course
			{
				Name = "fisica 3",
				UniversityId = university1.UniversityId
			};
			await _courseService.Create(course2);
			Course course3 = new Course
			{
				Name = "programacion 1",
				UniversityId = university1.UniversityId
			};
			await _courseService.Create(course3);

			#endregion

			// _logger.LogWarning("Courses created");

			#region Topic

			Topic topic_1_1 = new Topic
			{
				Name = "topic_1_1",
				CourseId = course1.CourseId
			};
			await _topicService.Create(topic_1_1);
			Topic topic_1_2 = new Topic
			{
				Name = "topic_1_2",
				CourseId = course1.CourseId
			};
			await _topicService.Create(topic_1_2);
			Topic topic_1_3 = new Topic
			{
				Name = "topic_1_3",
				CourseId = course1.CourseId
			};
			await _topicService.Create(topic_1_3);


			Topic topic_2_1 = new Topic
			{
				Name = "topic_2_1",
				CourseId = course1.CourseId
			};
			await _topicService.Create(topic_2_1);
			Topic topic_2_2 = new Topic
			{
				Name = "topic_2_2",
				CourseId = course1.CourseId
			};
			await _topicService.Create(topic_2_2);
			Topic topic_2_3 = new Topic
			{
				Name = "topic_2_3",
				CourseId = course1.CourseId
			};
			await _topicService.Create(topic_2_3);


			Topic topic_3_1 = new Topic
			{
				Name = "topic_3_1",
				CourseId = course1.CourseId
			};
			await _topicService.Create(topic_3_1);
			Topic topic_3_2 = new Topic
			{
				Name = "topic_3_2",
				CourseId = course1.CourseId
			};
			await _topicService.Create(topic_3_2);
			Topic topic_3_3 = new Topic
			{
				Name = "topic_3_3",
				CourseId = course1.CourseId
			};
			await _topicService.Create(topic_3_3);

			#endregion

			// _logger.LogWarning("Topics created");

			#region Person

			// TODO: Agregar carreras a las personas
			Person person1 = new Person
			{
				Name = "person_1",
				LastName = "lastname_1",
				Semester = 6,
				UniversityId = university1.UniversityId
			};
			await _personService.Create(person1);
			Person person2 = new Person
			{
				Name = "person_2",
				LastName = "lastname_2",
				Semester = 5,
				UniversityId = university1.UniversityId
			};
			await _personService.Create(person2);
			Person person3 = new Person
			{
				Name = "person_3",
				LastName = "lastname_3",
				Semester = 4,
				UniversityId = university1.UniversityId
			};
			await _personService.Create(person3);

			#endregion

			// _logger.LogWarning("People created");

			#region User

			User user1 = new User
			{
				Username = "username_1",
				Password = "password_1",
				Email = "email1@email.com",
				Role = RoleType.Tutor,
				Person = person1
			};
			await _userService.Create(user1);
			User user2 = new User
			{
				Username = "username_2",
				Password = "password_2",
				Email = "email2@email.com",
				Role = RoleType.Tutor,
				Person = person2
			};
			await _userService.Create(user2);
			User user3 = new User
			{
				Username = "username_3",
				Password = "password_3",
				Email = "email3@email.com",
				Role = RoleType.Tutor,
				Person = person3
			};
			await _userService.Create(user3);

			#endregion

			// _logger.LogWarning("Users created");

			#region Tutor

			Tutor tutor1 = new Tutor
			{
				Description = "Tutor de calculo",
				Points = 3.06,
				QualificationCount = 50,
				Person = person1,
				Status = TutorStatus.AVAILABLE,
			};
			tutor1.TutorCourses.Add(new TutorCourse
			{
				CourseId = course1.CourseId
			});
			await _tutorService.Create(tutor1);

			#endregion

			// _logger.LogWarning("Tutors created");

			#region TutoringOffer

			TutoringOffer tutoringOffer1 = new TutoringOffer
			{
				StartTime = new DateTime(),
				EndTime = new DateTime(),
				Capacity = 5,
				Description = "tutoria de calculo 2",
				TutorId = tutor1.TutorId,
				CourseId = course1.CourseId,
				UniversityId = university1.UniversityId,
				TopicTutoringOffers = new List<TopicTutoringOffer>
				{
					new TopicTutoringOffer
					{
						TopicId = topic_1_1.TopicId
					},
					new TopicTutoringOffer
					{
						TopicId = topic_1_2.TopicId
					},
					new TopicTutoringOffer
					{
						TopicId = topic_1_3.TopicId
					}
				}
			};
			await _tutoringOfferService.Create(tutoringOffer1);

			#endregion

			// _logger.LogWarning("TutoringOffers created");

			return "Datos de prueba cargados";
		}

		#endregion


		#region DeleteData

		[HttpGet("deleteData")]
		public async Task<ActionResult<string>> DeleteData()
		{
			await _tutoringOfferService.DeleteAll();
			await _tutorService.DeleteAll();
			await _userService.DeleteAll();
			await _personService.DeleteAll();
			await _topicService.DeleteAll();
			await _courseService.DeleteAll();
			await _universityService.DeleteAll();

			return "Los datos han sido eliminados";
		}

		#endregion


		#region HeartBeat

		[AllowAnonymous]
		[HttpGet("heartbeat")]
		public ActionResult HeartBeat()
		{
			return Ok();
		}

		#endregion

		#region Timeout

		[AllowAnonymous]
		[HttpGet("timeout")]
		public ActionResult Timeout()
		{
            // Delayed
			System.Threading.Thread.Sleep(6100);
            
            // Normal
			// System.Threading.Thread.Sleep(1000);
			return Ok();
		}

		#endregion
	}
}