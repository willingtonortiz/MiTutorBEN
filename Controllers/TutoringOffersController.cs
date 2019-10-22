using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiTutorBEN.DTOs.Requests;
using MiTutorBEN.Models;
using MiTutorBEN.Services;

namespace MiTutorBEN.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class TutoringOffersController : ControllerBase
    {
        private readonly ILogger<TutoringOffersController> _logger;

        private readonly IMapper _mapper;
        private readonly IUniversityService _universityService;

        private readonly ITutorService _tutorService;

        private readonly ICourseService _courseService;

        private readonly ITutoringOfferService _tutoringOfferService;

        private readonly ITutoringSessionService _tutoringSessionService;


        public TutoringOffersController(
            ILogger<TutoringOffersController> logger,
            IMapper mapper,
            IUniversityService universityService,
            ITutorService tutorService,
            ICourseService courseService,
            ITutoringOfferService tutoringOfferService,
            ITutoringSessionService tutoringSessionService
            )
        {
            _logger = logger;
            _mapper = mapper;
            _universityService = universityService;
            _tutorService = tutorService;
            _courseService = courseService;
            _tutoringOfferService = tutoringOfferService;
            _tutoringSessionService = tutoringSessionService;
        }



        [HttpPost]
        public ActionResult<TutoringOfferRequest> Create([FromBody] TutoringOfferRequest tutoringOfferRequest)
        {
            var TutoringOffer = _mapper.Map<TutoringOfferRequest, TutoringOffer>(tutoringOfferRequest);
            //TutoringOffer.University = _universityService.FindById(TutoringOffer.UniversityId);
           // TutoringOffer.Course = _courseService.FindById(TutoringOffer.CourseId);
           // TutoringOffer.Tutor = _tutorService.FindById(TutoringOffer.TutorId);

            _tutoringOfferService.Create(TutoringOffer);
            return Created($"", _mapper.Map<TutoringOffer, TutoringOfferRequest>(TutoringOffer));
         
        }
    }
}