using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MiTutorBEN.Data;
using MiTutorBEN.Enums;
using MiTutorBEN.Models;
using MiTutorBEN.Services;

namespace MiTutorBEN.ServicesImpl
{
	public class TutorServiceImpl : ITutorService
	{
		private readonly MiTutorContext _context;
		private readonly ILogger<TutorServiceImpl> _logger;

		#region Constructor
		public TutorServiceImpl(
			MiTutorContext context,
			ILogger<TutorServiceImpl> logger
			)
		{
			_context = context;
			_logger = logger;
		}

		#endregion


		#region Create

		public async Task<Tutor> Create(Tutor t)
		{
			await _context.Tutors
				.AddAsync(t);

			await _context.SaveChangesAsync();
			return t;
		}

		#endregion


		#region DeleteAll

		public async Task DeleteAll()
		{
			IEnumerable<Tutor> tutors = _context.Tutors
				.AsNoTracking();

			_context.Tutors
				.RemoveRange(tutors);

			await _context.SaveChangesAsync();
		}

		#endregion


		#region DeleteById

		public async Task<Tutor> DeleteById(int id)
		{
			Tutor found = await _context.Tutors
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.TutorId == id);

			if (found == null)
			{
				return null;
			}

			_context.Tutors
				.Remove(found);

			await _context.SaveChangesAsync();

			return found;
		}

		#endregion


		#region FindAll

		public async Task<IEnumerable<Tutor>> FindAll()
		{
			return await _context.Tutors
				.AsNoTracking().ToListAsync<Tutor>();
		}

		#endregion


		#region FindAllByUniversityIdAndCouseId

		public async Task<IEnumerable<Tutor>> FindAllByUniversityIdAndCourseId(int universityId, int courseId)
		{
			return await _context.Tutors
				.AsNoTracking()
				.Where(x =>
					x.Person.UniversityId == universityId
					&& x.TutorCourses.Any(y => y.CourseId == courseId)
					&& x.Status == TutorStatus.AVAILABLE
				)
				.ToListAsync();
		}

		#endregion


		#region FindById
		public async Task<Tutor> FindById(int id)
		{
			Tutor found = await _context.Tutors
				//.AsNoTracking()
				.FirstOrDefaultAsync(x => x.TutorId == id);

			return found;
		}

		#endregion


		#region Udpate

		public async Task<Tutor> Update(int id, Tutor t)
		{
			Tutor found = await _context.Tutors
				.FirstOrDefaultAsync(x => x.TutorId == id);

			if (found == null)
			{
				return null;
			}

			_context.Tutors
				.Update(t);

			await _context.SaveChangesAsync();

			return found;
		}

		#endregion
	}
}