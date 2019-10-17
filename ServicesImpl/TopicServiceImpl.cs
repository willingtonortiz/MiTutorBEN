using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MiTutorBEN.Data;
using MiTutorBEN.Models;
using MiTutorBEN.Services;

namespace MiTutorBEN.ServicesImpl
{
	public class TopicServiceImpl : ITopicService
	{
		private readonly MiTutorContext _context;

		public TopicServiceImpl(MiTutorContext context)
		{
			_context = context;
		}

		public Topic Create(Topic t)
		{
			_context.Topics
				.Add(t);

			_context.SaveChanges();
			return t;
		}

		public void DeleteAll()
		{
			IEnumerable<Topic> topics = _context.Topics
				.AsNoTracking();

			_context.Topics
				.RemoveRange(topics);

			_context.SaveChanges();
		}

		public Topic DeleteById(int id)
		{
			Topic found = _context.Topics
				.AsNoTracking()
				.FirstOrDefault(x => x.TopicId == id);

			if (found == null)
			{
				return null;
			}

			_context.Topics
				.Remove(found);

			_context.SaveChanges();

			return found;
		}

		public IEnumerable<Topic> FindAll()
		{
			return _context.Topics
				.AsNoTracking();
		}

		public Topic FindById(int id)
		{
			Topic found = _context.Topics
				.AsNoTracking()
				.FirstOrDefault(x => x.TopicId == id);

			return found;
		}

		public Topic Update(int id, Topic t)
		{
			Topic found = _context.Topics
				.FirstOrDefault(x => x.TopicId == id);

			if (found == null)
			{
				return null;
			}

			_context.Topics
				.Update(t);

			return found;
		}
	}
}