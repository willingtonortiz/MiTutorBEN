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

		public TutoringOffer Create(TutoringOffer t)
		{
			_context.TutoringOffers
				.Add(t);

			_context.SaveChanges();
			return t;
		}

		public TutoringOffer DeleteById(int id)
		{
			TutoringOffer found = _context.TutoringOffers
				.AsNoTracking()
				.FirstOrDefault(x => x.TutoringOfferId == id);

			if (found == null)
			{
				return null;
			}

			_context.TutoringOffers
				.Remove(found);

			_context.SaveChanges();

			return found;
		}

		public IEnumerable<TutoringOffer> FindAll()
		{
			return _context.TutoringOffers
				.AsNoTracking();
		}

		public TutoringOffer FindById(int id)
		{
			TutoringOffer found = _context.TutoringOffers
				.AsNoTracking()
				.FirstOrDefault(x => x.TutoringOfferId == id);

			return found;
		}

		public TutoringOffer Update(int id, TutoringOffer t)
		{
			TutoringOffer found = _context.TutoringOffers
				.FirstOrDefault(x => x.TutoringOfferId == id);

			if (found == null)
			{
				return null;
			}

			_context.TutoringOffers
				.Update(t);

			return found;
		}

		public void DeleteAll()
		{
			IEnumerable<TutoringOffer> tutoringOffers = _context.TutoringOffers
				.AsNoTracking();

			_context.TutoringOffers
				.RemoveRange(tutoringOffers);

			_context.SaveChanges();
		}

		public async Task<IEnumerable<TutoringOffer>> FindByUniversityAndCourse(int universityId, int courseId)
		{
			IEnumerable<TutoringOffer> tutoringOffers = await _context.TutoringOffers
				.AsNoTracking()
				.Where(x => x.UniversityId == universityId && x.CourseId == courseId)
				.ToListAsync();

			return tutoringOffers;
		}
	}
}