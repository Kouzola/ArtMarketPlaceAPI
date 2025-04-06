using Business_Layer.Exceptions;
using Domain_Layer.Entities;
using Domain_Layer.Interfaces.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<User> AddUserAsync(User user)
        {
            var salt = "" + user.FirstName.ElementAt(0) + user.LastName.ElementAt(0) + user.LastName.ElementAt(user.LastName.Length - 1) + DateTime.Now.DayOfWeek;
            user.Password = HashPassword(user.Password, salt);
            return await _userRepository.AddUserAsync(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
             return await _userRepository.DeleteUserAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) throw new NotFoundException("User not found!");
            return user;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null) throw new NotFoundException("User not found!");
            return user;
        }

        public async Task<string> Login(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            var loginException = new NotFoundException("This username or password is invalid!");

            if (user == null) throw loginException;

            var salt = "" + user.FirstName.ElementAt(0) + user.LastName.ElementAt(0) + user.LastName.ElementAt(user.LastName.Length - 1) + user.CreatedAt.DayOfWeek;
            var hashPassword = HashPassword(password, salt);
            if(hashPassword == user.Password)
            {
                var token = GenerateToken(user);
                return token;
            }

            throw loginException;
        }

        public async Task<bool> Register(string username, string firstName, string lastName, string email, string password, Role role)
        {
            //Test pour voir si userName existe déja ?
            if (await _userRepository.GetUserByUsernameAsync(username) != null)
            {
                throw new AlreadyExistException("This username already exists or is invalid!");
            }
            //Email existe déja ?
            if (await _userRepository.GetUserByEmailAsync(email) != null)
            {
                throw new AlreadyExistException("This email is already used or invalid!");
            }

            string salt = "" + firstName.ElementAt(0) + lastName.ElementAt(0) + lastName.ElementAt(lastName.Length - 1) + DateTime.Now.DayOfWeek;

            var newUser = new User 
            { 
                UserName = username, 
                Password = HashPassword(password, salt), 
                FirstName = firstName, 
                LastName = lastName, 
                Email = email, 
                Role = role, 
            };

            if (await AddUserAsync(newUser) != null) return true;
            else return false;

        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var userToUpdated = await _userRepository.GetUserByIdAsync(user.Id);
            if (userToUpdated == null) throw new NotFoundException("User not found!");

            //Test pour voir si userName existe déja ?
            if (userToUpdated.UserName != user.UserName && await _userRepository.GetUserByUsernameAsync(user.UserName) != null)
            {
                throw new AlreadyExistException("This username already exists or is invalid!");
            }
            //Email existe déja ?
            if (userToUpdated.Email != user.Email && await _userRepository.GetUserByEmailAsync(user.Email) != null)
            {
                throw new AlreadyExistException("This email is already used or invalid!");
            }

            var salt = "" + user.FirstName.ElementAt(0) + user.LastName.ElementAt(0) + user.LastName.ElementAt(user.LastName.Length - 1) + userToUpdated.CreatedAt.DayOfWeek;
            if (!string.IsNullOrWhiteSpace(user.Password)) user.Password = HashPassword(user.Password, salt);       
            var userUpdated = await _userRepository.UpdateUserAsync(user);
         
            return userUpdated == null ? throw new NotFoundException("User not found!") : userUpdated;
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            //Clé
            var key = _configuration["JwtConfig:Key"]!;
            //Security Key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            //Claim
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim("id", user.Id.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                Issuer = _configuration["JwtConfig:Issuer"],
                Audience = _configuration["JwtConfig:Audience"],
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string HashPassword(string password, string salt)
        {
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                    password: Encoding.UTF8.GetBytes(password),
                    salt: Encoding.UTF8.GetBytes(salt),
                    iterations: 10,
                    hashAlgorithm: HashAlgorithmName.SHA512,
                    outputLength: 16);

            return Convert.ToHexString(hash);
        }


    }
}
