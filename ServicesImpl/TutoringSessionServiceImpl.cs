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

        public Task DeleteAll()
        {
            throw new NotImplementedException();
        }

        public Task<TutoringSession> DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TutoringSession>> FindAll()
        {
            throw new NotImplementedException();
        }

        public Task<TutoringSession> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<TutoringSession> Update(int id, TutoringSession t)
        {

            if (_context.TutoringSessions.Any(t => t.TutoringSessionId == id))
            {
                TutoringSession found = _context.TutoringSessions
                .Update(t).Entity;

                await _context.SaveChangesAsync();
                return found;
            }
            else
            {
                return null;
            }
        }
    }
}