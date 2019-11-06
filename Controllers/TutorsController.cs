using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DefaultNamespace;
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
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class TutorsController : ControllerBase
    {
        private readonly ILogger<TutorsController> _logger;
        private readonly IUserService _userService;
        private readonly ITutorService _tutorService;
        private readonly ITutoringOfferService _tutoringOfferService;
        private readonly TutoringOfferConverter _tutoringOfferConverter;
        private readonly UniversityConverter _universityConverter;
        private readonly TutorConverter _tutorConverter;

        #region Constructor

        public TutorsController(
            ILogger<TutorsController> logger,
            IUserService userService,
            ITutorService tutorService,
            ITutoringOfferService tutoringOfferService,
            TutoringOfferConverter tutoringOfferConverter,
            TutorConverter tutorConverter,
            UniversityConverter universityConverter
        )
        {
            _logger = logger;
            _userService = userService;
            _tutorService = tutorService;
            _tutoringOfferService = tutoringOfferService;
            _tutoringOfferConverter = tutoringOfferConverter;
            _tutorConverter = tutorConverter;
            _universityConverter = universityConverter;
        }

        #endregion

        /// <summary>
        /// Find tutor by id
        /// </summary>
        /// <param name="id">The id of tutor</param>  
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

        #region FindAll

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TutorDTO>>> FindAll()
        {
            IEnumerable<Tutor> tutors = await _tutorService.FindAll();

            _logger.LogError(tutors.Count().ToString());

            IEnumerable<TutorDTO> tutorDtos = tutors.Select(x => _tutorConverter.FromEntity(x));

            return Ok(tutorDtos);
        }

        #endregion


        #region FindTutoringOffersByTutorId

        [HttpGet("{tutorId}/tutoringoffers")]
        public async Task<ActionResult<IEnumerable<TutoringOfferInfo>>> FindTutoringOffersByTutorId(
            [FromRoute] int tutorId
        )
        {
            Tutor foundTutor = await _tutorService.FindById(tutorId);
            if (foundTutor == null)
            {
                return NotFound();
            }

            IEnumerable<TutoringOffer> tutoringOffers = await _tutoringOfferService
                .FindAllByTutorIdAsync(tutorId);

            IEnumerable<TutoringOfferInfo> tutoringOffersInfo = tutoringOffers
                .Select(x => _tutoringOfferConverter.TutoringOfferToTutoringOfferInfo(x));

            return Ok(tutoringOffersInfo);
        }

        #endregion
    }
}