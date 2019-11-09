using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MiTutorBEN.Converters;
using MiTutorBEN.DTOs;
using MiTutorBEN.DTOs.Input;
using MiTutorBEN.DTOs.Responses;
using MiTutorBEN.Models;
using MiTutorBEN.Services;
using Swashbuckle.AspNetCore.Annotations;


namespace MiTutorBEN.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TutoringSessionStudentsController : ControllerBase
    {


        private readonly ITutoringSessionStudentService _tutoringSessionStudentService;

        private readonly IStudentService _studentService;

        private readonly ILogger<TutoringSessionStudentsController> _logger;


        private readonly ITutoringSessionService _tutoringSessionService;
        public TutoringSessionStudentsController(
            ITutoringSessionStudentService tutoringSessionStudentService,
            IStudentService studentService,
            ITutoringSessionService tutoringSessionService,
            ILogger<TutoringSessionStudentsController> logger
        )
        {
            _tutoringSessionStudentService = tutoringSessionStudentService;
            _studentService = studentService;
            _tutoringSessionService = tutoringSessionService;
            _logger = logger;
        }

        /// <summary>
        /// Create a register of user in a tutoring session
        /// </summary>
        /// <remarks>
        /// Create a register of user in a tutoring session
        /// </remarks>
        /// <param name="createTutoringSessionStudent">The student id and tutoring session id</param>
        /// <response code="200">Create register successfully</response>
        /// <response code="404">Student or tutoring session not found</response>
        /// <response code="500">Internal application error</response>
        [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(TutoringSessionStudentResponse))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(string))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(201)]
        [Produces("application/json")]
        public async Task<ActionResult<TutoringSessionStudentResponse>> ReserverTutoring([FromBody] CreateTutoringSessionStudent createTutoringSessionStudent)
        {


            Student studentFound = await _studentService.FindById(createTutoringSessionStudent.StudentId);
            TutoringSession tutoringSessionFound = await _tutoringSessionService.FindById(createTutoringSessionStudent.TutoringSessionId);

            if (studentFound == null)
            {
                return NotFound(new { messsage = "No se ha encontrado el estudiante" });
            }
            if (tutoringSessionFound == null)
            {
                return NotFound(new { messsage = "No se ha encontrado el tutoring Session" });
            }



            StudentTutoringSession newTutoringSessionStudent = new StudentTutoringSession();

            newTutoringSessionStudent.StudentId = createTutoringSessionStudent.StudentId;
            newTutoringSessionStudent.TutoringSessionId = createTutoringSessionStudent.TutoringSessionId;





            StudentTutoringSession created = await _tutoringSessionStudentService.Create(newTutoringSessionStudent);
            TutoringSessionStudentResponse response = new TutoringSessionStudentResponse();
            response.StudentId = created.StudentId;
            response.TutoringSessionId = created.TutoringSessionId;

            return Ok(response);

        }
    }
}