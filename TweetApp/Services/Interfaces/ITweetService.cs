using System.Collections.Generic;
using System.Threading.Tasks;
using TweetApp.Models.Requests;
using TweetApp.Models.Responses;

namespace TweetApp.Services.Interfaces
{
    public interface ITweetService
    {
        public Task<string> PostTweet(string username, TweetRequest tweet);

        public Task<List<TweetResponse>> GetAllTweets();

        public Task<List<TweetResponse>> GetAllTweetsOfUser(string username);

        public Task<TweetResponse> UpdateTweet(string username, string tweetid, TweetRequest tweet);

        public Task<bool> DeleteTweet(string username, string tweetid);

        public Task<TweetResponse> LikeTweet(string username, string tweetid);

        public Task<TweetResponse> GetTweetById(string tweetid);
    }
}
