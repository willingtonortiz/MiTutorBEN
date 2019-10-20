using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTutorBEN.Data;
using MiTutorBEN.Models;
using MiTutorBEN.Services;

namespace MiTutorBEN.ServicesImpl
{
	public class TutoringOfferServiceImpl : ITutoringOfferService
	{
		private readonly MiTutorContext _context;

		public TutoringOfferServiceImpl(MiTutorContext context)
		{
			_context = context;
		}

		public async Task<TutoringOffer> Create(TutoringOffer t)
		{
			await _context.TutoringOffers
				.AddAsync(t);

			await _context.SaveChangesAsync();
			return t;
		}

		public async Task<TutoringOffer> DeleteById(int id)
		{
			TutoringOffer found = await _context.TutoringOffers
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.TutoringOfferId == id);

			if (found == null)
			{
				return null;
			}

			_context.TutoringOffers
				.Remove(found);

			await _context.SaveChangesAsync();

			return found;
		}

		public async Task<IEnumerable<TutoringOffer>> FindAll()
		{
			return await _context.TutoringOffers
				.AsNoTracking().ToListAsync<TutoringOffer>();
		}

		public async Task<TutoringOffer> FindById(int id)
		{
			TutoringOffer found = await _context.TutoringOffers
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.TutoringOfferId == id);

			return found;
		}

		public async Task<TutoringOffer> Update(int id, TutoringOffer t)
		{
			TutoringOffer found = await _context.TutoringOffers
				.FirstOrDefaultAsync(x => x.TutoringOfferId == id);

			if (found == null)
			{
				return null;
			}

			_context.TutoringOffers
				.Update(t);

			await _context.SaveChangesAsync();

			return found;
		}

		public async Task<int> DeleteAll()
		{
			IEnumerable<TutoringOffer> tutoringOffers = _context.TutoringOffers
				.AsNoTracking();

			_context.TutoringOffers
				.RemoveRange(tutoringOffers);

			return await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<TutoringOffer>> FindByUniversityIdAndCourseId(int universityId, int courseId)
		{
			IEnumerable<TutoringOffer> tutoringOffers = await _context.TutoringOffers
				.AsNoTracking()
				.Include(x => x.Course)
				.Include(x => x.Tutor)
					.ThenInclude(x => x.Person)
				.Where(x => x.UniversityId == universityId && x.CourseId == courseId)
				.ToListAsync<TutoringOffer>();

			return tutoringOffers;
		}
	}
}