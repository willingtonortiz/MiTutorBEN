using MiTutorBEN.DTOs;
using MiTutorBEN.Entities;

namespace MiTutorBEN.Converters
{
	public class UniversityConverter : IConverter<University, UniversityDTO>
	{
		public University FromDto(UniversityDTO dto)
		{
			University university = new University
			{
				UniversityId = dto.UniversityId,
				Name = dto.Name
			};

			return university;
		}

		public UniversityDTO FromEntity(University entity)
		{
			UniversityDTO universityDTO = new UniversityDTO
			{
				UniversityId = entity.UniversityId,
				Name = entity.Name
			};

            return universityDTO;
		}
	}
}