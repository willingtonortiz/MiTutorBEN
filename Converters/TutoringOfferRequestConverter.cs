using MiTutorBEN.DTOs;
using MiTutorBEN.DTOs.Requests;
using MiTutorBEN.DTOs.Responses;
using MiTutorBEN.Models;
using MiTutorBEN.Services;

namespace MiTutorBEN.Converters
{


    public class TutoringOfferRequestConverter : IConverter<TutoringOffer, TutoringOfferRequest>
    {

        public TutoringSessionRequestConverter _tutoringSessionRequestConvert;
        public TutoringOfferRequestConverter(TutoringSessionRequestConverter tutoringSessionRequestConverter)
        {
            _tutoringSessionRequestConvert= tutoringSessionRequestConverter;
        }
        public TutoringOffer FromDto(TutoringOfferRequest dto)
        {
            TutoringOffer TutoringOffer = new TutoringOffer
            {
                Capacity = dto.Capacity,
                Description = dto.Description,
                CourseId = dto.CourseId,
                UniversityId = dto.UniversityId,
                TutorId = dto.TutorId
            };

            return TutoringOffer;
        }

        public TutoringOfferRequest FromEntity(TutoringOffer entity)
        {

            TutoringOfferRequest tutoringOfferRequest = new TutoringOfferRequest
            {
                UniversityId = entity.UniversityId,
                CourseId = entity.CourseId,
                TutorId = entity.TutorId,
                Capacity = entity.Capacity,
                Description = entity.Description,

            };

            foreach (var s in entity.TutoringSessions)
            {
                tutoringOfferRequest.TutoringSessionRequests.Add(_tutoringSessionRequestConvert.FromEntity(s));
            }

            return tutoringOfferRequest;
        }
    }
}