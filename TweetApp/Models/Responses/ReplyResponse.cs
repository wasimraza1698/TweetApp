using System;

namespace TweetApp.Models.Responses
{
    public class ReplyResponse
    {
        public string ReplyId { get; set; }

        public string ReplyText { get; set; }

        public string ReplyTag { get; set; }

        public int ReplyLikesCount { get; set; }

        public bool ReplyLiked { get; set; }

        public string RepliedBy { get; set; }

        public UserResponse RepliedByUser { get; set; }

        public DateTime RepliedAt { get; set; }
    }
}
