using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DAL;

namespace BLL
{
    public class AuthenticationService
    {
        private readonly JournalDbContext _context;

        public AuthenticationService(JournalDbContext context)
        {
            _context = context;
        }

        public string AuthenticateUser(string username, string password)
        {
            // Генеруємо SHA256 хеш паролю
            var hashedPassword = GenerateSha256Hash(password);

            // Пошук користувача з допомогою Entity Framework Core
            var user = _context.Users
                .SingleOrDefault(u => u.Username == username && u.Password == hashedPassword);

            // не знайдений = null
            if (user == null)
            {
                return null!;
            }

            // roll_id => page
            return user.RoleId switch
            {
                1 => "StudentPage", 
                2 => "TeacherPage", 
                3 => "AdminPage",   
                _ => "Invalid user role."
            };
        }

        protected virtual string GenerateSha256Hash(string input)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));


            var builder = new StringBuilder();
            foreach (var t in bytes)
            {
                builder.Append(t.ToString("x2"));
            }

            return builder.ToString();
        }
    }
}