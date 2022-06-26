using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweetApp.DBSettings;
using TweetApp.Models.DataModels;
using TweetApp.Repositories.Interfaces;

namespace TweetApp.Repositories
{
    public class ReplyRepository : IReplyRepository
    {
        private readonly IMongoCollection<Reply> _repliesCollection;
        public ReplyRepository(IOptions<ReplySettings> replySettings)
        {
            var mongoClient = new MongoClient(replySettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(replySettings.Value.DatabaseName);
            _repliesCollection = mongoDatabase.GetCollection<Reply>(replySettings.Value.CollectionName);
        }

        public async Task AddReply(Reply reply)
        {
            await _repliesCollection.InsertOneAsync(reply);
        }

        public async Task<List<Reply>> GetRepliesOfTweet(string tweetid)
        {
            var replies = await _repliesCollection.FindAsync(r => r.TweetId.Equals(tweetid));
            return await replies.ToListAsync();
        }
    }
}
