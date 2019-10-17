using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MiTutorBEN.Data;
using MiTutorBEN.Models;
using MiTutorBEN.Services;

namespace MiTutorBEN.ServicesImpl
{
	public class UserServiceImpl : IUserService
	{
		private readonly MiTutorContext _context;


		public UserServiceImpl(MiTutorContext context)
		{
			_context = context;
		}

		public User Create(User t)
		{
			_context.Users
				.Add(t);

			return t;
		}

		public void DeleteAll()
		{
			IEnumerable<User> users = _context.Users
				.AsNoTracking();

			_context.Users
				.RemoveRange(users);

			_context.SaveChanges();
		}

		public User DeleteById(int id)
		{
			User found = _context.Users
				.AsNoTracking()
				.FirstOrDefault(x => x.UserId == id);

			if (found == null)
			{
				return null;
			}

			_context.Users
				.Remove(found);
			_context.SaveChanges();

			return found;
		}

		public IEnumerable<User> FindAll()
		{
			return _context.Users
				.AsNoTracking();
		}

		public User FindById(int id)
		{
			User found = _context.Users
				.AsNoTracking()
				.FirstOrDefault(x => x.UserId == id);

			return found;
		}

		public User Update(int id, User t)
		{
			User found = _context.Users
				.FirstOrDefault(x => x.UserId == id);

			if (found == null)
			{
				return null;
			}

			_context.Users.Update(t);

			return found;
		}
	}
}