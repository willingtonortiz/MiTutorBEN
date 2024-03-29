using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiTutorBEN.DTOs;
using MiTutorBEN.Models;
using MiTutorBEN.Services;
using System.Threading.Tasks;
using MiTutorBEN.Converters;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace MiTutorBEN.Controllers
{
	[AllowAnonymous]
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


		#region Login

		/// <summary>
		/// Authenticates the user
		/// </summary>
		/// <remarks>
		/// Authenticates the user and returns basic user credentials and a JWT Token
		/// </remarks>
		/// <param name="loginUser">User data</param>
		/// <response code="200">User authenticated successfully.</response>
		/// <response code="400">The payload format is incorrect</response>
		/// <response code="400">Username or password are incorrect</response>
		/// <response code="500">Internal application error</response>
		[SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(AuthenticatedUser))]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(string))]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
		[HttpPost("login")]
		public ActionResult<AuthenticatedUser> Login([FromBody] LoginUser loginUser)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			AuthenticatedUser authenticatedUser = _authService.Login(loginUser.Username, loginUser.Password);

			if (authenticatedUser == null)
			{
				return BadRequest(new { message = "Nombre de usuario o contraseña incorrectos" });
			}

			return Ok(authenticatedUser);
		}

		#endregion


		#region Register

		/// <summary>
		/// Register user
		/// </summary>
		/// <param name="user">The user for register</param>  
		/// <response code="201">Returns the new created user</response>
		/// <response code="400">The request was invalid</response>
		/// <response code="500">Internet application error</response>
		[HttpPost("register")]
		// [Route("Register")]
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		[Produces("application/json")]
		public async Task<ActionResult<UserDTO>> Register(
			[FromBody] UserRegisterDTO user
		)
		{
			/* SE DEBE VERIFICAR QUE LA UNIVERSIDAD EXISTA, ARREGLAR */
					University university = await _universityService.FindById(user.UniversityId);

			if (university == null)
			{
				_logger.LogWarning(user.Name.ToString());
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
			newUser.Role = "STUDENT";
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

		#endregion


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