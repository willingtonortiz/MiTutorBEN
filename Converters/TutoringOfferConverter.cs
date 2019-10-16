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
				TutoringOfferId = dto.TutoringOfferId,
				StartTime = dto.StartTime,
				EndTime = dto.EndTime,
				Capacity = dto.Capacity,
				Description = dto.Description
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
	}
}