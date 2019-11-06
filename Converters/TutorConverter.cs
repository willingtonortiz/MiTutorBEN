using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
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
                TutorId = dto.Id,
                QualificationCount = dto.QualificationCount,
                Points = dto.Points,
                Description = dto.Description,
                Status = dto.Status
            };

            return tutor;
        }

        public TutorDTO FromEntity(Tutor entity)
        {
            TutorDTO tutorDTO = new TutorDTO
            {
                Id = entity.TutorId,
                QualificationCount = entity.QualificationCount,
                Points = entity.Points,
                Description = entity.Description,
                Status = entity.Status
            };

            return tutorDTO;
        }

        public TutorInfo TutorToTutorInfo(Tutor entity)
        {

            // TODO: Agregar el campo carrera a esta funcion
            return new TutorInfo
            {
                Id = entity.TutorId,
                Career = "TODO",
                Points = entity.Points,
                FullName = entity.Person.FullName
            };
        }
    }
}