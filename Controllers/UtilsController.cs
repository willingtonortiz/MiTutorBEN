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
		private readonly ITutoringSessionService _tutoringSessionService;
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
			ITutoringSessionService tutoringSessionService,
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
			_tutoringSessionService = tutoringSessionService;
			_courseService = courseService;
			_tutorService = tutorService;
			_topicService = topicService;
		}

		#endregion


		#region GenerateData

		[HttpGet("generateData")]
		public async Task<ActionResult<string>> GenerateData()
		{
			#region University

			University university1 = new University
			{
				Name = "Universidad Peruana de Ciencias Aplicadas"
			};
			await _universityService.Create(university1);
			// _logger.LogWarning("Universities created");

			#endregion


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
			// _logger.LogWarning("Courses created");

			#endregion


			#region Topic

			Topic topic_1_1 = new Topic
			{
				Name = "limites",
				CourseId = course1.CourseId
			};
			await _topicService.Create(topic_1_1);
			Topic topic_1_2 = new Topic
			{
				Name = "derivadas",
				CourseId = course1.CourseId
			};
			await _topicService.Create(topic_1_2);
			Topic topic_1_3 = new Topic
			{
				Name = "integrales",
				CourseId = course1.CourseId
			};
			await _topicService.Create(topic_1_3);


			Topic topic_2_1 = new Topic
			{
				Name = "punteros",
				CourseId = course2.CourseId
			};
			await _topicService.Create(topic_2_1);
			Topic topic_2_2 = new Topic
			{
				Name = "matrices",
				CourseId = course2.CourseId
			};
			await _topicService.Create(topic_2_2);
			Topic topic_2_3 = new Topic
			{
				Name = "sentencias condicionales",
				CourseId = course2.CourseId
			};
			await _topicService.Create(topic_2_3);


			Topic topic_3_1 = new Topic
			{
				Name = "topic_3_1",
				CourseId = course3.CourseId
			};
			await _topicService.Create(topic_3_1);
			Topic topic_3_2 = new Topic
			{
				Name = "topic_3_2",
				CourseId = course3.CourseId
			};
			await _topicService.Create(topic_3_2);
			Topic topic_3_3 = new Topic
			{
				Name = "topic_3_3",
				CourseId = course3.CourseId
			};
			await _topicService.Create(topic_3_3);
			// _logger.LogWarning("Topics created");

			#endregion


			#region Person

			Person person1 = new Person
			{
				Name = "Kevin",
				LastName = "Mitchell",
				Semester = 6,
				Career = "matematica pura",
				UniversityId = university1.UniversityId
			};
			await _personService.Create(person1);
			Person person2 = new Person
			{
				Name = "Elaine",
				LastName = "Sims",
				Semester = 5,
				Career = "ingeniería de sistemas",
				UniversityId = university1.UniversityId
			};
			await _personService.Create(person2);
			Person person3 = new Person
			{
				Name = "Alexander",
				LastName = "Eastman",
				Semester = 4,
				Career = "ingeniería de software",
				UniversityId = university1.UniversityId
			};
			await _personService.Create(person3);
			// _logger.LogWarning("People created");

			#endregion


			#region User

			User user1 = new User
			{
				Username = "Kevin",
				Password = "Mitchell",
				Email = "KevinJMitchell@gustr.com",
				Role = RoleType.Tutor,
				Person = person1
			};
			await _userService.Create(user1);
			User user2 = new User
			{
				Username = "Elaine",
				Password = "Sims",
				Email = "ElaineDSims@gustr.com",
				Role = RoleType.Tutor,
				Person = person2
			};
			await _userService.Create(user2);
			User user3 = new User
			{
				Username = "Alexander",
				Password = "Eastman",
				Email = "AlexanderSEastman@gustr.com",
				Role = RoleType.Tutor,
				Person = person3
			};
			await _userService.Create(user3);
			// _logger.LogWarning("Users created");

			#endregion


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

			Tutor tutor2 = new Tutor
			{
				Description = "Tutor de fisica",
				Points = 4.12,
				QualificationCount = 35,
				Person = person2,
				Status = TutorStatus.AVAILABLE,
			};
			tutor2.TutorCourses.Add(new TutorCourse
			{
				CourseId = course2.CourseId
			});
			await _tutorService.Create(tutor2);

			Tutor tutor3 = new Tutor
			{
				Description = "Tutor de programación",
				Points = 4.75,
				QualificationCount = 75,
				Person = person3,
				Status = TutorStatus.AVAILABLE,
			};
			tutor3.TutorCourses.Add(new TutorCourse
			{
				CourseId = course3.CourseId
			});
			await _tutorService.Create(tutor3);
			// _logger.LogWarning("Tutors created");

			#endregion


			#region TutoringOffer

			TutoringOffer tutoringOffer1 = new TutoringOffer
			{
				StartTime = new DateTime(2019, 11, 19),
				EndTime = new DateTime(2019, 11, 21),
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

			TutoringSession tutoringSession_1_1 = new TutoringSession
			{
				TutoringOfferId = tutoringOffer1.TutoringOfferId,
				StartTime = new DateTime(2019, 11, 19, 16, 0, 0),
				EndTime = new DateTime(2019, 11, 19, 18, 0, 0),
				Description = "Primer repaso de cálculo",
				Place = "UPC Villa cubículo 23",
				Price = 20,
				StudentCount = 0,
				TopicTutoringSessions = new List<TopicTutoringSession>{
					new TopicTutoringSession
					{
						TopicId = topic_1_1.TopicId
					}
				},
				TutorId = tutor1.TutorId
			};
			await _tutoringSessionService.Create(tutoringSession_1_1);

			TutoringSession tutoringSession_1_2 = new TutoringSession
			{
				TutoringOfferId = tutoringOffer1.TutoringOfferId,
				StartTime = new DateTime(2019, 11, 20, 16, 0, 0),
				EndTime = new DateTime(2019, 11, 20, 18, 0, 0),
				Description = "Segundo repaso de cálculo",
				Place = "UPC Villa cubículo 25",
				Price = 20,
				StudentCount = 0,
				TopicTutoringSessions = new List<TopicTutoringSession>{
					new TopicTutoringSession
					{
						TopicId = topic_1_2.TopicId
					}
				},
				TutorId = tutor1.TutorId
			};
			await _tutoringSessionService.Create(tutoringSession_1_2);

			TutoringSession tutoringSession_1_3 = new TutoringSession
			{
				TutoringOfferId = tutoringOffer1.TutoringOfferId,
				StartTime = new DateTime(2019, 11, 21, 16, 0, 0),
				EndTime = new DateTime(2019, 11, 21, 18, 0, 0),
				Description = "Último repaso de cálculo",
				Place = "UPC Villa cubículo 30",
				Price = 20,
				StudentCount = 0,
				TopicTutoringSessions = new List<TopicTutoringSession>{
					new TopicTutoringSession
					{
						TopicId = topic_1_3.TopicId
					}
				},
				TutorId = tutor1.TutorId
			};
			await _tutoringSessionService.Create(tutoringSession_1_3);


			TutoringOffer tutoringOffer2 = new TutoringOffer
			{
				StartTime = new DateTime(2019, 11, 19),
				EndTime = new DateTime(2019, 11, 21),
				Capacity = 5,
				Description = "tutoria de física",
				TutorId = tutor2.TutorId,
				CourseId = course2.CourseId,
				UniversityId = university1.UniversityId,
				TopicTutoringOffers = new List<TopicTutoringOffer>
				{
					new TopicTutoringOffer
					{
						TopicId = topic_2_1.TopicId
					},
					new TopicTutoringOffer
					{
						TopicId = topic_2_2.TopicId
					},
					new TopicTutoringOffer
					{
						TopicId = topic_2_3.TopicId
					}
				}
			};
			await _tutoringOfferService.Create(tutoringOffer2);

			TutoringSession tutoringSession_2_1 = new TutoringSession
			{
				TutoringOfferId = tutoringOffer2.TutoringOfferId,
				StartTime = new DateTime(2019, 11, 19, 10, 0, 0),
				EndTime = new DateTime(2019, 11, 19, 12, 0, 0),
				Description = "Primer repaso de física",
				Place = "UPC Villa cubículo 02",
				Price = 20,
				StudentCount = 0,
				TopicTutoringSessions = new List<TopicTutoringSession>{
					new TopicTutoringSession
					{
						TopicId = topic_2_1.TopicId
					}
				},
				TutorId = tutor2.TutorId,
			};
			await _tutoringSessionService.Create(tutoringSession_2_1);

			TutoringSession tutoringSession_2_2 = new TutoringSession
			{
				TutoringOfferId = tutoringOffer2.TutoringOfferId,
				StartTime = new DateTime(2019, 11, 20, 10, 0, 0),
				EndTime = new DateTime(2019, 11, 20, 12, 0, 0),
				Description = "Repaso final",
				Place = "UPC Villa cubículo 07",
				Price = 20,
				StudentCount = 0,
				TopicTutoringSessions = new List<TopicTutoringSession>{
					new TopicTutoringSession
					{
						TopicId = topic_2_2.TopicId
					},
					new TopicTutoringSession
					{
						TopicId = topic_2_3.TopicId
					}
				},
				TutorId = tutor2.TutorId,
			};
			await _tutoringSessionService.Create(tutoringSession_2_2);


			TutoringOffer tutoringOffer3 = new TutoringOffer
			{
				StartTime = new DateTime(2019, 11, 19),
				EndTime = new DateTime(2019, 11, 21),
				Capacity = 5,
				Description = "Preparación para el examen final O_O",
				TutorId = tutor3.TutorId,
				CourseId = course3.CourseId,
				UniversityId = university1.UniversityId,
				TopicTutoringOffers = new List<TopicTutoringOffer>
				{
					new TopicTutoringOffer
					{
						TopicId = topic_3_1.TopicId
					},
					new TopicTutoringOffer
					{
						TopicId = topic_3_2.TopicId
					},
					new TopicTutoringOffer
					{
						TopicId = topic_3_3.TopicId
					}
				}
			};
			await _tutoringOfferService.Create(tutoringOffer3);

			TutoringSession tutoringSession_3_1 = new TutoringSession
			{
				TutoringOfferId = tutoringOffer3.TutoringOfferId,
				StartTime = new DateTime(2019, 11, 19, 15, 0, 0),
				EndTime = new DateTime(2019, 11, 19, 17, 0, 0),
				Description = "Primer repaso de programación 1",
				Place = "UPC Villa cubículo 11",
				Price = 20,
				StudentCount = 0,
				TopicTutoringSessions = new List<TopicTutoringSession>{
					new TopicTutoringSession
					{
						TopicId = topic_3_1.TopicId
					}
				},
				TutorId = tutor3.TutorId,
			};
			await _tutoringSessionService.Create(tutoringSession_3_1);

			TutoringSession tutoringSession_3_2 = new TutoringSession
			{
				TutoringOfferId = tutoringOffer3.TutoringOfferId,
				StartTime = new DateTime(2019, 11, 20, 15, 0, 0),
				EndTime = new DateTime(2019, 11, 20, 17, 0, 0),
				Description = "Segundo repaso de programación 1",
				Place = "UPC Villa cubículo 12",
				Price = 20,
				StudentCount = 0,
				TopicTutoringSessions = new List<TopicTutoringSession>{
					new TopicTutoringSession
					{
						TopicId = topic_3_2.TopicId
					}
				},
				TutorId = tutor3.TutorId,
			};
			await _tutoringSessionService.Create(tutoringSession_3_2);

			TutoringSession tutoringSession_3_3 = new TutoringSession
			{
				TutoringOfferId = tutoringOffer3.TutoringOfferId,
				StartTime = new DateTime(2019, 11, 21, 15, 0, 0),
				EndTime = new DateTime(2019, 11, 21, 17, 0, 0),
				Description = "Último repaso de programación 1",
				Place = "UPC Villa cubículo 13",
				Price = 20,
				StudentCount = 0,
				TopicTutoringSessions = new List<TopicTutoringSession>{
					new TopicTutoringSession
					{
						TopicId = topic_3_3.TopicId
					}
				},
				TutorId = tutor3.TutorId,
			};
			await _tutoringSessionService.Create(tutoringSession_3_3);
			// _logger.LogWarning("TutoringOffers created");

			#endregion


			return "Datos de prueba cargados";
		}

		#endregion


		#region DeleteData

		[HttpGet("deleteData")]
		public async Task<ActionResult<string>> DeleteData()
		{
			await _tutoringOfferService.DeleteAll();
			await _tutoringSessionService.DeleteAll();
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