using NodeEditor.DTO;
using NodeEditor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.BuisnessLogic.Interfaces
{
    public interface IUserService
    {
        Task<User> Register(RegisterData data);
        Task<User> LogIn(string email, string password);
        Task<bool> DeleteAccount(string email);
    }

}
