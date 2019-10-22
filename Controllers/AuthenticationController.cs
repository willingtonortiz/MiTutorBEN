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
	[Route("[controller]")]
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
		public async Task<IActionResult> Register([FromBody] object sd)
		{
			var ob = JObject.Parse(sd.ToString());

			//_userService.GetPerson(3);

			_logger.LogWarning("Estas enviando un usuario");
			//_logger.LogWarning(ob.ToString());

			// _logger.LogWarning(newUser.Password.ToString());

			// _authService.RegisterUser(newUser);

			University university = await _universityService.FindById(ob["person"]["UniversityId"].ToObject<int>());

			university.Persons = new List<Person>();

			_logger.LogWarning(university.UniversityId.ToString());
			Person newPerson = new Person();

			newPerson.Name = ob["person"]["Name"].ToObject<string>();
			newPerson.LastName = ob["person"]["LastName"].ToObject<string>();
			newPerson.Semester = ob["person"]["Semester"].ToObject<int>();



			Student newStudent = new Student();
			newStudent.Points = 0;
			newStudent.QualificationCount = 0;

			User newUser = new User();
			newUser.Username = ob["user"]["Username"].ToObject<string>();
			newUser.Password = ob["user"]["Password"].ToObject<string>();
			newUser.Role = ob["user"]["Role"].ToObject<string>();
			newUser.Email = ob["user"]["Email"].ToObject<string>();


			newPerson.User = newUser;
			newUser.Person = newPerson;


			newPerson.UniversityId = ob["person"]["UniversityId"].ToObject<int>();
			university.Persons.Add(newPerson);


			newPerson.Student = newStudent;
			newStudent.Person = newPerson;


			_authService.Register(newPerson, newStudent, newUser);
			return Ok(true);
		}
	}
}