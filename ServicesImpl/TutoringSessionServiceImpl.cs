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

        public TutoringSession Create(TutoringSession t)
        {
           	_context.TutoringSessions
				.Add(t);

			_context.SaveChanges();
			return t;
        }

        public TutoringSession DeleteById(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TutoringSession> FindAll()
        {
           return _context.TutoringSessions
				.AsNoTracking();
        }

        public TutoringSession FindById(int id)
        {
           TutoringSession found = _context.TutoringSessions
				.AsNoTracking()
				.FirstOrDefault(x => x.TutoringOfferId == id);

			return found;
        }

        public TutoringSession Update(int id, TutoringSession t)
        {
            throw new System.NotImplementedException();
        }

        public bool Any(Func<TutoringSession,bool> criteria)
        {
            return _context.TutoringSessions.Any(criteria);
        }
    }
}