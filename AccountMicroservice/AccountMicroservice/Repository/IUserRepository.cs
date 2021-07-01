using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountMicroservice.Models;

namespace AccountMicroservice.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> Get();
        User Get(int id);
        void Create(User item);
        void Update(User item);
        User Delete(int id);
        User GetByLoginForm(string email, string password);
        void ChangeRole(int id);
    }
}
