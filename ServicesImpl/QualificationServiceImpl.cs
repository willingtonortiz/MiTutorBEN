using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MiTutorBEN.Data;
using MiTutorBEN.DTOs;
using MiTutorBEN.Enums;
using MiTutorBEN.Models;
using MiTutorBEN.Services;

namespace MiTutorBEN.ServicesImpl
{
    public class QualificationServiceImpl : IQualificationService
    {
        private readonly MiTutorContext _context;

        public QualificationServiceImpl(MiTutorContext context)
        {
            _context = context;
        }

        public async Task<Qualification> Create(Qualification t)
        {


            await _context.Qualifications
                .AddAsync(t);
            await _context.SaveChangesAsync();

            return t;
        }

        public async Task DeleteAll()
        {
            IEnumerable<Qualification> universities = _context.Qualifications
                .AsNoTracking();

            _context.Qualifications
                .RemoveRange(universities);

            await _context.SaveChangesAsync();
        }

        public async Task<Qualification> DeleteById(int id)
        {
            Qualification found = await _context.Qualifications
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.QualificationId == id);

            if (found == null)
            {
                return null;
            }

            _context.Qualifications
                .Remove(found);
            await _context.SaveChangesAsync();

            return found;
        }

        public async Task<IEnumerable<Qualification>> FindAll()
        {
            return await _context.Qualifications
                .AsNoTracking().ToListAsync<Qualification>();
        }

        public async Task<Qualification> FindById(int id)
        {
            Qualification found = await _context.Qualifications
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.QualificationId == id);

            return found;
        }

        public async Task<Qualification> Update(int id, Qualification t)
        {
            Qualification found = await _context.Qualifications
                .FirstOrDefaultAsync(x => x.QualificationId == id);

            _context.Qualifications
                .Update(t);

            await _context.SaveChangesAsync();

            return found;
        }

        public async Task<IEnumerable<Qualification>> FindAllQualificationsByTutor(int tutorId){
            return await _context.Qualifications.
            AsNoTracking().
            Where(x=>x.AdresseeId == tutorId).ToListAsync();
        }

    }


}