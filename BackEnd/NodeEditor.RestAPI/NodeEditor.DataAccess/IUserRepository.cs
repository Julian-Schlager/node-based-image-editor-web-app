using NodeEditor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.DataAccess
{
    public interface IUserRepository
    {
        Task<User> CreateAccount(User user);
        Task<User> GetAccount(string email);
        Task<bool> DeleteAccount(string email);
        Task<bool> CheckIfUserExists(string email);
    }
}
