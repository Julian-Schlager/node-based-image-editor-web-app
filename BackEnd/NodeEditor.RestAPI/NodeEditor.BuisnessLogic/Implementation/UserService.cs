using NodeEditor.BuisnessLogic.Interfaces;
using NodeEditor.DataAccess;
using NodeEditor.DTO;
using NodeEditor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.BuisnessLogic.Implementation
{
    public class UserService: IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository) 
        {
            this.userRepository = userRepository;
        }

        public async Task<User> Register(RegisterData data)
        {
            if(await this.userRepository.CheckIfUserExists(data.Email))
            {
                throw new ArgumentException("User with this email already exists");
            }

            if (data.Password != data.ConfirmPassword) throw new ArgumentException("Password does not match");
            var passwordData = HashPassword(data.Password);
            User user = new User
            {
                Email = data.Email,
                Password = passwordData.password,
                Salt = passwordData.salt
            };
            return await this.userRepository.CreateAccount(user);
        }

        public async Task<User> LogIn(string email,string password)
        {
            User? user = await this.userRepository.GetAccount(email);
            string hashedPassword = HashPassword(password, user?.Salt).password;
            if(user != null && user?.Password == hashedPassword) return user;
            throw new ArgumentException("Incorrect Email or Password");
        }

        public async Task<bool> DeleteAccount(string email)
        {
            return await this.userRepository.DeleteAccount(email);
        }

        private (string password,string salt) HashPassword(string password,string? saltString = null) //https://code-maze.com/csharp-hashing-salting-passwords-best-practices/
        {
            const int keySize = 64;
            const int iterations = 350000;
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
            byte[] salt = null;

            if(saltString == null) salt = RandomNumberGenerator.GetBytes(keySize);
            else salt = Convert.FromHexString(saltString);

            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);

            return (Convert.ToHexString(hash),Convert.ToHexString(salt));
        }
    }
}
