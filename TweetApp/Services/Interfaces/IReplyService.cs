using System.Threading.Tasks;
using TweetApp.Models.Requests;

namespace TweetApp.Services.Interfaces
{
    public interface IReplyService
    {
        public Task<string> AddReplyToTweet(string username, string tweetid, ReplyRequest reply);
    }
}
