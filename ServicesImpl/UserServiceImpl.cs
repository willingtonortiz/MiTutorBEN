
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

		public async Task<User> Create(User t)
		{
			await _context.Users
				.AddAsync(t);

			await _context.SaveChangesAsync();

			return t;
		}

		public async Task<bool> UserNameValid(string username)
		{
			User personWithThisUsername = await _context.Users.
			AsNoTracking().
			FirstOrDefaultAsync(x => x.Username == username);

		

			if (personWithThisUsername != null)
			{

				return true;
			}
			else
			{

				return false;
			}
		}

		public async Task<bool> EmailValid(string email)
		{
			User personWithThisEmail = await _context.Users.AsNoTracking().
										FirstOrDefaultAsync(x => x.Email == email);

			if (personWithThisEmail != null)
			{
				return true;
			}
			else
			{
				return false;
			}

		}

		public async Task DeleteAll()
		{
			IEnumerable<User> users = _context.Users
				.AsNoTracking();

			_context.Users
				.RemoveRange(users);

			await _context.SaveChangesAsync();
		}

		public async Task<User> DeleteById(int id)
		{
			User found = await _context.Users
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.UserId == id);

			if (found == null)
			{
				return null;
			}

			_context.Users
				.Remove(found);
			await _context.SaveChangesAsync();

			return found;
		}

		public async Task<IEnumerable<User>> FindAll()
		{
			return await _context.Users
				.AsNoTracking().ToListAsync<User>();
		}

		public async Task<User> FindById(int id)
		{
			User found = await _context.Users
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.UserId == id);

			return found;
		}

		public async Task<User> Update(int id, User t)
		{
			User found = await _context.Users
				.FirstOrDefaultAsync(x => x.UserId == id);

			if (found == null)
			{
				return null;
			}

			_context.Users.Update(t);

			await _context.SaveChangesAsync();

			return found;
		}
	}
}