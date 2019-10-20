using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTutorBEN.Data;
using MiTutorBEN.Models;
using MiTutorBEN.Services;

namespace MiTutorBEN.ServicesImpl
{
	public class TutorServiceImpl : ITutorService
	{
		private readonly MiTutorContext _context;

		public TutorServiceImpl(MiTutorContext context)
		{
			_context = context;
		}

		public async Task<Tutor> Create(Tutor t)
		{
			await _context.Tutors
				.AddAsync(t);

			await _context.SaveChangesAsync();
			return t;
		}

		public async Task DeleteAll()
		{
			IEnumerable<Tutor> tutors = _context.Tutors
				.AsNoTracking();

			_context.Tutors
				.RemoveRange(tutors);

			await _context.SaveChangesAsync();
		}

		public async Task<Tutor> DeleteById(int id)
		{
			Tutor found = await _context.Tutors
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.TutorId == id);

			if (found == null)
			{
				return null;
			}

			_context.Tutors
				.Remove(found);

			await _context.SaveChangesAsync();

			return found;
		}

		public async Task<IEnumerable<Tutor>> FindAll()
		{
			return await _context.Tutors
				.AsNoTracking().ToListAsync<Tutor>();
		}

		public async Task<Tutor> FindById(int id)
		{
			Tutor found = await _context.Tutors
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.TutorId == id);

			return found;
		}

		public async Task<Tutor> Update(int id, Tutor t)
		{
			Tutor found = await _context.Tutors
				.FirstOrDefaultAsync(x => x.TutorId == id);

			if (found == null)
			{
				return null;
			}

			_context.Tutors
				.Update(t);

			await _context.SaveChangesAsync();

			return found;
		}
	}
}