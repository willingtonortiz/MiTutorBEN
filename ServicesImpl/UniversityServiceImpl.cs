using System;
using System.Collections.Generic;
using System.Linq;
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

		public University Create(University t)
		{

			University found = _context.Universities
				.AsNoTracking()
				.FirstOrDefault(x => x.Name == t.Name);

			if (found != null)
			{
				return null;
			}

			_context.Universities
				.Add(t);
			_context.SaveChanges();

			return t;
		}

		public University DeleteById(int id)
		{
			University found = _context.Universities
				.AsNoTracking()
				.FirstOrDefault(x => x.UniversityId == id);

			if (found == null)
			{
				return null;
			}

			_context.Universities
				.Remove(found);
			_context.SaveChanges();

			return found;
		}

		public IEnumerable<University> FindAll()
		{
			return _context.Universities
				.AsNoTracking();
		}

		public University FindById(int id)
		{
			University found = _context.Universities
				.AsNoTracking()
				.FirstOrDefault(x => x.UniversityId == id);

			return found;
		}

		public University Update(University t)
		{
			int universityId = t.UniversityId;

			University found = _context.Universities
				.FirstOrDefault(x => x.UniversityId == universityId);

			found.Name = t.Name;

			_context.SaveChanges();

			return found;
		}
	}
}