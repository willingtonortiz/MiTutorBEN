using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiTutorBEN.Data;
using MiTutorBEN.Models;
using MiTutorBEN.Services;

namespace MiTutorBEN.ServicesImpl
{
	public class TutorCourseServiceImpl : ITutorCourseService
	{
		private readonly MiTutorContext _context;

		public TutorCourseServiceImpl(MiTutorContext context)
		{
			_context = context;
		}

		public async Task DeleteByTutorIdAndCourseIdAsync(TutorCourse tutorCourse)
		{
			_context.TutorCourses
				.Remove(tutorCourse);

			await _context.SaveChangesAsync();
			_context.Entry(tutorCourse).State = EntityState.Detached;
			return;
		}

		public async Task<TutorCourse> FindByTutorIdAndCourseIdAsync(int tutorId, int courseId)
		{
			TutorCourse tutorCourse = await _context.TutorCourses
                .AsNoTracking()
				.FirstOrDefaultAsync(x => x.TutorId == tutorId && x.CourseId == courseId);

			return tutorCourse;
		}
	}
}