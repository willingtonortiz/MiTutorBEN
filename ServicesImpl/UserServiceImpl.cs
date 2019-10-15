
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiTutorBEN.Models;
using MiTutorBEN.Helpers;
using MiTutorBEN.Services;
using MiTutorBEN.Data;

namespace MiTutorBEN.ServicesImpl
{
    public class UserServiceImpl : IUserService
    {
        private readonly MiTutorContext _context;
        private readonly ILogger<UserServiceImpl> _logger;
        private readonly AppSettings _appSettings;

        public UserServiceImpl(MiTutorContext context, IOptions<AppSettings> appSettings, ILogger<UserServiceImpl> logger)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _logger = logger;
        }

        public bool UserNameValid(string username)
        {
            User personWithThisEmail = _context.Users.
            AsNoTracking().
            FirstOrDefault(x => x.Username == username);
            if (personWithThisEmail != null)
            {
                _logger.LogWarning("Si existe");
                return true;
            }
            else
            {
                _logger.LogWarning("No existe");
                return false;
            }
        }
    }
}