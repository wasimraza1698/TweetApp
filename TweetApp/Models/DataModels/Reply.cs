using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using TweetApp.Models.Common;

namespace TweetApp.Models.DataModels
{
    public class Reply : AuditableEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        public ObjectId ReplyId { get; set; } = ObjectId.GenerateNewId();

        [BsonElement("tweet_id")]
        public string TweetId { get; set; }

        [BsonElement("reply_text")]
        public string ReplyText { get; set; }

        [BsonElement("reply_tag")]
        public string ReplyTag { get; set; }

        [BsonElement("reply_likes_count")]
        public int ReplyLikesCount { get; set; } = 0;

        [BsonElement("reply_liked_by")]
        public List<string> ReplyLikedBy { get; set; } = new List<string>();
    }
}
