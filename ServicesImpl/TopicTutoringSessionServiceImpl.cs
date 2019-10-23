using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTutorBEN.Data;
using MiTutorBEN.Models;
using MiTutorBEN.Services;

namespace MiTutorBEN.ServicesImpl
{
    public class TopicTutoringSessionServiceImpl : ITopicTutoringSessionService
    {

        private readonly MiTutorContext _context;

		public TopicTutoringSessionServiceImpl(MiTutorContext context)
		{
			_context = context;
		}
        public async Task<TopicTutoringSession> Create(TopicTutoringSession t)
        {
            await _context.TopicTutoringSessions
				.AddAsync(t);

			await _context.SaveChangesAsync();
			return t;
        }

        public Task DeleteAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<TopicTutoringSession> DeleteById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<TopicTutoringSession>> FindAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<TopicTutoringSession> FindById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<TopicTutoringSession> Update(int id, TopicTutoringSession t)
        {
            throw new System.NotImplementedException();
        }
    }

}