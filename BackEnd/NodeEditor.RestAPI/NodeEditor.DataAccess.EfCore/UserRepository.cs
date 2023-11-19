using Microsoft.EntityFrameworkCore;
using NodeEditor.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.DataAccess.EfCore
{
    public class UserRepository : IUserRepository
    {
        private readonly NodeEditorContext context;

        public UserRepository(NodeEditorContext context) {
            this.context = context;
        
        }

        public async Task<User> CreateAccount(User user)
        {
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<User> userRes = await this.context.Users.AddAsync(user);
            this.context.SaveChanges();
            return userRes.Entity;
        }

        public async Task<User> GetAccount(string email)
        {
            return await this.context.Users.AsNoTracking().FirstOrDefaultAsync(x=>x.Email== email);
        }

        public async Task<bool> DeleteAccount(string email)
        {

            int deletedRows = await this.context.Users.Where(x => x.Email == email).ExecuteDeleteAsync();
            return deletedRows > 0;
        }

        public async Task<bool> CheckIfUserExists(string email)
        {
            return await this.context.Users.AnyAsync(x => x.Email == email);
        }
    }
}
