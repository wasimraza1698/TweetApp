using System.Collections.Generic;
using System.Threading.Tasks;
using TweetApp.Models.DataModels;

namespace TweetApp.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task AddUser(User user);

        public Task UpdateUser(User user);

        public Task<User> FindUserByUserName(string userName);

        public Task<User> FindUserByEmail(string email);

        public Task<List<User>> LookUpUsers(string userName);

        public Task<List<User>> GetAllUsers();
    }
}
