using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using TweetApp.Models.Common;

namespace TweetApp.Models.DataModels
{
    public class Tweet : AuditableEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        [BsonElement("tweet_id")]
        public ObjectId TweetId { get; set; } = ObjectId.GenerateNewId();

        [BsonElement("tweet_text")]
        public string TweetText { get; set; }

        [BsonElement("tweet_likes_count")]
        public int TweetLikesCount { get; set; } = 0;

        [BsonElement("tweet_liked_by")]
        public List<string> TweetLikedBy { get; set; } = new List<string>();

        [BsonElement("tweet_tag")]
        public string TweetTag { get; set; }
    }
}
