using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTutorBEN.Data;
using MiTutorBEN.Models;
using MiTutorBEN.Services;

namespace MiTutorBEN.ServicesImpl
{
    public class CourseServiceImpl : ICourseService
    {
        private readonly MiTutorContext _context;

        public CourseServiceImpl(MiTutorContext context)
        {
            _context = context;
        }

        public async Task<Course> Create(Course t)
        {
            await _context.Courses
                .AddAsync(t);

            await _context.SaveChangesAsync();

            return t;
        }

        public async Task DeleteAll()
        {
            IEnumerable<Course> courses = _context.Courses
                .AsNoTracking();

            _context.Courses
                .RemoveRange(courses);

            await _context.SaveChangesAsync();
        }

        public async Task<Course> DeleteById(int id)
        {
            Course found = await _context.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CourseId == id);

            if (found == null)
            {
                return null;
            }

            _context.Courses
                .Remove(found);

            await _context.SaveChangesAsync();

            return found;
        }

        public Task<bool> ExistsById(int courseId)
        {
            return _context.Courses
                .AnyAsync(x => x.CourseId == courseId);
        }

        public async Task<IEnumerable<Course>> FindAll()
        {
            return await _context.Courses
                .AsNoTracking().ToListAsync<Course>();
        }

        public async Task<Course> FindById(int id)
        {
            Course found = await _context.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CourseId == id);

            return found;
        }

        public async Task<IEnumerable<Course>> FindAllByUniversityId(int universityId)
        {
            List<Course> courses = await _context.Courses
                .AsNoTracking()
                .Where(x => x.UniversityId == universityId)
                .ToListAsync();

            return courses;
        }

        public async Task<Course> FindByUniversityIdAndCourseName(int universityId, string courseName)
        {
            Course found = await _context.Courses
                .FirstOrDefaultAsync(x => x.UniversityId == universityId && x.Name == courseName);

            return found;
        }

        public async Task<Course> Update(int id, Course t)
        {
            Course found = await _context.Courses
                .FirstOrDefaultAsync(x => x.CourseId == id);

            if (found == null)
            {
                return null;
            }

            _context.Courses
                .Update(t);

            await _context
                .SaveChangesAsync();

            return found;
        }

        public async Task<List<Topic>> FindTopics(int courseId)
        {
            List<Topic> topics = new List<Topic>();

            Course found = await _context.Courses.Include(c => c.Topics)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CourseId == courseId);

            foreach (var t in found.Topics)
            {
                topics.Add(t);
            }

            return topics;
        }

        public async Task<IEnumerable<Course>> FindAllByTutorIdAsync(int tutorId)
        {
            return await _context.Courses
                .AsNoTracking()
                .Where(x => x.TutorCourses.Any(y => y.TutorId == tutorId))
                .ToListAsync();
        }
    }
}