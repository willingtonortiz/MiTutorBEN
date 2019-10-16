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

		public UtilsController(
			ILogger<UtilsController> logger,
			IUniversityService universityService,
			IPersonService personService,
			IUserService userService
			)
		{
			_logger = logger;
			_universityService = universityService;
			_personService = personService;
			_userService = userService;
		}

		[HttpGet("generatedata")]
		public ActionResult<string> GenerateData()
		{

			// Creando universidades
			University university = new University
			{
				Name = "Universidad Peruana de Ciencias Aplicadas"
			};
			_universityService.Create(university);


			// Creando personas
			Person person1 = new Person
			{
				Name = "person_1",
				LastName = "lastname_1",
				Semester = 6
			};
			_personService.Create(person1);
			Person person2 = new Person
			{
				Name = "person_2",
				LastName = "lastname_2",
				Semester = 5
			};
			_personService.Create(person2);
			Person person3 = new Person
			{
				Name = "person_3",
				LastName = "lastname_3",
				Semester = 4
			};
			_personService.Create(person3);


			// Creando usuarios
			User user1 = new User
			{
				Username = "username_1"	,
				Password = "password_1",
				Email = "email1@email.com",
				Role = "TUTOR"
			};
			_userService.Create(user1);
			User user2 = new User
			{
				Username = "username_2"	,
				Password = "password_2",
				Email = "email2@email.com",
				Role = "STUDENT"
			};
			_userService.Create(user2);
			User user3 = new User
			{
				Username = "username_3"	,
				Password = "password_3",
				Email = "email3@email.com",
				Role = "STUDENT"
			};
			_userService.Create(user3);
			
			return null;
		}
	}
}