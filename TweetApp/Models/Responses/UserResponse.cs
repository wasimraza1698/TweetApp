using MongoDB.Bson;

namespace TweetApp.Models.Responses
{
    public class UserResponse
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string EmailId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ContactNumber { get; set; }
    }
}
