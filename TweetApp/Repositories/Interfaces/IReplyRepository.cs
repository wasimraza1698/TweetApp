using System.Collections.Generic;
using System.Threading.Tasks;
using TweetApp.Models.DataModels;

namespace TweetApp.Repositories.Interfaces
{
    public interface IReplyRepository
    {
        public Task AddReply(Reply reply);

        public Task<List<Reply>> GetRepliesOfTweet(string tweetid);
    }
}
