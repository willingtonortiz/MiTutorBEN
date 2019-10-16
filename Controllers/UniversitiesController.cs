using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiTutorBEN.Converters;
using MiTutorBEN.DTOs;
using MiTutorBEN.Models;
using MiTutorBEN.Services;

namespace MiTutorBEN.Controllers
{
	[ApiController]
	[AllowAnonymous]
	[Route("[controller]")]
	public class UniversitiesController : ControllerBase
	{
		private readonly IUniversityService _universityService;
		private readonly UniversityConverter _universityConverter;

		public UniversitiesController(IUniversityService universityService, UniversityConverter universityConverter)
		{
			_universityService = universityService;
			_universityConverter = universityConverter;
		}

		[HttpGet]
		public ActionResult<IEnumerable<UniversityDTO>> FindAll()
		{
			IEnumerable<University> universities = _universityService.FindAll();

			return universities.Select(x => _universityConverter.FromEntity(x)).ToList();
		}

		[HttpGet("{universityId}")]
		public ActionResult<UniversityDTO> FindById(int universityId)
		{
			University university = _universityService.FindById(universityId);

			if (university == null)
			{
				return NotFound(new { message = "No se encontró la universidad" });
			}

			return _universityConverter.FromEntity(university);
		}

		[HttpPost]
		public ActionResult<UniversityDTO> Create([FromBody] UniversityDTO universityDTO)
		{
			University university = _universityConverter.FromDto(universityDTO);

			University created = _universityService.Create(university);

			if (created == null)
			{
				return Created("", new { message = "Ya existe una universidad con ese nombre" });
			}
			return Created($"", _universityConverter.FromEntity(created));

			// return _universityConverter.FromEntity(created);
		}

		[HttpDelete("{universityId}")]
		public ActionResult<UniversityDTO> Delete(int universityId)
		{
			University deleted = _universityService.DeleteById(universityId);

			if (deleted == null)
			{
				return NotFound(new { message = "No se encontró la universidad" });
			}

			return _universityConverter.FromEntity(deleted);
		}
	}
}