using System.Collections.Generic;
using System.Linq;
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

		public Tutor Create(Tutor t)
		{
			_context.Tutors
				.Add(t);

			_context.SaveChanges();
			return t;
		}

		public void DeleteAll()
		{
			IEnumerable<Tutor> tutors = _context.Tutors
				.AsNoTracking();

			_context.Tutors
				.RemoveRange(tutors);

			_context.SaveChanges();
		}

		public Tutor DeleteById(int id)
		{
			Tutor found = _context.Tutors
				.AsNoTracking()
				.FirstOrDefault(x => x.TutorId == id);

			if (found == null)
			{
				return null;
			}

			_context.Tutors
				.Remove(found);

			_context.SaveChanges();

			return found;
		}

		public IEnumerable<Tutor> FindAll()
		{
			return _context.Tutors
				.AsNoTracking();
		}

		public Tutor FindById(int id)
		{
			Tutor found = _context.Tutors
				.AsNoTracking()
				.FirstOrDefault(x => x.TutorId == id);

			return found;
		}

		public Tutor Update(int id, Tutor t)
		{
			Tutor found = _context.Tutors
				.FirstOrDefault(x => x.TutorId == id);

			if (found == null)
			{
				return null;
			}

			_context.Tutors
				.Update(t);

			return found;
		}
	}
}