using System.Collections.Generic;
using System.Threading.Tasks;
using TweetApp.Models;
using TweetApp.Models.Requests;
using TweetApp.Models.Responses;

namespace TweetApp.Services.Interfaces
{
    public interface IUserService
    {
        public Task<string> RegisterUser(UserRequest user);

        public Task<(string, string)> LoginUser(Credentials credentials);

        public Task<bool> ResetPassword(ResetPasswordRequest resetPasswordRequest);

        public Task<List<UserResponse>> SearchUserByUserName(string userName);

        public Task<List<UserResponse>> GetAllUsers();
    }
}
