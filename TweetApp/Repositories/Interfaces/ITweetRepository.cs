using System.Collections.Generic;
using System.Threading.Tasks;
using TweetApp.Models.DataModels;

namespace TweetApp.Repositories.Interfaces
{
    public interface ITweetRepository
    {
        public Task AddTweet(Tweet tweet);

        public Task<List<Tweet>> GetAllTweets();

        public Task<List<Tweet>> GetAllTweetsOfUser(string username);

        public Task<Tweet> GetTweetById(string tweetid);

        public Task UpdateTweet(Tweet tweet);

        public Task DeleteTweet(string tweetid);
    }
}
