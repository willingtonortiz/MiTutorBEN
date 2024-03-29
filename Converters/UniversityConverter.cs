using MiTutorBEN.DTOs;
using MiTutorBEN.Models;

namespace MiTutorBEN.Converters
{
	public class UniversityConverter : IConverter<University, UniversityDTO>
	{
		public University FromDto(UniversityDTO dto)
		{
			University university = new University
			{
				UniversityId = dto.Id,
				Name = dto.Name
			};

			return university;
		}

		public UniversityDTO FromEntity(University entity)
		{
			UniversityDTO universityDTO = new UniversityDTO
			{
				Id = entity.UniversityId,
				Name = entity.Name
			};

            return universityDTO;
		}
	}
}