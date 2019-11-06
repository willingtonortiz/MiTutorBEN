using DefaultNamespace;
using MiTutorBEN.DTOs;
using MiTutorBEN.Models;

namespace MiTutorBEN.Converters
{
    public class TutoringOfferConverter : IConverter<TutoringOffer, TutoringOfferDTO>
    {
        public TutoringOffer FromDto(TutoringOfferDTO dto)
        {
            TutoringOffer tutoringOffer = new TutoringOffer
            {
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Capacity = dto.Capacity,
                Description = dto.Description,
                TutorId = dto.TutorId
            };

            return tutoringOffer;
        }

        public TutoringOfferDTO FromEntity(TutoringOffer entity)
        {
            TutoringOfferDTO tutoringOfferDTO = new TutoringOfferDTO
            {
                TutoringOfferId = entity.TutoringOfferId,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                Capacity = entity.Capacity,
                Description = entity.Description
            };

            return tutoringOfferDTO;
        }

        public TutoringOfferInfo TutoringOfferToTutoringOfferInfo(TutoringOffer entity)
        {
            return new TutoringOfferInfo
            {
                Id = entity.TutoringOfferId,
                CourseName = entity.Course.Name,
                TutorName = entity.Tutor.Person.FullName,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime
            };
        }
    }
}