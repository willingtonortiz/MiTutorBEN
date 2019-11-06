using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiTutorBEN.DTOs;
using MiTutorBEN.Services;
using MiTutorBEN.Models;
using MiTutorBEN.ServicesImpl;
using MiTutorBEN.Data;
using Microsoft.EntityFrameworkCore;
using MiTutorBEN.Converters;

namespace MiTutorBEN.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class TutorsController : ControllerBase
    {
        private readonly ILogger<TutorsController> _logger;

        private readonly IUserService _userService;
        private readonly ITutorService _tutorService;

        private readonly UniversityConverter _universityConverter;
        public TutorsController(
            ILogger<TutorsController> logger,
            IUserService userService,
            ITutorService tutorService,
            UniversityConverter universityConverter
        )
        {
            _logger = logger;
            _userService = userService;
            _tutorService = tutorService;
            _universityConverter = universityConverter;
        }

        /// <summary>
        /// Find tutor by id
        /// </summary>
        /// <param name="id">The id of tutor</param>  

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Tutor>> GetTutor(int id)
        {
            var tutor = await _tutorService.FindById(id);

            if (tutor == null)
            {
                return NotFound();
            }

            return tutor;
        }


        [AllowAnonymous]
        [HttpGet("{id}/university")]
        public async Task<ActionResult<UniversityDTO>> GetTutorUniversity(long id)
        {
            var university = await _tutorService.FindUniversity(id);
            if (university == null)
            {
                return NotFound();
            }
            return _universityConverter.FromEntity(university);
        }






       
    }
}