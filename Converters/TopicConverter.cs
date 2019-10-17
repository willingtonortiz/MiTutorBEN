using MiTutorBEN.DTOs;
using MiTutorBEN.Models;

namespace MiTutorBEN.Converters
{
	public class TopicConverter : IConverter<Topic, TopicDTO>
	{
		public Topic FromDto(TopicDTO dto)
		{
			Topic topic = new Topic
			{
				TopicId = dto.TopicId,
				Name = dto.Name
			};

			return topic;
		}

		public TopicDTO FromEntity(Topic entity)
		{
			TopicDTO topicDTO = new TopicDTO
			{
				TopicId = entity.TopicId,
				Name = entity.Name
			};

			return topicDTO;
		}
	}
}