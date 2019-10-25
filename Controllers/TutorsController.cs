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

namespace MiTutorBEN.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class TutorsController : ControllerBase
    {
        private readonly ILogger<TutorsController> _logger;


        private readonly MiTutorContext _context;

        private readonly IUserService _userService;
        private readonly ITutorService _tutorService;
        public TutorsController(
			MiTutorContext context,
			ILogger<TutorsController> logger, 
			IUserService userService, 
			ITutorService tutorService)
        {
            _logger = logger;
            _userService = userService;
            _tutorService = tutorService;
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Tutor>> GetTutor(long id)
        {
            var tutor = await _context.Tutors.FindAsync(id);

            if (tutor == null)
            {
                return NotFound();
            }

            return tutor;
        }




        [AllowAnonymous]
        [HttpPost]
        [Route("Subscription")]

        public async Task<ActionResult<TutorDTO>> Subscription([FromBody] MembershipDTO membershipDTO)
        {

            User foundUser = await _userService.FindById(membershipDTO.UserId);

            _logger.LogWarning(membershipDTO.CreditCard);

            if (foundUser == null)
            {
                return NotFound(new { message = "User not found" });
            }
            foundUser.Role = "tutor";

            _context.Entry(foundUser).State = EntityState.Modified;

            Tutor newTutor = new Tutor();
            newTutor.TutorId = foundUser.UserId;
            newTutor.QualificationCount = 0;
            newTutor.Points = 0.0;
            newTutor.Person = foundUser.Person;
            newTutor.Description = "Un nuevo tutor";
			newTutor.Status = "Avaliable";

            TutorDTO tutorResponse = new TutorDTO();
            tutorResponse.TutorId = foundUser.UserId;
            tutorResponse.QualificationCount = 0;
            tutorResponse.Points = 0.0;
            tutorResponse.Description = "Un nuevo tutor";
			tutorResponse.Status ="Avaliable";

            await _tutorService.Create(newTutor);
            await _context.SaveChangesAsync();



            return CreatedAtAction("GetTutor", new { id = tutorResponse.TutorId }, tutorResponse);


        }
    }
}