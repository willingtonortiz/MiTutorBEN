using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTutorBEN.Data;
using MiTutorBEN.Models;
using MiTutorBEN.Services;

namespace MiTutorBEN.ServicesImpl
{
	public class PersonServiceImpl : IPersonService
	{
		private readonly MiTutorContext _context;

		public PersonServiceImpl(MiTutorContext context)
		{
			_context = context;
		}

		public async Task<Person> Create(Person t)
		{
			await _context.People
				.AddAsync(t);

			await _context.SaveChangesAsync();
			return t;
		}

		public async Task<int> DeleteAll()
		{
			IEnumerable<Person> people = _context.People
				.AsNoTracking();

			_context.People
				.RemoveRange(people);

			return await _context.SaveChangesAsync();
		}

		public async Task<Person> DeleteById(int id)
		{
			Person found = await _context.People
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.PersonId == id);

			if (found == null)
			{
				return null;
			}

			_context.People
				.Remove(found);

			await _context.SaveChangesAsync();

			return found;
		}

		public async Task<IEnumerable<Person>> FindAll()
		{
			return await _context.People
				.AsNoTracking().ToListAsync();
		}

		public async Task<Person> FindById(int id)
		{
			Person found = await _context.People
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.PersonId == id);

			return found;
		}

		public async Task<Person> Update(int id, Person t)
		{
			Person found = await _context.People
				.FirstOrDefaultAsync(x => x.PersonId == id);

			if (found == null)
			{
				return null;
			}

			_context.People
				.Update(t);

			await _context.SaveChangesAsync();

			return found;
		}
	}
}