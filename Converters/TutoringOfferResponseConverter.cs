using MiTutorBEN.DTOs;
using MiTutorBEN.DTOs.Responses;
using MiTutorBEN.Models;

namespace MiTutorBEN.Converters
{
	public class TutoringOfferResponseConverter : IConverter<TutoringOffer, TutoringOfferResponse>
	{
        private readonly TutoringSessionResponseConverter _tutoringSessionResponseConverter;

        public TutoringOfferResponseConverter (TutoringSessionResponseConverter tutoringSessionResponseConverter)
        {
            _tutoringSessionResponseConverter = tutoringSessionResponseConverter;
        }

		public TutoringOffer FromDto(TutoringOfferResponse dto)
		{
			TutoringOffer tutoringOffer = new TutoringOffer
			{
				TutoringOfferId = dto.TutoringOfferId,
				StartTime = dto.StartTime,
				EndTime = dto.EndTime,
				Capacity = dto.Capacity,
				Description = dto.Description
				
			};

			return tutoringOffer;
		}

		public TutoringOfferResponse FromEntity(TutoringOffer entity)
		{
            Tutor tutor = entity.Tutor;
			TutoringOfferResponse tutoringOfferResponse = new TutoringOfferResponse
			{
				TutoringOfferId = entity.TutoringOfferId,
				StartTime = entity.StartTime,
				EndTime = entity.EndTime,
				Capacity = entity.Capacity,
				Description = entity.Description,
                Course = entity.Course.Name,
                Tutor = entity.Tutor.Person.FullName,
                University = entity.University.Name,
				TutorId = entity.TutorId
			};

            foreach(var topic in entity.TopicTutoringOffers)
            {
                tutoringOfferResponse.Topics.Add(topic.Topic.Name);
            }

            foreach(var session in entity.TutoringSessions)
            {
                tutoringOfferResponse.Sessions.Add(_tutoringSessionResponseConverter
                .FromEntity(session));
            }


			return tutoringOfferResponse;
		}

        

    }
}