using System;
using System.Collections.Generic;

namespace TweetApp.Models.Responses
{
    public class TweetResponse
    {
        public string TweetId { get; set; }

        public string TweetText { get; set; }

        public int TweetLikesCount { get; set; }

        public List<string> TweetLikedBy { get; set; } = new List<string>();

        public string TweetTag { get; set; }

        public bool TweetLiked { get; set; }

        public string TweetedBy { get; set; }

        public DateTime TweetedAt { get; set; }

        public List<ReplyResponse> Replies { get; set; }
    }
}
