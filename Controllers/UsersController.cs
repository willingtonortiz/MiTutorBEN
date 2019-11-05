using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiTutorBEN.DTOs;
using MiTutorBEN.Models;
using MiTutorBEN.Services;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;


namespace MiTutorBEN.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserService _userService;

        public UsersController(
            ILogger<UsersController> logger,
            IUserService userService
        )
        {
            _logger = logger;
            _userService = userService;
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("isUsernameExist")]
        public async Task<ActionResult<bool>> AuthenticateUsername(string username)
        {
            if (await _userService.UserNameValid(username))
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("isEmailExist")]
        public async Task<ActionResult<bool>> AuthenticateEmail(string email)
        {
            if (await _userService.EmailValid(email))
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }

        }


        /// <summary>
        /// Tutor Subscription
        /// </summary>
        /// <param name="membershipDTO">The credentials for subscription</param>  
        /// <response code="201">Returns the new created tutor</response>
        /// <response code="500">Internet application error</response>
        /// <response code="404">The user not found</response>
        [AllowAnonymous]
        [HttpPost]
        [Route("Subscription")]
        [ProducesResponseType(201)]
        [Produces("application/json")]
        public async Task<ActionResult<TutorDTO>> Subscription([FromBody] MembershipDTO membershipDTO)
        {

            Tutor newTutor = await _userService.Subscription(membershipDTO);

            TutorDTO tutorResponse = new TutorDTO();
            tutorResponse.TutorId = newTutor.TutorId;
            tutorResponse.QualificationCount = 0;
            tutorResponse.Points = 0.0;
            tutorResponse.Description = "Un nuevo tutor";
            tutorResponse.Status = "Available";

            return Ok(tutorResponse);

        }
    }
}