using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

		public async Task<Topic> Create(Topic t)
		{
			await _context.Topics
				.AddAsync(t);

			await _context.SaveChangesAsync();
			return t;
		}

		public async Task DeleteAll()
		{
			IEnumerable<Topic> topics = _context.Topics
				.AsNoTracking();

			_context.Topics
				.RemoveRange(topics);

			await _context.SaveChangesAsync();
		}

		public async Task<Topic> DeleteById(int id)
		{
			Topic found = await _context.Topics
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.TopicId == id);

			if (found == null)
			{
				return null;
			}

			_context.Topics
				.Remove(found);

			await _context.SaveChangesAsync();

			return found;
		}

		public async Task<IEnumerable<Topic>> FindAll()
		{
			return await _context.Topics
				.AsNoTracking().ToListAsync<Topic>();
		}

		public async Task<Topic> FindById(int id)
		{
			Topic found = await _context.Topics
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.TopicId == id);

			return found;
		}

		public async Task<Topic> Update(int id, Topic t)
		{
			Topic found = await _context.Topics
				.FirstOrDefaultAsync(x => x.TopicId == id);

			if (found == null)
			{
				return null;
			}

			_context.Topics
				.Update(t);

			await _context.SaveChangesAsync();

			return found;
		}
	}
}