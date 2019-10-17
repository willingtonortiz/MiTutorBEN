using MiTutorBEN.DTOs;
using MiTutorBEN.Models;

namespace MiTutorBEN.Converters
{
	public class TutorConverter : IConverter<Tutor, TutorDTO>
	{
		public Tutor FromDto(TutorDTO dto)
		{
			Tutor tutor = new Tutor
			{
				TutorId = dto.TutorId,
				QualificationCount = dto.QualificationCount,
				Points = dto.Points,
				Description = dto.Description
			};

			return tutor;
		}

		public TutorDTO FromEntity(Tutor entity)
		{
			TutorDTO tutorDTO = new TutorDTO
			{
                TutorId = entity.TutorId,
				QualificationCount = entity.QualificationCount,
				Points = entity.Points,
				Description = entity.Description
			};

			return tutorDTO;
		}
	}
}