using Domain_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Interfaces.User
{
    public interface IUserService
    {
        Task<IEnumerable<Entities.User>> GetAllUsersAsync();
        Task<Entities.User> GetUserByIdAsync(int id);
        Task<Entities.User> GetUserByUsernameAsync(string username);
        Task<Entities.User> AddUserAsync(Entities.User user);
        Task<Entities.User> UpdateUserAsync(Entities.User user);
        Task<bool> DeleteUserAsync(int id);
        Task<string> Login(string username, string password);
        Task<bool> Register(string username, string firstName, string lastName, string email, string password, Role role, Address address);
    }
}
