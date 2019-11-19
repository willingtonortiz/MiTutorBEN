using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiTutorBEN.Converters;
using MiTutorBEN.DTOs.Requests;
using MiTutorBEN.DTOs.Responses;
using MiTutorBEN.Models;
using MiTutorBEN.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace MiTutorBEN.Controllers
{
    [ApiController]
	[AllowAnonymous]
	[Route("api/[controller]")]
    public class TutoringSessionsController: ControllerBase
    {
        #region Attributes
        private readonly ITutoringSessionService _tutoringSessionService; 
        private readonly TutoringSessionResponseConverter  _tutoringSessionResponseConverter;

        #endregion

        #region Constructor

        public TutoringSessionsController( ITutoringSessionService tutoringSessionService, 
            TutoringSessionResponseConverter tutoringSessionResponseConverter){
            _tutoringSessionService = tutoringSessionService;
            _tutoringSessionResponseConverter = tutoringSessionResponseConverter;
        }

        #endregion


        #region FindById
		/// <summary>
		/// Find tutorings sessions by id
		/// </summary>
		/// <remarks>
		/// Find tutorings sessions by id as a param variable 
		/// </remarks>
		/// <param name="tutoringId">The id of the tutoring session</param>
		/// <response code="200">Tutoring sessions found.</response>
		/// <response code="404">ITutoring session not found</response>
		/// <response code="500">Internal application error</response>
		[SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(TutoringSessionRequest))]
		[SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(string))]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
		[HttpGet("{tutoringId}")]
		public async Task<ActionResult<TutoringSessionResponse>> GetTutoring(int tutoringId)
		{
			TutoringSession TutoringSession = await _tutoringSessionService.FindById(tutoringId);

			if(TutoringSession!=null){
			TutoringSessionResponse TutoringSessionResponse = _tutoringSessionResponseConverter.FromEntity(TutoringSession);
			return Ok(TutoringSessionResponse);
			} else{
				return NotFound("Not found");
			}
		}

		#endregion

        





    }
}