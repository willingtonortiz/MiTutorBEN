using MiTutorBEN.DTOs;
using MiTutorBEN.DTOs.Responses;
using MiTutorBEN.Models;

namespace MiTutorBEN.Converters
{
    public class TutoringSessionResponseConverter : IConverter<TutoringSession, TutoringSessionResponse>
    {   

        public TutoringSession FromDto(TutoringSessionResponse dto)
        {
            TutoringSession tutoringSession = new TutoringSession
            {
              
            };

            return tutoringSession;
        }

        public TutoringSessionResponse FromEntity(TutoringSession entity)
        {

            TutoringSessionResponse tutoringSessionResponse = new TutoringSessionResponse
            {
                TutoringSessionId = entity.TutoringSessionId,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                StudentCount = entity.StudentCount,
                Description = entity.Description,
                Price = entity.Price
        
            };

            foreach (var t in entity.TopicTutoringSessions)
            {
                tutoringSessionResponse.Topics.Add(t.Topic.Name);
            }

            return tutoringSessionResponse;
        }

    }
}