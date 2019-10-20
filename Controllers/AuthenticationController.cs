using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiTutorBEN.DTOs;
using MiTutorBEN.Models;
using MiTutorBEN.Services;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiTutorBEN.Controllers

{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class AuthenticationController : ControllerBase
	{
		private readonly IAuthService _authService;
		private readonly ILogger<AuthenticationController> _logger;

		private readonly IUserService _userService;

		private readonly IUniversityService _universityService;

		public AuthenticationController(IAuthService authService, ILogger<AuthenticationController> logger, IUniversityService universityService, IUserService userService)
		{
			_authService = authService;
			_universityService = universityService;
			_logger = logger;
			_userService = userService;
		}


		[AllowAnonymous]
		[HttpPost]
		public IActionResult Authenticate([FromBody] User userParam)
		{
			UserAuthDTO user = _authService.Authenticate(userParam.Username, userParam.Password);

			if (user == null)
			{
				return BadRequest(new { message = "Nombre de usuario o contraseña incorrectos" });
			}

			return Ok(user);
		}


		[AllowAnonymous]
		[HttpPost]
		[Route("Register")]
		public async Task<IActionResult> Register([FromBody] object user)
		{
			var userJson = JObject.Parse(user.ToString());

	
			University university = await _universityService.FindById(userJson["person"]["universityId"].ToObject<int>());

			university.Persons = new List<Person>();

			
			Person newPerson = new Person();

			newPerson.Name = userJson["person"]["name"].ToObject<string>();
			newPerson.LastName = userJson["person"]["lastName"].ToObject<string>();
			newPerson.Semester = userJson["person"]["semester"].ToObject<int>();



			Student newStudent = new Student();
			newStudent.Points = 0;
			newStudent.QualificationCount = 0;

			User newUser = new User();
			newUser.Username = userJson["user"]["username"].ToObject<string>();
			newUser.Password = userJson["user"]["password"].ToObject<string>();
			newUser.Role = userJson["user"]["role"].ToObject<string>();
			newUser.Email = userJson["user"]["email"].ToObject<string>();


			newPerson.User = newUser;
			newUser.Person = newPerson;


			newPerson.UniversityId = userJson["person"]["universityId"].ToObject<int>();
			university.Persons.Add(newPerson);


			newPerson.Student = newStudent;
			newStudent.Person = newPerson;


			await _authService.Register(newPerson, newStudent, newUser);
			return Ok(true);
		}
	}
}