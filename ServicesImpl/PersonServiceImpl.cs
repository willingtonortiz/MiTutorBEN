using System.Collections.Generic;
using System.Linq;
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

		public Person Create(Person t)
		{
			_context.People
				.Add(t);

			_context.SaveChanges();
			return t;
		}

		public Person DeleteById(int id)
		{
			Person found = _context.People
				.AsNoTracking()
				.FirstOrDefault(x => x.PersonId == id);

			if (found == null)
			{
				return null;
			}

			_context.People
				.Remove(found);

			_context.SaveChanges();

			return found;
		}

		public IEnumerable<Person> FindAll()
		{
			return _context.People
				.AsNoTracking();
		}

		public Person FindById(int id)
		{
			Person found = _context.People
				.AsNoTracking()
				.FirstOrDefault(x => x.PersonId == id);

			return found;
		}

		public Person Update(int id, Person t)
		{
			Person found = _context.People
				.FirstOrDefault(x => x.PersonId == id);

			if (found == null)
			{
				return null;
			}

			_context.People
				.Update(t);

			return found;
		}
	}
}