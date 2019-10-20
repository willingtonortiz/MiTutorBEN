using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTutorBEN.Data;
using MiTutorBEN.Models;
using MiTutorBEN.Services;

namespace MiTutorBEN.ServicesImpl
{
	public class UniversityServiceImpl : IUniversityService
	{
		private readonly MiTutorContext _context;

		public UniversityServiceImpl(MiTutorContext context)
		{
			_context = context;
		}

		public async Task<University> Create(University t)
		{

			University found = await _context.Universities
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Name == t.Name);

			if (found != null)
			{
				return null;
			}

			await _context.Universities
				.AddAsync(t);
			await _context.SaveChangesAsync();

			return t;
		}

		public async Task<int> DeleteAll()
		{
			IEnumerable<University> universities = _context.Universities
				.AsNoTracking();

			_context.Universities
				.RemoveRange(universities);

			return await _context.SaveChangesAsync();
		}

		public async Task<University> DeleteById(int id)
		{
			University found = await _context.Universities
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.UniversityId == id);

			if (found == null)
			{
				return null;
			}

			_context.Universities
				.Remove(found);
			await _context.SaveChangesAsync();

			return found;
		}

		public async Task<IEnumerable<University>> FindAll()
		{
			return await _context.Universities
				.AsNoTracking().ToListAsync<University>();
		}

		public async Task<University> FindById(int id)
		{
			University found = await _context.Universities
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.UniversityId == id);

			return found;
		}

		public async Task<University> FindByName(string name)
		{
			University found = await _context.Universities
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Name == name);

			return found;
		}

		public async Task<University> Update(int id, University t)
		{
			University found = await _context.Universities
				.FirstOrDefaultAsync(x => x.UniversityId == id);

			_context.Universities
				.Update(t);

			await _context.SaveChangesAsync();

			return found;
		}
	}
}