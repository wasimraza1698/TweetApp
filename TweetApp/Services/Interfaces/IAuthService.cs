using System.Threading.Tasks;
using TweetApp.Models;

namespace TweetApp.Services.Interfaces
{
    public interface IAuthService
    {
        public string GenerateJWT(string userName);

        public string GetUserNameFromToken();
    }
}
