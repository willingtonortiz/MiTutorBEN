using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiTutorBEN.Data;
using MiTutorBEN.Models;
using MiTutorBEN.Services;

namespace MiTutorBEN.Controllers
{
	[ApiController]
	[AllowAnonymous]
	[Route("api/[controller]")]
	public class UtilsController
	{
		private readonly ILogger<UtilsController> _logger;
		private readonly IUniversityService _universityService;
		private readonly IPersonService _personService;
		private readonly IUserService _userService;
		private readonly ITutoringOfferService _tutoringOfferService;
		private readonly ICourseService _courseService;
		private readonly ITopicService _topicService;
		private readonly ITutorService _tutorService;

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


		[HttpGet("generateData")]
		public async Task<ActionResult<string>> GenerateData()
		{
			// University isCreated = _universityService.FindByName("Universidad Peruana de Ciencias Aplicadas");

			// if (isCreated != null)
			// {
			// 	return "Los datos ya se encuentran creados";
			// }


			#region University

			// Creando universidades
			University university1 = new University
			{
				Name = "Universidad Peruana de Ciencias Aplicadas"
			};
			await _universityService.Create(university1);

			#endregion

			_logger.LogWarning("Universities created");

			#region Course

			// Creando cursos
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

			_logger.LogWarning("Courses created");

			#region Topic

			// Creando topicss
			Topic topic1 = new Topic
			{
				Name = "topic_1_1",
				CourseId = course1.CourseId
			};
			await _topicService.Create(topic1);
			Topic topic2 = new Topic
			{
				Name = "topic_1_2",
				CourseId = course1.CourseId
			};
			await _topicService.Create(topic2);
			Topic topic3 = new Topic
			{
				Name = "topic_1_3",
				CourseId = course1.CourseId
			};
			await _topicService.Create(topic3);

			#endregion

			_logger.LogWarning("Topics created");

			#region Person

			// Creando personas
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

			_logger.LogWarning("People created");

			#region User

			// Creando usuarios
			User user1 = new User
			{
				Username = "username_1",
				Password = "password_1",
				Email = "email1@email.com",
				Role = "TUTOR",
				Person = person1
			};
			await _userService.Create(user1);
			User user2 = new User
			{
				Username = "username_2",
				Password = "password_2",
				Email = "email2@email.com",
				Role = "STUDENT",
				Person = person2
			};
			await _userService.Create(user2);
			User user3 = new User
			{
				Username = "username_3",
				Password = "password_3",
				Email = "email3@email.com",
				Role = "STUDENT",
				Person = person3
			};
			await _userService.Create(user3);

			#endregion

			_logger.LogWarning("Users created");

			#region Tutor

			// Creando tutores
			Tutor tutor1 = new Tutor
			{
				Description = "Tutor de calculo",
				Points = 3.06,
				QualificationCount = 50,
				Person = person1
			};
			await _tutorService.Create(tutor1);

			#endregion

			_logger.LogWarning("Tutors created");

			#region TutoringOffer

			// Creando tutorings offers
			TutoringOffer tutoringOffer1 = new TutoringOffer
			{
				StartTime = new DateTime(),
				EndTime = new DateTime(),
				Capacity = 5,
				Description = "tutoria de calculo",
				TutorId = tutor1.TutorId,
				CourseId = course1.CourseId,
				UniversityId = university1.UniversityId,
				TopicTutoringOffers = new List<TopicTutoringOffer>
				{
					new TopicTutoringOffer{
						TopicId = topic1.TopicId
					},
					new TopicTutoringOffer{
						TopicId = topic2.TopicId
					},
					new TopicTutoringOffer{
						TopicId = topic3.TopicId
					}
				}
			};
			await _tutoringOfferService.Create(tutoringOffer1);

			#endregion

			_logger.LogWarning("TutoringOffers created");

			return "Datos de prueba cargados";
		}

		[HttpGet("deleteData")]
		public ActionResult<string> DeleteData()
		{
			_tutoringOfferService.DeleteAll();
			_tutorService.DeleteAll();
			_userService.DeleteAll();
			_personService.DeleteAll();
			_topicService.DeleteAll();
			_courseService.DeleteAll();
			_universityService.DeleteAll();


			return "Los datos han sido eliminados";
		}
	}
}