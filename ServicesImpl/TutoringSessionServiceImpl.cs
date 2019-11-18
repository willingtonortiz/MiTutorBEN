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
	public class TutoringSessionServiceImpl : ITutoringSessionService
	{
		private readonly MiTutorContext _context;

		public TutoringSessionServiceImpl(MiTutorContext context)
		{
			_context = context;
		}

		public bool Any(Func<TutoringSession, bool> criteria)
		{
			throw new NotImplementedException();
		}

		public async Task<TutoringSession> Create(TutoringSession t)
		{
			await _context.TutoringSessions
			 .AddAsync(t);

			await _context.SaveChangesAsync();
			return t;
		}

		public async Task DeleteAll()
		{
			IEnumerable<TutoringSession> tutoringSessions = _context.TutoringSessions
				.AsNoTracking();

			foreach (var tutoringSession in tutoringSessions)
			{
				_context.Remove(tutoringSession);
			}

			await _context.SaveChangesAsync();
		}

		public Task<TutoringSession> DeleteById(int id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<TutoringSession>> FindAll()
		{
			throw new NotImplementedException();
		}

		public async Task<TutoringSession> FindById(int id)
		{
			TutoringSession found = await _context.TutoringSessions
			.AsNoTracking()
			.FirstOrDefaultAsync(x => x.TutoringSessionId == id);

			return found;
		}

		public async void SaveAsync()
		{
			await _context.SaveChangesAsync();
		}

		public async Task<TutoringSession> Update(int id, TutoringSession t)
		{

			TutoringSession found = await _context.TutoringSessions

				  .FirstOrDefaultAsync(x => x.TutoringSessionId == id);

			if (found == null)
			{
				return null;
			}

			_context.TutoringSessions
				.Update(t);

			await _context.SaveChangesAsync();

			return found;
		}


	}
}