using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.Json.Serialization;

namespace TweetApp.Models.Common
{
    public class AuditableEntity
    {
        [BsonElement("created_by")]
        [JsonIgnore]
        public string CreatedBy { get; set; }

        [BsonDateTimeOptions]
        [BsonElement("created_on")]
        [JsonIgnore]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [BsonElement("updated_by")]
        [JsonIgnore]
        public string UpdatedBy { get; set; }

        [BsonDateTimeOptions]
        [BsonElement("updated_on")]
        [JsonIgnore]
        public DateTime? UpdatedOn { get; set; }
    }
}
