using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiTutorBEN.DTOs;
using MiTutorBEN.Models;
using MiTutorBEN.Services;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MiTutorBEN.Converters;
using System.Security.Cryptography;
using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace MiTutorBEN.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class AuthenticationController : ControllerBase
	{
		#region Attributes

		private readonly ILogger<AuthenticationController> _logger;
		private readonly IAuthService _authService;
		private readonly IUserService _userService;
		private readonly IUniversityService _universityService;
		private readonly UserConverter _userConverter;

		#endregion

		#region Constructor

		public AuthenticationController(
			UserConverter userConverter,
			IAuthService authService,
			ILogger<AuthenticationController> logger,
			IUniversityService universityService,
			IUserService userService)
		{
			_authService = authService;
			_universityService = universityService;
			_logger = logger;
			_userService = userService;
			_userConverter = userConverter;
		}

		#endregion


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


		/// <summary>
		/// Register user
		/// </summary>
		/// <param name="user">The user for register</param>  
		/// <response code="201">Returns the new created user</response>
		/// <response code="400">The request was invalid</response>
		/// <response code="500">Internet application error</response>
		[HttpPost]
		[AllowAnonymous]
		[Route("Register")]
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		[Produces("application/json")]

		public async Task<ActionResult<UserDTO>> Register(
			[FromBody] UserRegisterDTO user
			)
		{
			University university = await _universityService.FindById(user.UniversityId);

			if(university == null){
				return NotFound();
			}

			Person newPerson = new Person();
			newPerson.Name = user.Name;
			newPerson.LastName = user.LastName;
			newPerson.Semester = user.Semester;


			Student newStudent = new Student();
			newStudent.Points = 0;
			newStudent.QualificationCount = 0;


			User newUser = new User();
			newUser.Username = user.Username;
			newUser.Password = user.Password;
			newUser.Role = "student";
			newUser.Email = user.Password;

			newPerson.User = newUser;
			newUser.Person = newPerson;

			newPerson.UniversityId = user.UniversityId;
			university.Persons.Add(newPerson);

			newPerson.Student = newStudent;
			newStudent.Person = newPerson;

			User userCreated = await _authService.Register(newPerson, newStudent, newUser);

			
			return Ok(_userConverter.FromEntity(userCreated));
		}

		/* 
		public string CypherText(string text)
		{
			byte[] salt = new byte[128 / 8];

			using (var random = RandomNumberGenerator.Create())
			{
				random.GetBytes(salt);
			}

			string hashedText = Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password: text,
				salt: salt,
				prf: KeyDerivationPrf.HMACSHA1,
				iterationCount: 10000,
				numBytesRequested: 256 / 5
			));

			return hashedText;
		}
		*/
	}
}