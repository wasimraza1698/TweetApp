using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweetApp.DBSettings;
using TweetApp.Models.DataModels;
using TweetApp.Repositories.Interfaces;

namespace TweetApp.Repositories
{
    public class TweetRepository : ITweetRepository
    {
        private readonly IMongoCollection<Tweet> _tweetsCollection;
        public TweetRepository(IOptions<TweetSettings> tweetSettings)
        {
            var mongoClient = new MongoClient(tweetSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(tweetSettings.Value.DatabaseName);
            _tweetsCollection = mongoDatabase.GetCollection<Tweet>(tweetSettings.Value.CollectionName);
        }
        public async Task AddTweet(Tweet tweet)
        {
            await _tweetsCollection.InsertOneAsync(tweet);
        }

        public async Task UpdateTweet(Tweet tweet)
        {
            await _tweetsCollection.ReplaceOneAsync<Tweet>(t => t.TweetId == tweet.TweetId, tweet);
        }

        public async Task DeleteTweet(string tweetid)
        {
            await _tweetsCollection.DeleteOneAsync<Tweet>(t => t.TweetId == ObjectId.Parse(tweetid));
        }

        public async Task<List<Tweet>> GetAllTweets()
        {
            var tweets = await _tweetsCollection.FindAsync(t => true);
            return await tweets.ToListAsync();
        }

        public async Task<List<Tweet>> GetAllTweetsOfUser(string username)
        {
            var tweets = await _tweetsCollection.FindAsync(t => t.CreatedBy.Equals(username));
            return await tweets.ToListAsync();
        }

        public async Task<Tweet> GetTweetById(string tweetid)
        {
            var tweet = await _tweetsCollection.FindAsync(t => t.TweetId == ObjectId.Parse(tweetid));
            return await tweet.FirstOrDefaultAsync();
        }
    }
}
