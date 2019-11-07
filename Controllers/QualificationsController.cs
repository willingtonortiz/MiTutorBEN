using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DefaultNamespace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MiTutorBEN.Converters;
using MiTutorBEN.DTOs;
using MiTutorBEN.DTOs.Responses;
using MiTutorBEN.Models;
using MiTutorBEN.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace MiTutorBEN.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class QualificationsController : ControllerBase
    {


        private readonly IQualificationService _qualificationService;
        private readonly IPersonService _personService;

        public QualificationsController(
            IQualificationService qualificationService,
             IPersonService personService
        )
        {
            _qualificationService = qualificationService;
            _personService = personService;
        }


        [HttpGet("tutors/{tutorId}")]

        public async Task<ActionResult<List<QualificationsResponse>>> FindAllQualificationsByTutor(
            [FromRoute] int tutorId
        ){


            IEnumerable<Qualification> allQualifications =  await _qualificationService.FindAllQualificationsByTutor(tutorId);
            List<QualificationsResponse> listResponse =  new List<QualificationsResponse>();
            foreach (var item in allQualifications)
            {
                QualificationsResponse newQualification =  new QualificationsResponse();
                newQualification.Comment = item.Comment;
                newQualification.FullName =  _personService.FindById(item.AdresserId).Result.FullName;
                newQualification.rate = item.Rate;

                listResponse.Add(newQualification);
            }
            
            return Ok(listResponse);
        }



    }
}