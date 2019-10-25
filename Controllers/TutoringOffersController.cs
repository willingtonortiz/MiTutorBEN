using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
	public class TutoringOffersController : ControllerBase
	{
		#region Attributes

		private readonly ILogger<TutoringOffersController> _logger;
		private readonly IUniversityService _universityService;
		private readonly ITutorService _tutorService;
		private readonly ICourseService _courseService;
		private readonly ITutoringOfferService _tutoringOfferService;
		private readonly ITutoringSessionService _tutoringSessionService;
		private readonly ITopicService _topicService;
		private readonly ITopicTutoringSessionService _topicTutoringSessionService;
		private readonly ITopicTutoringOfferService _topicTutoringOfferService;
		private readonly TutoringOfferResponseConverter _tutoringOfferResponseConverter;
		private readonly TutoringOfferRequestConverter _tutoringOfferRequestConverter;
		private readonly TutoringSessionRequestConverter _tutoringSessionRequestConverter;

		#endregion


		#region Constructor

		public TutoringOffersController(
			ILogger<TutoringOffersController> logger,
			IUniversityService universityService,
			ITutorService tutorService,
			ICourseService courseService,
			ITutoringOfferService tutoringOfferService,
			ITutoringSessionService tutoringSessionService,
			ITopicService topicService,
			ITopicTutoringSessionService topicTutoringSessionService,
			ITopicTutoringOfferService topicTutoringOfferService,
			TutoringOfferResponseConverter tutoringOfferResponseConverter,
			TutoringOfferRequestConverter tutoringOfferRequestConverter,
			TutoringSessionRequestConverter tutoringSessionRequestConverter
			)
		{
			_logger = logger;
			_universityService = universityService;
			_tutorService = tutorService;
			_courseService = courseService;
			_tutoringOfferService = tutoringOfferService;
			_tutoringSessionService = tutoringSessionService;
			_topicService = topicService;
			_topicTutoringSessionService = topicTutoringSessionService;
			_topicTutoringOfferService = topicTutoringOfferService;
			_tutoringOfferResponseConverter = tutoringOfferResponseConverter;
			_tutoringOfferRequestConverter = tutoringOfferRequestConverter;
			_tutoringSessionRequestConverter = tutoringSessionRequestConverter;
		}

		#endregion


		#region Create
		/// <summary>
		/// Create a tutoring offer 
		/// </summary>
		/// <remarks>
		/// Create a tutoring from a request object
		/// </remarks>
	
		/// <response code="201">Tutoring offers created.</response>
		/// <response code="400">Invalid request</response>
		/// <response code="500">Internal application error</response>
		[SwaggerResponse((int)HttpStatusCode.Created, Type = typeof(TutoringOfferRequest))]
		[SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(TutoringSessionRequest))]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
		[HttpPost]
		public async System.Threading.Tasks.Task<ActionResult<TutoringOfferRequest>> Create([FromBody] TutoringOfferRequest tutoringOfferRequest)
		{
			var TutoringOffer = _tutoringOfferRequestConverter.FromDto(tutoringOfferRequest);

			DateTime StartTime = tutoringOfferRequest.TutoringSessionRequests.Min(x => x.StartTime);
			DateTime EndTime = tutoringOfferRequest.TutoringSessionRequests.Max(x => x.EndTime);

			TutoringOffer.StartTime = StartTime;
			TutoringOffer.EndTime = EndTime;

			TutoringOffer = _tutoringOfferService.Create(TutoringOffer).Result;

			TutoringSession TutoringSession = null;
			TopicTutoringSession TopicTutoringSession = null;
			TopicTutoringOffer TopicTutoringOffer = null;
			Topic Topic = null;
			HashSet<int> Topics = new HashSet<int>();


			//CREATE THE SESSIONS FOR THE OFFER
			foreach (var t in tutoringOfferRequest.TutoringSessionRequests)
			{
				TutoringSession = _tutoringSessionRequestConverter.FromDto(t);
				TutoringSession.TutoringOfferId = TutoringOffer.TutoringOfferId;

				TutoringSession = _tutoringSessionService.Create(TutoringSession).Result;

				foreach (var id in t.Topics)
				{
					//SAVE TOPIC TUTORING SESSION WITH HIS SERVICE 
					Topic = _topicService.FindById(id).Result;
					TopicTutoringSession = new TopicTutoringSession();

					TopicTutoringSession.TopicId = id;
					TopicTutoringSession.TutoringSessionId = TutoringSession.TutoringSessionId;

					await _topicTutoringSessionService.Create(TopicTutoringSession);


					//Save TTS IN TUTORING SESSION AND TOPIC

					TutoringSession.TopicTutoringSessions.Add(TopicTutoringSession);
					Topic.TopicTutoringSessions.Add(TopicTutoringSession);

					await _topicService.Update(Topic.TopicId, Topic);

					//SAVE TOPIC IN SET
					Topics.Add(Topic.TopicId);
				}

				await _tutoringSessionService.Update(TutoringSession.TutoringSessionId, TutoringSession);

				TutoringOffer.TutoringSessions.Add(TutoringSession);
			}

			TutoringOffer = await _tutoringOfferService.Update(TutoringOffer.TutoringOfferId, TutoringOffer);


			//OFFER SESSIONS
			foreach (var t in Topics)
			{
				Topic = _topicService.FindById(t).Result;
				TopicTutoringOffer = new TopicTutoringOffer();

				TopicTutoringOffer.TopicId = t;
				TopicTutoringOffer.TutoringOfferId = TutoringOffer.TutoringOfferId;

				await _topicTutoringOfferService.Create(TopicTutoringOffer);


				TutoringOffer.TopicTutoringOffers.Add(TopicTutoringOffer);
				Topic.TopicTutoringOffers.Add(TopicTutoringOffer);

				await _topicService.Update(Topic.TopicId, Topic);

			}

			await _tutoringOfferService.Update(TutoringOffer.TutoringOfferId, TutoringOffer);

			return Created($"", _tutoringOfferRequestConverter.FromEntity(TutoringOffer));
		}
		#endregion


		#region FindById
		/// <summary>
		/// Find tutorings sessions by id
		/// </summary>
		/// <remarks>
		/// Find tutorings sessions by id 
		/// </remarks>
		/// <param name="tutoringId">The id of the tutoring offer</param>
		/// <response code="200">Tutoring offers found.</response>
		/// <response code="404">ITutoring offer not found</response>
		/// <response code="500">Internal application error</response>
		[SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(TutoringOfferRequest))]
		[SwaggerResponse((int)HttpStatusCode.NotFound, Type = typeof(string))]
		[SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(string))]
		[HttpGet("{tutoringId}")]
		public async System.Threading.Tasks.Task<ActionResult<TutoringOfferResponse>> GetTutoring(int tutoringId)
		{
			TutoringOffer TutoringOffer = await _tutoringOfferService.FindById(tutoringId);

			if(TutoringOffer!=null){
			TutoringOfferResponse TutoringOfferResponse = _tutoringOfferResponseConverter.FromEntity(TutoringOffer);
			return Ok(TutoringOfferResponse);
			} else{
				return NotFound("Not found");
			}
		}

		#endregion

	}
}