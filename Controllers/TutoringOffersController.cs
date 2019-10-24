using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiTutorBEN.Converters;
using MiTutorBEN.DTOs.Requests;
using MiTutorBEN.DTOs.Responses;
using MiTutorBEN.Models;
using MiTutorBEN.Services;

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
			TutoringOfferRequestConverter tutoringOfferRequestConverter
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
		}

		#endregion


		#region Create

		[HttpPost("{tutorId}")]
		public async System.Threading.Tasks.Task<ActionResult<TutoringOfferRequest>> Create([FromBody] TutoringOfferRequest tutoringOfferRequest, int tutorId)
		{
			var TutoringOffer = _tutoringOfferRequestConverter.FromDto(tutoringOfferRequest);

			DateTime StartTime = tutoringOfferRequest.TutoringSessionRequests.Min(x => x.StartTime);
			DateTime EndTime = tutoringOfferRequest.TutoringSessionRequests.Max(x => x.EndTime);

			TutoringOffer.TutorId = tutorId;

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
				TutoringSession = new TutoringSession();
				TutoringSession.Description = t.Description;
				TutoringSession.EndTime = t.EndTime;
				TutoringSession.Place = t.Place;
				TutoringSession.Price = t.Price;
				TutoringSession.StartTime = t.StartTime;
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

		[HttpGet("{tutoringId}")]
		public async System.Threading.Tasks.Task<ActionResult<TutoringOfferResponse>> GetTutoring(int tutoringId)
		{
			TutoringOffer TutoringOffer = await _tutoringOfferService.FindById(tutoringId);
			Tutor t = TutoringOffer.Tutor;
			TutoringOfferResponse TutoringOfferResponse = _tutoringOfferResponseConverter.FromEntity(TutoringOffer);
			return TutoringOfferResponse;
		}

		#endregion

	}
}