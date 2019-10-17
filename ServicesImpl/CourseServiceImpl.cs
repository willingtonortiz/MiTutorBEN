using System.Collections.Generic;
using System.Linq;
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

		public Course Create(Course t)
		{
			_context.Courses
				.Add(t);

			_context.SaveChanges();
			return t;
		}

		public void DeleteAll()
		{
			IEnumerable<Course> courses = _context.Courses
				.AsNoTracking();

			_context.Courses
				.RemoveRange(courses);

			_context.SaveChanges();
		}

		public Course DeleteById(int id)
		{
			Course found = _context.Courses
				.AsNoTracking()
				.FirstOrDefault(x => x.CourseId == id);

			if (found == null)
			{
				return null;
			}

			_context.Courses
				.Remove(found);

			_context.SaveChanges();

			return found;
		}

		public IEnumerable<Course> FindAll()
		{
			return _context.Courses
				.AsNoTracking();
		}

		public Course FindById(int id)
		{
			Course found = _context.Courses
				.AsNoTracking()
				.FirstOrDefault(x => x.CourseId == id);

			return found;
		}

		public Course Update(int id, Course t)
		{
			Course found = _context.Courses
				.FirstOrDefault(x => x.CourseId == id);

			if (found == null)
			{
				return null;
			}

			_context.Courses
				.Update(t);

			return found;
		}
	}
}