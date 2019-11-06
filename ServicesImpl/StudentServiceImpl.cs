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
    public class StudentServiceImpl : IStudentService
    {

        private readonly MiTutorContext _context;

        public StudentServiceImpl(MiTutorContext context)
        {
            _context = context;
        }

        public async Task<Student> FindById(int id)
        {
            Student found = await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.StudentId == id);

            return found;
        }
        public async Task<IEnumerable<Student>> FindAll()
        {
            return await _context.Students.AsNoTracking().ToListAsync<Student>();
        }
        public async Task<Student> Create(Student t)
        {
            await _context.Students.AddAsync(t);
            await _context.SaveChangesAsync();

            return t;
        }
        public async Task<Student> Update(int id, Student t)
        {
            Student found = await _context.Students
                 .FirstOrDefaultAsync(x => x.StudentId == id);

            _context.Students
                .Update(t);

            await _context.SaveChangesAsync();

            return found;
        }
        public async Task<Student> DeleteById(int id)
        {
            Student found = await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.StudentId == id);

            if (found == null)
            {
                return null;
            }

            _context.Students
                .Remove(found);
            await _context.SaveChangesAsync();

            return found;
        }
        public async Task DeleteAll()
        {
            IEnumerable<Student> students = _context.Students
            .AsNoTracking();

            _context.Students
                .RemoveRange(students);

            await _context.SaveChangesAsync();
        }
    }

}