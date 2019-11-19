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
    public class TutoringSessionStudentServiceImpl : ITutoringSessionStudentService
    {
        private readonly MiTutorContext _context;

        public TutoringSessionStudentServiceImpl(MiTutorContext context)
        {
            _context = context;
        }


        public async Task<StudentTutoringSession> Create(StudentTutoringSession t)
        {

            await _context.StudentTutoringSession
                 .AddAsync(t);
            await _context.SaveChangesAsync();

            return t;
        }

        public async Task <IEnumerable<StudentTutoringSession>> findSessionsByUserId(int userId){

            return await _context.StudentTutoringSession.AsNoTracking().Where(x=>x.StudentId == userId).ToListAsync();
            
        }
        public Task<StudentTutoringSession> FindById(int id)
        {
            return null;
        }
        public Task<IEnumerable<StudentTutoringSession>> FindAll()
        {
            return null;
        }
        public Task<StudentTutoringSession> Update(int id, StudentTutoringSession t)
        {
            return null;
        }
        public Task<StudentTutoringSession> DeleteById(int id)
        {
            return null;
        }
        public Task DeleteAll()
        {
            return null;
        }
    }

}