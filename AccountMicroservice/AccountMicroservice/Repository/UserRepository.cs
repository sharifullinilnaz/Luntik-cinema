using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountMicroservice.Models;
using System.Security.Cryptography;
using System.Text;

namespace AccountMicroservice.Repository
{
    public class UserRepository : IUserRepository
    {
        private UserContext Context;
        public IEnumerable<User> Get()
        {
            return Context.Users;
        }
        public User Get(int Id)
        {
            return Context.Users.Find(Id);
        }

        public User GetByLoginForm(string email, string password)
        {
            byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(password);
            byte[] hashedPassword = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            string passwordToCheck = Encoding.Default.GetString(hashedPassword);

            User user = Context.Users.SingleOrDefault(item => item.Email == email);
            if (user!= null && user.HashedPassword == passwordToCheck)
            {
                return user;
            }
            return null;
        }

        public UserRepository(UserContext context)
        {
            Context = context;
        }
        public void Create(User item)
        {
            byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(item.HashedPassword);
            byte[]  hashedPassword = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            item.HashedPassword = Encoding.Default.GetString(hashedPassword);
            Context.Users.Add(item);
            Context.SaveChanges();
        }
        public void Update(User updatedUser)
        {
            byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(updatedUser.HashedPassword);
            byte[] hashedPassword = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            string passwordToSet = Encoding.Default.GetString(hashedPassword);

            User userToUpdate = Get(updatedUser.Id);
            userToUpdate.Email = updatedUser.Email;
            userToUpdate.FirstName = updatedUser.FirstName;
            userToUpdate.LastName = updatedUser.LastName;
            userToUpdate.DateOfBirth = updatedUser.DateOfBirth;
            userToUpdate.PhoneNumber = updatedUser.PhoneNumber;
            userToUpdate.HashedPassword = passwordToSet;
            userToUpdate.IsAdmin = updatedUser.IsAdmin;

            Context.Users.Update(userToUpdate);
            Context.SaveChanges();
        }

        public User Delete(int Id)
        {
            User user = Get(Id);

            if (user != null)
            {
                Context.Users.Remove(user);
                Context.SaveChanges();
            }

            return user;
        }

    }
}
