using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MiTutorBEN.Models;
using MiTutorBEN.Helpers;
using MiTutorBEN.DTOs;
using MiTutorBEN.Services;
using MiTutorBEN.Data;

namespace MiTutorBEN.ServicesImpl
{
    public class AuthServiceImpl : IAuthService
    {

        private readonly MiTutorContext _context;
        private readonly ILogger<AuthServiceImpl> _logger;
        private readonly AppSettings _appSettings;

        public AuthServiceImpl(MiTutorContext context, IOptions<AppSettings> appSettings, ILogger<AuthServiceImpl> logger)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _logger = logger;
        }

        public UserAuthDTO Authenticate(string username, string password)
        {
            User user = _context.Users
                .AsNoTracking()
                .SingleOrDefault(x => x.Username == username && x.Password == password);

            if (user == null)
                return null;

            UserAuthDTO authUser = new UserAuthDTO();
            authUser.UserId = user.UserId;
            authUser.Username = user.Username;
            authUser.Role = user.Role;

            // Agregando el token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            authUser.Token = tokenHandler.WriteToken(token);

            user.Password = null;
            return authUser;
        }

        public User RegisterUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();


            return user;
        }

        public async Task<User> Register(Person person, Student student, User user)
        {
            await _context.People.AddAsync(person);
            

            await _context.Users.AddAsync(user);
            
			
            await _context.Students.AddAsync(student);
            
			
			
			await _context.SaveChangesAsync();
            

            _logger.LogWarning(user.ToString());
            
            return user;


         
        }


    }
}