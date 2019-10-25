using MiTutorBEN.DTOs;
using MiTutorBEN.DTOs.Requests;
using MiTutorBEN.DTOs.Responses;
using MiTutorBEN.Models;

namespace MiTutorBEN.Converters
{
    public class TutoringSessionRequestConverter : IConverter<TutoringSession, TutoringSessionRequest>
    {
        public TutoringSession FromDto(TutoringSessionRequest dto)
        {
            TutoringSession TutoringSession = new TutoringSession();
			TutoringSession.Description = dto.Description;
			TutoringSession.EndTime = dto.EndTime;
			TutoringSession.Place = dto.Place;
			TutoringSession.Price = dto.Price;
			TutoringSession.StartTime = dto.StartTime;

            return TutoringSession;
		
        }

        public TutoringSessionRequest FromEntity(TutoringSession entity)
        {
            throw new System.NotImplementedException();
        }
    }

}
