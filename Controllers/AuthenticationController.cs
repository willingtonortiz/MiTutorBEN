using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiTutorBEN.DTOs;
using MiTutorBEN.Models;
using MiTutorBEN.Services;
using Newtonsoft.Json.Linq;
namespace MiTutorBEN.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthenticationController> _logger;


        public AuthenticationController(IAuthService authService, ILogger<AuthenticationController> logger)
        {
            _authService = authService;
            _logger = logger;
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
        public IActionResult Register([FromBody] object sd)
        {
            var ob = JObject.Parse(sd.ToString());

            JToken token1 = ob["user"];
            if (token1 != null)
            {
                // _logger.LogWarning("Estas enviando un usuario");
                User newUser = new User();
                newUser.Username = ob["user"]["Username"].ToObject<string>();
                newUser.Password = ob["user"]["Password"].ToObject<string>();
                newUser.Role = ob["user"]["Role"].ToObject<string>();
                newUser.Email = ob["user"]["Email"].ToObject<string>();

                _logger.LogWarning(newUser.Password.ToString());

                _authService.RegisterUser(newUser);
                return Ok(newUser);
            }
            else
            {
                _logger.LogWarning(ob.ToString());
                Student newStudent =  new Student();
                Person newPerson = new Person();

                newPerson.Name = ob["person"]["Name"].ToObject<string>();
                newPerson.LastName = ob["person"]["LastName"].ToObject<string>();
                newPerson.Semester = ob["person"]["Semester"].ToObject<int>();
                newPerson.UniversityId = ob["person"]["UniversityId"].ToObject<int>();
                newPerson.UserId = ob["person"]["UserId"].ToObject<int>();

                newStudent.StudentId =  ob["person"]["UserId"].ToObject<int>();
                newStudent.Points = 0;

                _authService.RegisterPerson(newPerson,newStudent);
                return Ok(true);



            }
        }




    }
}