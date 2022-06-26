using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;
using TweetApp.Models.Common;

namespace TweetApp.Models.DataModels
{
    public class User : AuditableEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        [JsonIgnore]
        public ObjectId UserId { get; set; } = ObjectId.GenerateNewId();

        [BsonElement("user_name")]
        public string UserName { get; set; }

        [BsonElement("email_id")]
        public string EmailId { get; set; }

        [BsonElement("first_name")]
        public string FirstName { get; set; }

        [BsonElement("last_name")]
        public string LastName { get; set; }

        [BsonElement("contact_number")]
        public string ContactNumber { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }
    }
}
