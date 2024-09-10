using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Homework_3._1
{
    public class UserRepository
    {
        private readonly ApplicationContext _context;
        public UserRepository(ApplicationContext context) //получаем обьект через конструктор ApplicationContext
        {
            _context = context;
        }

        public bool ReguisterUser(User user)
        {
            if (_context.Users.Any(u => u.UserName == user.UserName))
            {
                //Halper.WriteErrorMessage("User with this username already exists.");
                return false;
            }
            string passwordHash = ComputeHash(user.PasswordHash);
            user.PasswordHash = passwordHash;
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }

        public bool AuthenticateUser(User user)
        {
            string passwordHash = ComputeHash(user.PasswordHash);
            return _context.Users.Any(u => u.UserName == user.UserName && u.PasswordHash == passwordHash);
        }

        private string ComputeHash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
