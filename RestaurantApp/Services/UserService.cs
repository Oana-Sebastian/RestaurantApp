using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantApp.Models;
using System.Security.Cryptography;
using System.Text;

namespace RestaurantApp.Services
{
    public class UserService : IUserService
    {
        private readonly RestaurantContext _context;

        public UserService(RestaurantContext context)
        {
            _context = context;
        }

        public User? Authenticate(string email, string password) // Updated return type to match IUserService
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Email == email);

            if (user == null)
                return null;

            if (VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return user;

            return null;
        }

        public bool RegisterUser(User user, string password) // Updated return type to match IUserService
        {
            // Check if the email is already registered
            bool emailExists = _context.Users.Any(u => u.Email == user.Email);
            if (emailExists)
                return false;

            CreatePasswordHash(password, out string hash, out string salt);

            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            _context.Users.Add(user);
            _context.SaveChanges();

            return true;
        }

        private void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = Convert.ToBase64String(hmac.Key);
            passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

        private bool VerifyPasswordHash(string password, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            using var hmac = new HMACSHA512(saltBytes);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            var computedHashString = Convert.ToBase64String(computedHash);
            return computedHashString == storedHash;
        }
    }
}
