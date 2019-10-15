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
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        private readonly IUserService _userService;

        public UserController(
            IUserService userService,
            ILogger<UserController> logger
        )
        {   
             _userService =  userService;
            _logger =  logger;

        }


        [AllowAnonymous]
        [HttpGet]
        [Route("isUsernameExist")]
        public ActionResult Authenticate(string username)
        {
            if(_userService.UserNameValid(username)){
                _logger.LogWarning("El usuario existe en la bd");
                return Ok(true);
            }
            else{
                _logger.LogWarning("El usuario no existe");
                return Ok(false);
            }
            
        }
    }
}