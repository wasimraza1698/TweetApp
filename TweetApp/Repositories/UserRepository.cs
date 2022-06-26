using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweetApp.DBSettings;
using TweetApp.Models;
using TweetApp.Models.DataModels;
using TweetApp.Models.Responses;
using TweetApp.Repositories.Interfaces;

namespace TweetApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _usersCollection;
        public UserRepository(IOptions<UserSettings> userSettings)
        {
            var mongoClient = new MongoClient(userSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(userSettings.Value.DatabaseName);
            _usersCollection = mongoDatabase.GetCollection<User>(userSettings.Value.CollectionName);
        }
        public async Task AddUser(User user)
        {
            await _usersCollection.InsertOneAsync(user);
        }

        public async Task<User> FindUserByUserName(string userName)
        {

            var user = await _usersCollection.FindAsync<User>(u => u.UserName == userName);
            return await user.FirstOrDefaultAsync();
        }

        public async Task<User> FindUserByEmail(string email)
        {
            var user = await _usersCollection.FindAsync<User>(u => u.EmailId == email);
            return await user.FirstOrDefaultAsync();
        }

        public async Task<List<User>> LookUpUsers(string userName)
        {
            var users = await _usersCollection.FindAsync<User>(u => u.UserName.Contains(userName));
            return await users.ToListAsync();
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _usersCollection.FindAsync(u => true);
            return await users.ToListAsync();
        }
    }
}
