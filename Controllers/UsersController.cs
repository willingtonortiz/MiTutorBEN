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
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        private readonly IUserService _userService;

        public UsersController(
            IUserService userService,
            ILogger<UserController> logger
        )
        {
            _userService = userService;
            _logger = logger;

        }


        [AllowAnonymous]
        [HttpGet]
        [Route("isUsernameExist")]
        public ActionResult AuthenticateUsername(string username)
        {
            if (_userService.UserNameValid(username))
            {
                _logger.LogWarning("El usuario existe en la bd");
                return Ok(true);
            }
            else
            {
                _logger.LogWarning("El usuario no existe");
                return Ok(false);
            }

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("isEmailExist")]
        public ActionResult AuthenticateEmail(string email)
        {
            if (_userService.EmailValid(email))
            {
                _logger.LogWarning("El usuario existe en la bd");
                return Ok(true);
            }
            else
            {
                _logger.LogWarning("El usuario no existe");
                return Ok(false);
            }

        }



    }
}