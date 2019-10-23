using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTutorBEN.Data;
using MiTutorBEN.Models;
using MiTutorBEN.Services;

namespace MiTutorBEN.ServicesImpl
{
    public class TopicTutoringOfferServiceImpl : ITopicTutoringOfferService
    {

        private readonly MiTutorContext _context;

		public TopicTutoringOfferServiceImpl(MiTutorContext context)
		{
			_context = context;
		}
        public async Task<TopicTutoringOffer> Create(TopicTutoringOffer t)
        {
            await _context.TopicTutoringOffers
				.AddAsync(t);

			await _context.SaveChangesAsync();
			return t;
        }

        public Task DeleteAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<TopicTutoringOffer> DeleteById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<TopicTutoringOffer>> FindAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<TopicTutoringOffer> FindById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<TopicTutoringOffer> Update(int id, TopicTutoringOffer t)
        {
            throw new System.NotImplementedException();
        }
    }

}
