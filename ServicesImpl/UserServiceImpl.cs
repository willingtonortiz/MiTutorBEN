
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
            User personWithThisUsername = _context.Users.
            AsNoTracking().
            FirstOrDefault(x => x.Username == username);
            if (personWithThisUsername != null)
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

        public bool EmailValid(string email){
            User personWithThisEmail = _context.Users.AsNoTracking().
                                        FirstOrDefault(x=>x.Email == email);
            
            if (personWithThisEmail != null){
                _logger.LogWarning("El correo ya esta registrado");
                return true;
            }
            else{
                _logger.LogWarning("El correo no existe");
                return false;
            }

        }


       
    }
}